namespace PlaceHolder.QueryModel.Consumers
{
    public interface IConsumerQueryRepository
    {
        Task<ConsumerDto> GetOneByIdAsync(Guid guid);
        Task<List<ConsumerDto>> GetAllAsync(int limit = 10);
    }
}
