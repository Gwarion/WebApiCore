using PlaceHolder.Application.Services.Ports.Cqrs;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace PlaceHolder.Application.Services.BackgroundServices
{
    public sealed class BackgroundRequestQueueManager
    {
        private readonly ConcurrentQueue<IAsyncCommand> _queue = new();
        private readonly ReaderWriterLockSlim _lock = new();
        private bool _canDequeue = false;

        public void Enqueue(IAsyncCommand command)
        {
            _lock.EnterWriteLock();

            try
            {
                _queue.Enqueue(command);
            }
            finally
            {
                _canDequeue = !_queue.IsEmpty;
                _lock.ExitWriteLock();
            }
        }

        public async Task<IAsyncCommand> DequeueAsync(CancellationToken cancellationToken)
        {
            if (!_canDequeue) await PoolAsync(cancellationToken);
            if (cancellationToken.IsCancellationRequested) return null;

            if(_lock.TryEnterReadLock(10))
            {
                try
                {
                    _queue.TryDequeue(out var request);
                    return request;
                }
                finally
                {
                    _canDequeue = !_queue.IsEmpty;
                    _lock.ExitReadLock();
                }
            }

            return null;
        }

        private async Task PoolAsync(CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                SpinWait.SpinUntil(() =>
                {
                    Thread.MemoryBarrier();
                    return _canDequeue || cancellationToken.IsCancellationRequested;
                });
            });
        }
    }
}
