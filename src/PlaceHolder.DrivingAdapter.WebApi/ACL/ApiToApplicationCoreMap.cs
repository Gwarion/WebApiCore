using AutoMapper;
using PlaceHolder.API.Controllers.Consumers.Dtos;
using PlaceHolder.Application.Logic.Commands.Consumers;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate;
using System;

namespace PlaceHolder.DrivingAdapter.WebApi.ACL
{
    class ApiToApplicationCoreMap : Profile
    {
        public ApiToApplicationCoreMap()
        {
            CreateMap<AddressDto, Address>();

            CreateMap<ConsumerDto, CreateConsumerCommand>();

            CreateMap<ConsumerDto, UpdateConsumerCommand>()
                .ForMember(destination => destination.Guid, opts => opts.MapFrom(source => Guid.Parse(source.Guid)));
        }
    }
}
