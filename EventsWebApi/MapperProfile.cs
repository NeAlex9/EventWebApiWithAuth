using AutoMapper;
using Events.Services.Entities;
using EventsWebApi.Models;

namespace EventsWebApi
{
    internal class MapperProfile : Profile
    {
        public MapperProfile()
        {
            this.CreateMap<Event, EventToCreate>()
                .ReverseMap();

            this.CreateMap<Event, EventToUpdate>()
                .ReverseMap();
        }
    }
}
