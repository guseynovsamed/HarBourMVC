using System;
using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repository.Data
{
	public class AppDbContext : IdentityDbContext<AppUser>
    {
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<About> Abouts { get; set; }
        public DbSet<AboutImage> AboutImages { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ComplaintSuggest> ComplaintSuggests { get; set; }
        public DbSet<DestinationCity> DestinationCities { get; set; }
        public DbSet<DestinationCountry> DestinationCountries { get; set; }
        public DbSet<DestinationImage> DestinationImages { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<SliderVideo> SliderVideos { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<WaterSport> WaterSports { get; set; }
        public DbSet<WaterSportImage> WaterSportImages { get; set; }
        public DbSet<Yacht> Yachts { get; set; }
        public DbSet<YachtCategory> YachtCategories { get; set; }
        public DbSet<YachtImage> YachtImages { get; set; }
        public DbSet<Reservation> Reservations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

