using PlaceHolder.Application.Services.Ports.Cqrs;
using System.Threading.Tasks;

namespace PlaceHolder.Application.Services.BackgroundServices
{
    public class AsyncCommandDispatcher : IAsyncCommandDispatcher
    {
        private readonly BackgroundRequestQueueManager _queueManager;

        public AsyncCommandDispatcher(BackgroundRequestQueueManager queueManager)
            => _queueManager = queueManager;

        public async Task Send(IAsyncCommand asyncCommand)
        {
            _queueManager.Enqueue(asyncCommand);

            await Task.CompletedTask;
        }
    }
}
