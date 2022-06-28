using Events.Services.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace Events.Services.EntityFramework.Context
{
    public class EventContext : DbContext
    {
        public EventContext(DbContextOptions<EventContext> options)
            : base(options)
        {
        }

        public DbSet<EventDTO> Events { get; set; }
        public DbSet<OrganizerDTO> Organizers { get; set; }
        public DbSet<SpeakerDTO> Speakers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<EventDTO>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
