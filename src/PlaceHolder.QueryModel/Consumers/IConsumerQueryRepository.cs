namespace PlaceHolder.QueryModel.Consumers
{
    public interface IConsumerQueryRepository
    {
        Task<ConsumerDto> GetOneByIdAsync(Guid guid);
        Task<List<ConsumerDto>> GetAllAsync(int limit = 10);
        Task<IEnumerable<ConsumerDto>> GetAllAsync(int startId, int chunkSize, CancellationToken token);
    }
}
