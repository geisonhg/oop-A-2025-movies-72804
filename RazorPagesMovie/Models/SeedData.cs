using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RazorPagesMovie.Data;
using Bogus;
using System;
using System.Linq;

namespace RazorPagesMovie.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new RazorPagesMovieContext(
                serviceProvider.GetRequiredService<DbContextOptions<RazorPagesMovieContext>>()))
            {
                if (context == null || context.Movie == null)
                {
                    throw new ArgumentNullException("Null RazorPagesMovieContext");
                }

                // ========================
                // Seed Movies if none exist
                // ========================
                if (!context.Movie.Any())
                {
                    context.Movie.AddRange(
                        new Movie
                        {
                            Title = "When Harry Met Sally",
                            ReleaseDate = DateTime.Parse("1989-2-12"),
                            Genre = "Romantic Comedy",
                            Price = 7.99M,
                            Rating = "R",
                            Director = "Rob Reiner",
                            Cast = "Billy Crystal, Meg Ryan",
                            IMDbRating = 7.6M,
                            BoxOfficeRevenue = 92800000,
                            ReleaseCountry = "USA"
                        },
                        new Movie
                        {
                            Title = "Ghostbusters",
                            ReleaseDate = DateTime.Parse("1984-3-13"),
                            Genre = "Comedy",
                            Price = 8.99M,
                            Rating = "G",
                            Director = "Ivan Reitman",
                            Cast = "Bill Murray, Dan Aykroyd",
                            IMDbRating = 7.8M,
                            BoxOfficeRevenue = 295000000,
                            ReleaseCountry = "USA"
                        },
                        new Movie
                        {
                            Title = "Ghostbusters 2",
                            ReleaseDate = DateTime.Parse("1986-2-23"),
                            Genre = "Comedy",
                            Price = 9.99M,
                            Rating = "G",
                            Director = "Ivan Reitman",
                            Cast = "Bill Murray, Dan Aykroyd",
                            IMDbRating = 6.6M,
                            BoxOfficeRevenue = 215000000,
                            ReleaseCountry = "USA"
                        },
                        new Movie
                        {
                            Title = "Rio Bravo",
                            ReleaseDate = DateTime.Parse("1959-4-15"),
                            Genre = "Western",
                            Price = 3.99M,
                            Rating = "NA",
                            Director = "Howard Hawks",
                            Cast = "John Wayne, Dean Martin",
                            IMDbRating = 8.0M,
                            BoxOfficeRevenue = 12300000,
                            ReleaseCountry = "USA"
                        }
                    );

                    context.SaveChanges();
                }

                // ========================
                // Seed 100 fake Actors if none exist
                // ========================
                if (!context.Actor.Any())
                {
                    var faker = new Faker<Actor>()
                        .RuleFor(a => a.Name, f => f.Name.FullName())
                        .RuleFor(a => a.DateOfBirth, f => f.Date.Past(60, DateTime.Today.AddYears(-18)));

                    var fakeActors = faker.Generate(100);

                    context.Actor.AddRange(fakeActors);
                    context.SaveChanges();
                }
            }
        }
    }
}
