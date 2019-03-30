using CinnabunsFinal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CinnabunsFinal
{
    public class AppContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactPhone> ContactPhones { get; set; }
        public DbSet<ContactEmail> ContactEmails { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventPartner> EventPartners { get; set; }
        public DbSet<Interaction> Interactions { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagPartner> TagPartners { get; set; }
        public DbSet<Task> Tasks { get; set; }

        public AppContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EventPartner>().HasKey(ep => new { ep.EventId, ep.PartnerId });
            modelBuilder.Entity<TagPartner>().HasKey(tp => new { tp.TagId, tp.PartnerId });

            modelBuilder.Entity<EventPartner>().HasOne(ep => ep.Event)
                                               .WithMany(e => e.EventPartners)
                                               .HasForeignKey(ep => ep.EventId);

            modelBuilder.Entity<EventPartner>().HasOne(ep => ep.Partner)
                                               .WithMany(e => e.EventPartners)
                                               .HasForeignKey(ep => ep.PartnerId);

            modelBuilder.Entity<TagPartner>().HasOne(ep => ep.Tag)
                                             .WithMany(e => e.TagPartners)
                                             .HasForeignKey(ep => ep.TagId);

            modelBuilder.Entity<TagPartner>().HasOne(ep => ep.Partner)
                                             .WithMany(e => e.TagPartners)
                                             .HasForeignKey(ep => ep.PartnerId);
        }
    }  
}
