using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Movies
{
    public class CreateModel : PageModel
    {
        private readonly RazorPagesMovieContext _context;

        public CreateModel(RazorPagesMovieContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Movie Movie { get; set; } = default!;

        [BindProperty]
        public List<int> SelectedActors { get; set; } = new();

        public List<AssignedActorData> AssignedActorDataList { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadActorsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadActorsAsync();
                return Page();
            }

            // Create empty list for relationships
            Movie.MovieActors = new List<MovieActor>();

            // Add selected actor relationships
            foreach (var actorId in SelectedActors)
            {
                Movie.MovieActors.Add(new MovieActor
                {
                    ActorId = actorId
                });
            }

            _context.Movie.Add(Movie);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private async Task LoadActorsAsync()
        {
            var allActors = await _context.Actor.ToListAsync();
            AssignedActorDataList = allActors.Select(actor => new AssignedActorData
            {
                ActorId = actor.ID,
                Name = actor.Name,
                Assigned = SelectedActors.Contains(actor.ID)
            }).ToList();
        }
    }
}
