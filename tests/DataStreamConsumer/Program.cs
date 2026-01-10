using PlaceHolder.QueryModel.Consumers;
using System.Net.Http.Json;

var client = new HttpClient();

try
{


    await foreach (var consumer in client.GetFromJsonAsAsyncEnumerable<ConsumerDto>("http://localhost:55000/api/v1/Consumer/data-stream"))
    {
        if (consumer is not null)
            Console.WriteLine($"{consumer.Guid} - {consumer.FirstName} - {consumer.LastName} - {consumer.Email} - {consumer.PhoneNumber}");
        else
            Console.WriteLine("null");
    }
}
catch (Exception e)
{
    Console.WriteLine(e.Message);

    if (e.InnerException is not null)
        Console.WriteLine($"{e.InnerException.Message}");
}

Console.ReadLine();