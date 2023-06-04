using System.Threading.Tasks;

namespace PlaceHolder.Application.Services.Ports.Cqrs
{
    public interface IAsyncCommandDispatcher
    {
        Task Send(IAsyncCommand asyncCommand);
    }
}
