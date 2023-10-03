using FilmFinder;
using Microsoft.AspNetCore.Mvc;
using System.IO;

/*
 * \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
 * 
 * Here is the API interface for Movie
 * 
 * \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
 */

namespace FilmFinderTwo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        Manager manager = new Manager();

        private readonly ILogger<MovieController> _logger;

        public MovieController(ILogger<MovieController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "PostMovie")]
        public ActionResult Post(AddMovieDTO movie)
        {
            manager.AddMovie(movie.Title);
            _logger.LogInformation(movie.ToString());
            return Ok();
        }

        [HttpDelete(Name = "DeleteMovie")]
        public ActionResult Delete(DeleteMovieDTO toDelete)
        {
            manager.DeleteMovie(toDelete.id);
            _logger.LogInformation($"Deleted movie with ID: {toDelete}");
            return Ok();
        }

        [HttpPut(Name = "PutMovie")]
        public ActionResult Put(UpdateMovieDTO toUpdate)
        {
            manager.UpdateMovie(toUpdate.id, toUpdate.preId);
            _logger.LogInformation($"Updated movie with title: {toUpdate}");
            return Ok();
        }

        [HttpGet(Name = "GetMovie")]
        public ActionResult<Movie> Get(ReadMovieDTO toRead)
        {
            Movie movie = manager.ReadMovie(toRead.id);
            _logger.LogInformation($"Read movie with id: {toRead}");
            return movie;
        }

        public record AddMovieDTO(string Title);
        public record DeleteMovieDTO(int id);
        public record UpdateMovieDTO(int id, int preId);
        public record ReadMovieDTO(int id);
    }
}