using AutoMapper;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate;
using DbConsumer = PlaceHolder.DrivenAdapter.SQLServer.EFCore.Entities.Consumer;

namespace PlaceHolder.DrivenAdapter.SQLServer.ACL
{
    public class InfrastructureToApplicationCoreMap : Profile
    {
        public InfrastructureToApplicationCoreMap()
        {
            CreateMap<Consumer, DbConsumer>();
        }
    }
}
