using FilmFinder;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using static FilmFinderTwo.Controllers.MovieController;
using static FilmFinderTwo.Controllers.StorageController;

/*
 * \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
 * 
 * Here is the API interface for Storage
 * 
 * \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
 */

namespace FilmFinderTwo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StorageController : ControllerBase
    {
        Manager manager = new Manager();

        private readonly ILogger<StorageController> _logger;

        public StorageController(ILogger<StorageController> logger)
        {
            _logger = logger;
        }

        [HttpPost("PostStorage")]
        public ActionResult Post(AddStorageDTO storage)
        {
            manager.AddStorage(storage.userId, storage.name, storage.description);
            _logger.LogInformation(storage.ToString());
            return Ok();
        }

        [HttpDelete("DeleteStorage")]
        public ActionResult Delete([FromBody] DeleteStorageDTO toDelete)
        {
            manager.DeleteStorage(toDelete.id);
            _logger.LogInformation($"Deleted storage with ID: {toDelete}");
            return Ok();
        }

        [HttpPut("PutStorage")]
        public ActionResult Put(UpdateStorageDTO toUpdate)
        {
            manager.UpdateStorage(toUpdate.name, toUpdate.description, toUpdate.id);
            _logger.LogInformation($"Updated storage with title: {toUpdate}");
            return Ok();
        }

        // \\\\\\\\\\\\\\\\\  Get Methods  \\\\\\\\\\\\\\\\\

       [HttpGet("GetStorageQuery")]
        public ActionResult<Movie> Get([FromQuery] int idToFind)
        {
            Storage storage = manager.ReadStorage(idToFind);
            _logger.LogInformation($"Read storage with id: {idToFind}");
            return Ok(storage);
        }

        [HttpGet("GetAllStorage")]
        public ActionResult<List<Storage>> GetAllStorage()
        {
            List<Storage> storage = manager.ReadAllStorage();
            _logger.LogInformation($"Read all storages. Count: {storage.Count}");
            return Ok(storage);
        }

        [HttpGet("GetAllStorageName")]
        public ActionResult<Movie> get([FromQuery] ReadAllStorageNameDTO toRead)
        {
            List<Storage> storages = manager.ReadAllStorage();
            List<Storage> filteredMovies = manager.FilterStorages(storages, toRead.name);
            _logger.LogInformation($"read all Storages");
            return Ok(filteredMovies);
        }

        public record AddStorageDTO(int userId, string name, string description);
        public record DeleteStorageDTO(int id);
        public record UpdateStorageDTO(string name, string description, int id);
        public record ReadStorageDTO(int idToFind);
        public record ReadAllStorageNameDTO(string name);
    }
}