using AutoMapper;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate;
using PlaceHolder.DrivenAdapter.SQLServer.EFCore.Entities;

namespace PlaceHolder.DrivenAdapter.SQLServer.ACL
{
    public class InfrastructureToApplicationCoreMap : Profile
    {
        public InfrastructureToApplicationCoreMap()
        {
            CreateMap<ConsumerEntity, Consumer>()
                .ForMember(d => d.Email, opts => opts.Ignore())
                .ForMember(d => d.PhoneNumber, opts => opts.Ignore());

            CreateMap<AddressEntity, Address>();
        }
    }
}
