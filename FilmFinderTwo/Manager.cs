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
                string query = "INSERT INTO movies(`title`, `imdb_url`, `storage_id`) VALUES (@title, @imdb_url, @storageId)";
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
                string query = "DELETE FROM movies WHERE id = @Id;";
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
                string query = "UPDATE movies SET title = @title, imdb_url = @newImdbUrl WHERE id = @id;";
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
            Movie movie = null;

            DBConnection dbCon = DBConnection.Instance();
            string query;
            var cmd = new MySqlCommand();
            if (dbCon.IsConnected())
            {
                query = "SELECT id, title, storage_id,imdb_url FROM movies WHERE id = @id;";
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
                        movie.StorageId = storage_id;
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
                query = "SELECT id, title, imdb_url FROM movies WHERE storage_id = @id;";
                cmd = new MySqlCommand(query, dbCon.Connection);
                cmd.Parameters.AddWithValue ("id", idToFind);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("id");
                        string title = reader.GetString("title");
                        string imdb_url = reader.GetString("imdb_url");
                        Movie movie = new Movie(id, 0, title, imdb_url);
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
                query = "SELECT id, title, storage_id, imdb_url FROM movies;";
                cmd = new MySqlCommand(query, dbCon.Connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("id");
                        string title = reader.GetString("title");
                        int storage_id = reader.GetInt32("storage_id");
                        string imdb_url = reader.GetString("imdb_url");
                        Movie movie = new Movie(id, storage_id, title, imdb_url);
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
                query = "SELECT id, title, imdb_url FROM movies WHERE title = @title;";
                cmd = new MySqlCommand(query, dbCon.Connection);
                cmd.Parameters.AddWithValue("@title", titleToFind.Trim());

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("id");
                        string title = reader.GetString("title");
                        string imdb_url = reader.GetString("imdb_url");
                        Movie movie = new Movie(id, 0, title, imdb_url);
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
        public void AddStorage(int userId, string name, string description)
        {
            DBConnection dbCon = DBConnection.Instance();
            if (dbCon.IsConnected())
            {
                string query = "INSERT INTO storages(user_id, name, description) VALUES(@userId, @name, @description)";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                cmd.Parameters.AddWithValue("@userId", userId);
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
                string query = "DELETE FROM storages WHERE id = @Id;";
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
                string query = $"UPDATE storages SET name = @name, description = @description WHERE id = @id;";
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
            Storage storage = null;

            DBConnection dbCon = DBConnection.Instance();
            string query;
            var cmd = new MySqlCommand();
            if (dbCon.IsConnected())
            {
                query = "SELECT id, name, description FROM storages WHERE id = @id;";
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
                query = "SELECT id, name, description FROM storages;";
                cmd = new MySqlCommand(query, dbCon.Connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("id");
                        string name = reader.GetString("name");
                        string description = reader.GetString("description");
                        Storage storage = null;
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

        // Methods to manage user

        public void Register(string email, string name, string password)
        {
            DBConnection dbCon = DBConnection.Instance();
            if (dbCon.IsConnected())
            {
                string query = "INSERT INTO users (email, password, name) VALUES (@email, @password, @name)";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }
        public User Login(string emailToFind, string password)
        {

            User user = null;

            DBConnection dbCon = DBConnection.Instance();
            string query;
            var cmd = new MySqlCommand();
            if (dbCon.IsConnected())
            {
                query = "SELECT id, email, password, created_at, updated_at, name FROM users WHERE email = @email AND password = @password;";
                cmd = new MySqlCommand(query, dbCon.Connection);
                cmd.Parameters.AddWithValue("@email", emailToFind);
                cmd.Parameters.AddWithValue("@password", password);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int id = reader.GetInt32("id");
                        string email = reader.GetString("email");
                        string name = reader.GetString("name"); 
                        //string password = reader.GetString("password");
                        DateTime created_at = reader.GetDateTime("created_at");
                        DateTime updated_at = reader.GetDateTime("updated_at");

                        if (password == reader.GetString("password"))
                        {
                            user = new User(id, email, password, name, created_at, updated_at);
                        }
      
                    }
                }
            }
            return user;
        }
    }
}

