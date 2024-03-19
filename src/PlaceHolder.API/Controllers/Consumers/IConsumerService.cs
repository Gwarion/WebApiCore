using PlaceHolder.API.Controllers.Consumers.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlaceHolder.API.Controllers.Consumers
{
    public interface IConsumerService
    {
        Task<ConsumerResource> CreateConsumerAsync(ConsumerResource consumerResource);
        Task<ConsumerResource> UpdateConsumerAsync(ConsumerResource consumerResource);
        Task<ConsumerResource> GetOneByIdAsync(string guid);
        Task<List<ConsumerResource>> GetAllAsync();
    }
}
