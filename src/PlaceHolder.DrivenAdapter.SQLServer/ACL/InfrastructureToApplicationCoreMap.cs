using AutoMapper;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate;
using PlaceHolder.DrivenAdapter.SQLServer.EFCore.Entities;

namespace PlaceHolder.DrivenAdapter.SQLServer.ACL
{
    public class InfrastructureToApplicationCoreMap : Profile
    {
        public InfrastructureToApplicationCoreMap()
        {
            CreateMap<ConsumerEntity, Consumer>();
            CreateMap<AddressEntity, Address>();
        }
    }
}
