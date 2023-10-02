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
        public ActionResult Post(MovieDTO movie)
        {
            manager.AddMovie(movie.Title);
            _logger.LogInformation(movie.ToString());
            return Ok();
        }

        [HttpDelete(Name = "DeleteMovie")]
        public ActionResult Delete(DeleteDTO toDelete)
        {
            manager.DeleteMovie(toDelete.id);
            _logger.LogInformation($"Deleted movie with ID: {toDelete}");
            return Ok();
        }

        [HttpPut(Name = "PutMovie")]
        public ActionResult Put(UpdateDTO toUpdate)
        {
            manager.UpdateMovie(toUpdate.title, toUpdate.preTitle);
            _logger.LogInformation($"Updated movie with title: {toUpdate}");
            return Ok();
        }

        [HttpGet(Name = "GetMovie")]
        public ActionResult<Movie> Get(ReadDTO toRead)
        {
            Movie movie = manager.ReadMovie(toRead.id);
            _logger.LogInformation($"Read movie with id: {toRead}");
            return movie;
        }

        public record MovieDTO(string Title);
        public record DeleteDTO(int id);
        public record UpdateDTO(string title, string preTitle);
        public record ReadDTO(int id);
    }

}