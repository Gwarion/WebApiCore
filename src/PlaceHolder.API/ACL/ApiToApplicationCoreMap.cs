using AutoMapper;
using PlaceHolder.API.Controllers.Consumers.Resources;
using PlaceHolder.Application.Logic.Commands.Consumers;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate;

namespace PlaceHolder.API.ACL
{
    public class ApiToApplicationCoreMap : Profile
    {
        public ApiToApplicationCoreMap()
        {
            CreateMap<AddressResource, Address>();

            CreateMap<ConsumerResource, CreateConsumerCommand>();

            CreateMap<ConsumerResource, UpdateConsumerCommand>();
        }
    }
}
