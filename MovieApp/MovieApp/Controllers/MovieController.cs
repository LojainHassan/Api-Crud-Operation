using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Models;
using Microsoft.EntityFrameworkCore;
namespace MovieApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieContext movieContext;

        public MovieController(MovieContext movieContext)
        {
            this.movieContext = movieContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            if (movieContext.Movies == null)
            {
                return BadRequest();
            }
            return await movieContext.Movies.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            if (movieContext.Movies == null)
            {
                return BadRequest();
            }
            return await movieContext.Movies.FindAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> AddMovie(Movie movie)
        {
            if(movie == null)
            {
                return BadRequest();
            }
            movieContext.AddAsync(movie);
            movieContext.SaveChanges();
            return await movieContext.Movies.FindAsync(movie.Id);
        }

        [HttpDelete]
        public async Task  DeleteMovie(int id)
        {
            movieContext.Remove(id);
            movieContext.SaveChanges();
            
        }



    }
}
