using Events.Services.EntityFramework.Entities;
using Microsoft.AspNetCore.Identity;
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
        public DbSet<UserDTO> Users { get; set; }
        public DbSet<RoleDTO> Roles { get; set; }
        public DbSet<UserRoleDTO> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.HasDefaultSchema("Identity");

            modelBuilder
                .Entity<EventDTO>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            modelBuilder
                .Entity<EventDTO>()
                .HasOne(e => e.Speaker)
                .WithMany();

            modelBuilder
                .Entity<EventDTO>()
                .HasOne(e => e.Organizer)
                .WithMany();

            modelBuilder.Entity<UserRoleDTO>()
                .HasKey(ur => new {ur.UserId, ur.RoleId});

            modelBuilder.Entity<UserDTO>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<UserRoleDTO>(
                    right => right.HasOne<RoleDTO>().WithMany().HasForeignKey(x => x.RoleId) ,
                    left => left.HasOne<UserDTO>().WithMany().HasForeignKey(x => x.UserId),
                    join => join.ToTable("UserRoles"));
        }
    }
}
