using AutoMapper;
using PlaceHolder.API.Controllers.Consumers.Resources;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate;

namespace PlaceHolder.DrivingAdapter.WebApi.ACL
{
    public class ApplicationCoreToApiMap : Profile
    {
        public ApplicationCoreToApiMap()
        {
            CreateMap<Address, AddressResource>();

            CreateMap<Consumer, ConsumerResource>()
                .ForMember(destination => destination.Email,
                    opts => opts.Ignore())
                .ForMember(destination => destination.PhoneNumber,
                    opts => opts.Ignore());
        }
    }
}