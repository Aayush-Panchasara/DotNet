using Event_Management_System.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Event_Management_System.Data
{
    public class AppDBContext(DbContextOptions<AppDBContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventRegistration> EventRegistrations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventRegistration>()
                        .HasKey(r => new {r.AttendeeId, r.EventId });

            modelBuilder.Entity<EventRegistration>()
                        .HasOne(r => r.Attendee)
                        .WithMany(a => a.RegisterEvents)
                        .HasForeignKey(r => r.AttendeeId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EventRegistration>()
                        .HasOne(r => r.Event)
                        .WithMany(e => e.RegisterUsers)
                        .HasForeignKey(e => e.EventId)
                        .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
