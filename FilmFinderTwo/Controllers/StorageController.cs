using FilmFinder;
using Microsoft.AspNetCore.Mvc;
using System.IO;
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
            manager.AddStorage(storage.name, storage.description);
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
            manager.UpdateStorage(toUpdate.name, toUpdate.preName);
            _logger.LogInformation($"Updated storage with title: {toUpdate}");
            return Ok();
        }

        // \\\\\\\\\\\\\\\\\  Get Methods  \\\\\\\\\\\\\\\\\

        // With Body
        //[HttpGet("GetStorageBody")]
        //public ActionResult<Movie> Get(ReadStorageDTO toRead)
        //{
        //    Storage storage = manager.ReadStorage(toRead.idToFind);
        //    _logger.LogInformation($"Read storage with ID: {toRead}");
        //    return Ok(storage);
        //}

        //with Query
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

        public record AddStorageDTO(string name, string description);
        public record DeleteStorageDTO(int id);
        public record UpdateStorageDTO(string name, string preName);
        public record ReadStorageDTO(int idToFind);
    }
}