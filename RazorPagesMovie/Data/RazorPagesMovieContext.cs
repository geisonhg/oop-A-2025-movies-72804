using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Data
{
    public class RazorPagesMovieContext : DbContext
    {
        public RazorPagesMovieContext(DbContextOptions<RazorPagesMovieContext> options)
            : base(options)
        {
        }

        // This method configures the model, defining relationships and column types
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Definimos la clave compuesta para la tabla intermedia MovieActor
            modelBuilder.Entity<MovieActor>()
                .HasKey(ma => new { ma.MovieId, ma.ActorId });

            modelBuilder.Entity<MovieActor>()
                .HasOne(ma => ma.Movie)   // Relación con la tabla Movie
                .WithMany(m => m.MovieActors) // Relación de "muchos a muchos"
                .HasForeignKey(ma => ma.MovieId);

            modelBuilder.Entity<MovieActor>()
                .HasOne(ma => ma.Actor)   // Relación con la tabla Actor
                .WithMany(a => a.MovieActors) // Relación de "muchos a muchos"
                .HasForeignKey(ma => ma.ActorId);
        }

        public DbSet<RazorPagesMovie.Models.Movie> Movie { get; set; } = default!;
        public DbSet<RazorPagesMovie.Models.Actor> Actor { get; set; } = default!;
        public DbSet<MovieActor> MovieActor { get; set; } = default!;
    }
}
