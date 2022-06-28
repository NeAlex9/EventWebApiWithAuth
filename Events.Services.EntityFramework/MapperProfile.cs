using AutoMapper;
using Events.Services.Entities;
using Events.Services.EntityFramework.Entities;

namespace Events.Services.EntityFramework
{
    internal class MapperProfile : Profile
    {
        public MapperProfile()
        {
            this.CreateMap<Event, EventDTO>()
                .ReverseMap();

            this.CreateMap<Organizer, OrganizerDTO>()
                .ReverseMap();

            this.CreateMap<Speaker, SpeakerDTO>()
                .ReverseMap();
        }
    }
}
