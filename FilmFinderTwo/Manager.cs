using FilmFinderTwo;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

/*
 * \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
 * 
 * This is Model-class :/
 * Here is everything to work with the Database (CRUD) 
 * 
 * \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
 */

namespace FilmFinder
{
    internal class Manager
    {
        // Methods to manage movies

        public int AddMovie(string title, string imdbUrl, int storageId)
        {
            int id = 0;
            DBConnection dbCon = DBConnection.Instance();
            if (dbCon.IsConnected())
            {
                string query = "INSERT INTO movie(`title`, `imdb_url`, `storage_id`) VALUES (@title, @imdb_url, @storageId)";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@storageId", storageId);
                cmd.Parameters.AddWithValue("@imdb_url", imdbUrl);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                id = (int)cmd.LastInsertedId;
            }
            return id;
        }


        public void DeleteMovie(int Id)
        {
            DBConnection dbCon = DBConnection.Instance();
            if (dbCon.IsConnected())
            {
                string query = "DELETE FROM movie WHERE id = @Id;";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Prepare();
                try
                {
                    int affected = cmd.ExecuteNonQuery();
                    Console.WriteLine($"Deleted {affected} rows!");
                }
                catch (Exception)
                {
                    throw new Exception("cant do this because Foreign Key is set");
                }
            }
        }
        public void UpdateMovie(int id, string newTitle, string newImdbUrl)
        {
            DBConnection dbCon = DBConnection.Instance();
            if (dbCon.IsConnected())
            {
                string query = "UPDATE movie SET title = @title, imdb_url = @newImdbUrl WHERE id = @id;";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@title", newTitle);
                cmd.Parameters.AddWithValue("@newImdbUrl", newImdbUrl);
                cmd.Prepare();
                int affected = cmd.ExecuteNonQuery();
                Console.WriteLine($"Updated {affected} rows!");
            }
        }
        public Movie ReadMovie(int idToFind)
        {
            Movie movie = new Movie(0, null, 0, null);

            DBConnection dbCon = DBConnection.Instance();
            string query;
            var cmd = new MySqlCommand();
            if (dbCon.IsConnected())
            {
                query = "SELECT id, title, storage_id,imdb_url FROM movie WHERE id = @id;";
                cmd = new MySqlCommand(query, dbCon.Connection);
                cmd.Parameters.AddWithValue("@id", idToFind);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("id");
                        string title = reader.GetString("title");
                        int storage_id = reader.GetInt32("storage_id");
                        string imdb_url = reader.GetString("imdb_url");
                        movie.Id = id;
                        movie.Title = title;
                        movie.Storage_id = storage_id;
                        movie.IMDbUrl = imdb_url;
                    }
                }
            }
            return movie;
        }

        public List<Movie> ReadAllMovie(int idToFind)
        {
            List<Movie> movieList = new List<Movie>();

            DBConnection dbCon = DBConnection.Instance();
            string query;
            var cmd = new MySqlCommand();
            if (dbCon.IsConnected())
            {
                query = "SELECT id, title, imdb_url FROM movie WHERE storage_id = @id;";
                cmd = new MySqlCommand(query, dbCon.Connection);
                cmd.Parameters.AddWithValue ("id", idToFind);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("id");
                        string title = reader.GetString("title");
                        string imdb_url = reader.GetString("imdb_url");
                        Movie movie = new Movie(id, title, 0, imdb_url);
                        movieList.Add(movie);
                    }
                }
            }
            return movieList;
        }
        public List<Movie> ReadAllMovie()
        {
            List<Movie> movieList = new List<Movie>();

            DBConnection dbCon = DBConnection.Instance();
            string query;
            var cmd = new MySqlCommand();
            if (dbCon.IsConnected())
            {
                query = "SELECT id, title, storage_id, imdb_url FROM movie;";
                cmd = new MySqlCommand(query, dbCon.Connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("id");
                        string title = reader.GetString("title");
                        int storage_id = reader.GetInt32("storage_id");
                        string imdb_url = reader.GetString("imdb_url");
                        Movie movie = new Movie(id, title, storage_id, imdb_url);
                        movieList.Add(movie);
                    }
                }
            }
            return movieList;
        }

        public List<Movie> ReadAllMovieName(string titleToFind)
        {
            List<Movie> movieList = new List<Movie>();

            DBConnection dbCon = DBConnection.Instance();
            string query;
            var cmd = new MySqlCommand();
            if (dbCon.IsConnected())
            {
                query = "SELECT id, title, imdb_url FROM movie WHERE title = @title;";
                cmd = new MySqlCommand(query, dbCon.Connection);
                cmd.Parameters.AddWithValue("@title", titleToFind.Trim());

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("id");
                        string title = reader.GetString("title");
                        string imdb_url = reader.GetString("imdb_url");
                        Movie movie = new Movie(id, title, 0, imdb_url);
                        movieList.Add(movie);
                    }
                }
            }
            return movieList;
        }

        public List<Movie> FilterMovies(List<Movie> movies, string title)
        {
            List<Movie> filteredMovies = new List<Movie>();
            foreach (Movie movie in movies)
            {
                if (movie.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
                {
                    filteredMovies.Add(movie);
                }
            }
            return filteredMovies;
        }

        // Methods to manage storage
        public void AddStorage(string name, string description)
        {
            DBConnection dbCon = DBConnection.Instance();
            if (dbCon.IsConnected())
            {
                string query = "INSERT INTO storage(`name`, `description`)VALUES(@name, @description)";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                int id = (int)cmd.LastInsertedId;
            }
        }

        public void DeleteStorage(int Id)
        {
            DBConnection dbCon = DBConnection.Instance();
            if (dbCon.IsConnected())
            {
                string query = "DELETE FROM storage WHERE id = @Id;";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Prepare();
                try
                {
                    int affected = cmd.ExecuteNonQuery();
                    Console.WriteLine($"Deleted {affected} rows!");
                }
                catch (Exception)
                {
                    throw new Exception("cant do this because Foreign Key is set"); // custom Exception
                }
            }
        }
        public void UpdateStorage(string name, string description, int id)
        {
            DBConnection dbCon = DBConnection.Instance();
            if (dbCon.IsConnected())
            {
                string query = $"UPDATE storage SET name = @name, description = @description WHERE id = @id;";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Prepare();
                int affected = cmd.ExecuteNonQuery();
                Console.WriteLine($"Updated {affected} rows!");
            }
        }

        public Storage ReadStorage(int idToFind)
        {
            Storage storage = new Storage(0, null, null);

            DBConnection dbCon = DBConnection.Instance();
            string query;
            var cmd = new MySqlCommand();
            if (dbCon.IsConnected())
            {
                query = "SELECT id, name, description FROM storage WHERE id = @id;";
                cmd = new MySqlCommand(query, dbCon.Connection);
                cmd.Parameters.AddWithValue("@id", idToFind);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("id");
                        string name = reader.GetString("name");
                        string description = reader.GetString("description");
                        storage.Id = id;
                        storage.Name = name;
                        storage.Description = description;
                    }
                }
            }
            return storage;
        }

        public List<Storage> ReadAllStorage()
        {
            List<Storage> storageList = new List<Storage>();

            DBConnection dbCon = DBConnection.Instance();
            string query;
            var cmd = new MySqlCommand();
            if (dbCon.IsConnected())
            {
                query = "SELECT id, name, description FROM storage;";
                cmd = new MySqlCommand(query, dbCon.Connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("id");
                        string name = reader.GetString("name");
                        string description = reader.GetString("description");
                        Storage storage = new Storage(id, name, description);
                        storageList.Add(storage);
                    }
                }
            }
            return storageList;
        }

        public List<Storage> FilterStorages(List<Storage> storages, string name)
        {
            List<Storage> filteredStorages = new List<Storage>();

            foreach (Storage storage in storages)
            {
                if (storage.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                {
                    filteredStorages.Add(storage);
                }
            }
            return filteredStorages;
        }

        // Other methods for managing and coordinating operations

        public void DisplayMovies()
        {

        }

        public void DisplayStorageLocations()
        {

        }
    }
}
