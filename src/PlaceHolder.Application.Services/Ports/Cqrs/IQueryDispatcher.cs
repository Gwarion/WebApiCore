using System.Threading.Tasks;

namespace PlaceHolder.Application.Services.Ports.Cqrs
{
    public interface IQueryDispatcher
    {
        Task<TResult> DispatchAsync<TResult>(IQuery<TResult> query);
    }
}
