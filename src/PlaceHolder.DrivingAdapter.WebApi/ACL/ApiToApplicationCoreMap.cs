using AutoMapper;
using PlaceHolder.API.Controllers.Consumers;
using PlaceHolder.Application.Logic.Commands.Consumers;
using System;

namespace PlaceHolder.DrivingAdapter.WebApi.ACL
{
    class ApiToApplicationCoreMap : Profile
    {
        public ApiToApplicationCoreMap()
        {
            CreateMap<ConsumerDto, CreateConsumerCommand>();
            CreateMap<ConsumerDto, UpdateConsumerCommand>()
                .ForMember(destination => destination.Guid, opts => opts.MapFrom(source => Guid.Parse(source.Guid)));
        }
    }
}
