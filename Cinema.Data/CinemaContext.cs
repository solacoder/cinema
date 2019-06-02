using CinemaApp.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CinemaApp.Data
{
    public class CinemaContext : IdentityDbContext
    {
        public CinemaContext(DbContextOptions<CinemaContext> options) : base(options)
        {
          
        }
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            foreach (var relationship in modelbuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelbuilder.Entity("CinemaApp.Core.Models.CinemaScreen", b =>
            {
                b.HasOne("CinemaApp.Core.Models.Cinema", "Cinema")
                    .WithMany("Screens")
                    .HasForeignKey("CinemaId")
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne("CinemaApp.Core.Models.CinemaOwner", "CinemaOwner")
                    .WithMany()
                    .HasForeignKey("CinemaOwnerId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelbuilder);
        }
        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<CinemaOwner> CinemaOwners { get; set; }
        public virtual DbSet<Cinema> Cinemas { get; set; }
        public virtual DbSet<CinemaScreen> CinemaScreens { get; set; }
        public virtual DbSet<MovieCategory> MovieCategories { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Producer> Producers { get; set; }
        public virtual DbSet<ShowTime> ShowTimes { get; set; }
        public virtual DbSet<AppUser> AppUsers { get; set; }
    }
}
