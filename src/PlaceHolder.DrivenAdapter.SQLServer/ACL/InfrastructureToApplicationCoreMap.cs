using AutoMapper;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate;
using DbConsumer = PlaceHolder.DrivenAdapter.SQLServer.EFCore.Entities.Consumer;
using DbAddress = PlaceHolder.DrivenAdapter.SQLServer.EFCore.Entities.Address;

namespace PlaceHolder.DrivenAdapter.SQLServer.ACL
{
    public class InfrastructureToApplicationCoreMap : Profile
    {
        public InfrastructureToApplicationCoreMap()
        {
            CreateMap<Consumer, DbConsumer>();
            CreateMap<Address, DbAddress>();
        }
    }
}
