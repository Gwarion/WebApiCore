using AutoMapper;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate;
using PlaceHolder.DrivenAdapter.SQLServer.EFCore.Entities;

namespace PlaceHolder.DrivenAdapter.SQLServer.ACL
{
    public class InfrastructureToDomainModelMap : Profile
    {
        public InfrastructureToDomainModelMap()
        {
            CreateMap<ConsumerEntity, Consumer>()
                .ForMember(destination => destination.Email, opts => opts.Ignore())
                .ForMember(destination => destination.PhoneNumber, opts => opts.Ignore());

            CreateMap<AddressEntity, Address>();
        }
    }
}
