using AutoMapper;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate;
using PlaceHolder.DrivenAdapter.SQLServer.EFCore.Entities;

namespace PlaceHolder.DrivenAdapter.SQLServer.ACL
{
    public class ApplicationCoreToInfrastructureMap : Profile
    {
        public ApplicationCoreToInfrastructureMap()
        {
            CreateMap<Consumer, ConsumerEntity>()
                .ForMember(destination => destination.Email, 
                    opts => opts.MapFrom(source => (string)source.Email))
                .ForMember(destination => destination.PhoneNumber, 
                    opts => opts.MapFrom(source => (string)source.PhoneNumber));

            CreateMap<Address, AddressEntity>();
        }
    }
}

