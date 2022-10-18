using MediatR;

namespace PlaceHolder.Application.Services.Ports.Cqrs
{
    public interface IQuery<TResult> : IRequest<TResult>
    {
    }
}
