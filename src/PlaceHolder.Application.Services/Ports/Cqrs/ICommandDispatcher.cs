using System.Threading.Tasks;

namespace PlaceHolder.Application.Services.Ports.Cqrs
{
    public interface ICommandDispatcher
    {
        Task<T> DispatchAsync<T>(ICommand<T> command);
        Task DispatchAsync(IAsyncCommand command);
    }
}
