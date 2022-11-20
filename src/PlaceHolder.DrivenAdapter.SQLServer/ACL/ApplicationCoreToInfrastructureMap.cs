using AutoMapper;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate;
using DbConsumer = PlaceHolder.DrivenAdapter.SQLServer.EFCore.Entities.Consumer;
using DbAddress = PlaceHolder.DrivenAdapter.SQLServer.EFCore.Entities.Address;

namespace PlaceHolder.DrivenAdapter.SQLServer.ACL
{
    public class ApplicationCoreToInfrastructureMap : Profile
    {
        public ApplicationCoreToInfrastructureMap()
        {
            CreateMap<DbConsumer, Consumer>();
            CreateMap<DbAddress, Address>();
        }
    }
}

