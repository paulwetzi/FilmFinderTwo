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

        [HttpPost("PostMovie")]
        public ActionResult Post(AddMovieDTO movie)
        {
            manager.AddMovie(movie.Title, movie.imdb_url, movie.storageId);
            _logger.LogInformation(movie.ToString());
            return Ok();
        }

        [HttpDelete("DeleteMovie")]
        public ActionResult Delete(DeleteMovieDTO toDelete)
        {
            manager.DeleteMovie(toDelete.id);
            _logger.LogInformation($"Deleted movie with ID: {toDelete}");
            return Ok();
        }

        [HttpPut("PutMovie")]
        public ActionResult Put(UpdateMovieDTO toUpdate)
        {
            manager.UpdateMovie(toUpdate.id, toUpdate.newTitle, toUpdate.newImdbUrl);
            _logger.LogInformation($"Updated movie with title: {toUpdate}");
            return Ok();
        }

        [HttpGet("GetMovie")]
        public ActionResult<Movie> Get([FromQuery] ReadMovieDTO toRead)
        {
            Movie movie = manager.ReadMovie(toRead.id);
            _logger.LogInformation($"Read movie with id: {toRead.id}");
            return Ok(movie);
        }

        [HttpGet("GetAllMovie")]
        public ActionResult<Movie> get([FromQuery] ReadAllMovieDTO toRead)
        {
            List<Movie> movies = manager.ReadAllMovie(toRead.moviesId);
            _logger.LogInformation($"read movie with id: ");
            return Ok(movies);
        }

        [HttpGet("GetAllMovies")]
        public ActionResult<Movie> get()
        {
            List<Movie> movies = manager.ReadAllMovie();
            _logger.LogInformation($"read all movies there are");
            return Ok(movies);
        }

        [HttpGet("GetAllMovieName")]
        public ActionResult<Movie> get([FromQuery] ReadAllMovieNameDTO toRead)
        {
            List<Movie> movies = manager.ReadAllMovie();
            List<Movie> filteredMovies = manager.FilterMovies(movies, toRead.moviesTitle);
            _logger.LogInformation($"read movie with id: ");
            return Ok(filteredMovies);
        }

        [HttpGet("GetAllMovieNameStorage")]
        public ActionResult<Movie> get([FromQuery] ReadAllMovieNameStorageDTO toRead)
        {
            List<Movie> movies = manager.ReadAllMovie(toRead.moviesId);
            List<Movie> filteredMovies = manager.FilterMovies(movies, toRead.moviesTitle);
            _logger.LogInformation($"read movie with id: ");
            return Ok(filteredMovies);
        }

        public record AddMovieDTO(string Title, string imdb_url, int storageId);
        public record DeleteMovieDTO(int id);
        public record UpdateMovieDTO(int id, string newTitle, string newImdbUrl);
        public record ReadMovieDTO(int id);
        public record ReadAllMovieDTO(int moviesId);
        public record ReadAllMovieNameDTO(string moviesTitle);
        public record ReadAllMovieNameStorageDTO(int moviesId, string moviesTitle);
    }
}