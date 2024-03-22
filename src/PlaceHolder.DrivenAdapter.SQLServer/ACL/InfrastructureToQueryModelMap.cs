using AutoMapper;
using PlaceHolder.DrivenAdapter.SQLServer.EFCore.Entities;
using PlaceHolder.QueryModel.Consumers;

namespace PlaceHolder.DrivenAdapter.SQLServer.ACL
{
    public class InfrastructureToQueryModelMap : Profile
    {
        public InfrastructureToQueryModelMap()
        {
            CreateMap<ConsumerEntity, ConsumerDto>();

            CreateMap<AddressEntity, string>()
                .ConvertUsing(address => $"{address.Street}, {address.PostalCode} {address.City} ({address.Country.ToUpper()})");             
        }
    }
}
