using AutoMapper;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate;
using DbConsumer = PlaceHolder.DrivenAdapter.SQLServer.EFCore.Entities.Consumer;

namespace PlaceHolder.DrivenAdapter.SQLServer.ACL
{
    public class ApplicationCoreToInfrastructureMap : Profile
    {
        public ApplicationCoreToInfrastructureMap()
        {
            CreateMap<DbConsumer, Consumer>();
        }
    }
}

