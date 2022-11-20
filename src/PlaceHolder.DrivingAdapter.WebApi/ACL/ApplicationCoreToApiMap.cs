using AutoMapper;
using PlaceHolder.API.Controllers.Consumers.Dtos;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate;

namespace PlaceHolder.DrivingAdapter.WebApi.ACL
{
    public class ApplicationCoreToApiMap : Profile
    {
        public ApplicationCoreToApiMap()
        {
            CreateMap<Consumer, ConsumerDto>()
                .ForMember(destination => destination.Guid, opts => opts.MapFrom(source => source.Guid.ToString()));
        }
    }
}
