using PlaceHolder.QueryModel.Consumers;
using System.Net.Http.Json;

using var cts = new CancellationTokenSource();

Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true;
    cts.Cancel();
};

Console.WriteLine("Select data format:");
Console.WriteLine("1. JSON streaming (data-stream)");
Console.WriteLine("2. CSV streaming (csv-data-stream)");
Console.Write("Choice: ");
var choice = Console.ReadLine()?.Trim();

while (choice != "1" && choice != "2")
{
    Console.Write("Choice: ");
    choice = Console.ReadLine()?.Trim();
}

using var client = new HttpClient();

try
{
    if (choice == "1")
    {
        await ProcessJsonStreamingAsync(client, cts.Token);
    }
    else
    {
        await ProcessCsvStreamingAsync(client, cts.Token);
    }
}
catch (OperationCanceledException)
{
    Console.WriteLine("Streaming cancelled by user.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

//Json Streaming
static async Task ProcessJsonStreamingAsync(HttpClient client, CancellationToken cancellationToken)
{
    var url = "http://localhost:55000/api/v1/Consumer/data-stream";

    await foreach (var chunk in client.GetFromJsonAsAsyncEnumerable<List<ConsumerDto>>(url, cancellationToken))
    {
        if (chunk is null) continue;

        foreach (var consumer in chunk)
        {
            if (consumer is null)
            {
                Console.WriteLine("null");
                continue;
            }

            Console.WriteLine($"{consumer.Guid} - {consumer.FirstName} - {consumer.LastName} - {consumer.Email} - {consumer.PhoneNumber}");
        }
    }
}

//CSV Streaming
static async Task ProcessCsvStreamingAsync(HttpClient client, CancellationToken cancellationToken)
{
    var url = "http://localhost:55000/api/v1/Consumer/csv-data-stream";

    using var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
    response.EnsureSuccessStatusCode();

    await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
    using var reader = new StreamReader(stream);

    string? line;
    while ((line = await reader.ReadLineAsync()) is not null)
    {
        cancellationToken.ThrowIfCancellationRequested();
        Console.WriteLine(line);
    }
}