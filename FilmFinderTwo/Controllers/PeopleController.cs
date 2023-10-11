using FilmFinder;
using Microsoft.AspNetCore.Mvc;
using System.IO;

/*
 * \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
 * 
 * Here is the API interface for People
 * 
 * \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
 */

//namespace FilmFinderTwo.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class PeopleController : ControllerBase
//    {
//        Manager manager = new Manager();

//        private readonly ILogger<MovieController> _logger;

//        public PeopleController(ILogger<MovieController> logger)
//        {
//            _logger = logger;
//        }

//        [HttpPost(Name = "PostStorage")]
//        public ActionResult Post(AddStorageDTO storage)
//        {
//            manager.AddStorage(storage.name, storage.description);
//            _logger.LogInformation(storage.ToString());
//            return Ok();
//        }

//        [HttpDelete(Name = "DeleteStorage")]
//        public ActionResult Delete([FromBody] DeleteStorageDTO toDelete)
//        {
//            manager.DeleteStorage(toDelete.id);
//            _logger.LogInformation($"Deleted storage with ID: {toDelete}");
//            return Ok();
//        }

//        [HttpPut(Name = "PutStorage")]
//        public ActionResult Put(UpdateStorageDTO toUpdate)
//        {
//            manager.UpdateStorage(toUpdate.name, toUpdate.preName);
//            _logger.LogInformation($"Updated storage with title: {toUpdate}");
//            return Ok();
//        }

//        [HttpGet(Name = "GetStorage")]
//        public ActionResult<Movie> Get(ReadStorageDTO toRead)
//        {
//            Storage storage = manager.ReadStorage(toRead.idToFind);
//            _logger.LogInformation($"Read storage with id: {toRead}");
//            return Ok(storage);
//        }

//        public record AddStorageDTO(string name, string description);
//        public record DeleteStorageDTO(int id);
//        public record UpdateStorageDTO(string name, string preName);
//        public record ReadStorageDTO(int idToFind);
//    }

//}