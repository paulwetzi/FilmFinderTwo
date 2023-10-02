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
        public int AddMovie(string title)
        {
            int id = 0;
            DBConnection dbCon = DBConnection.Instance();
            if (dbCon.IsConnected())
            {
                string query = "INSERT INTO movie(`title`)VALUES(@title)";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                cmd.Parameters.AddWithValue("@title", title);
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
        public void UpdateMovie(int id ,int preId)
        {
            DBConnection dbCon = DBConnection.Instance();
            if (dbCon.IsConnected())
            {
                string query = $"UPDATE movie SET id = @id WHERE id = @preId;";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@preId", preId);
                cmd.Prepare();
                int affected = cmd.ExecuteNonQuery();
                Console.WriteLine($"Updated {affected} rows!");
            }
        }
        public Movie ReadMovie(int movieId)
        {
            Movie movie = new Movie(0, null, null, null, null);

            DBConnection dbCon = DBConnection.Instance();
            string query;
            var cmd = new MySqlCommand();
            if (dbCon.IsConnected())
            {
                query = "SELECT id, title FROM movie WHERE id = @id;";
                cmd = new MySqlCommand(query, dbCon.Connection);
                cmd.Parameters.AddWithValue("@id", movieId);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("id");
                        string title = reader.GetString("title");
                        movie.Id = id;
                        movie.Title = title;
                    }
                }

                query = "SELECT p.id, p.name, mp.role FROM people p JOIN movie_people mp ON p.id = mp.person_id WHERE mp.movie_id = @movieId;";
                cmd = new MySqlCommand(query, dbCon.Connection);
                cmd.Parameters.AddWithValue("@movieId", movieId);

                movie.Directors = new List<string>();
                movie.Writers = new List<string>();
                movie.Stars = new List<string>();

                using (var reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        string role = reader.GetString("role");
                        string name = reader.GetString("name");

                        if (role == "director")
                        {
                            movie.Directors.Add(name);
                        }
                        else if (role == "writer")
                        {
                            movie.Writers.Add(name);
                        }
                        else if (role == "star")
                        {
                            movie.Stars.Add(name);
                        }
                    }
                }
            }
            return movie;
        }

        // Methods to manage storage
        public void AddStorage(string name, string description)
        {
            DBConnection dbCon = DBConnection.Instance();
            if (dbCon.IsConnected())
            {
                string query = "INSERT INTO storage(`name`, `description`)VALUES(@title, @description)";
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
        public void UpdateStorage(string name, string preName)
        {
            DBConnection dbCon = DBConnection.Instance();
            if (dbCon.IsConnected())
            {
                string query = $"UPDATE storage SET name = @name WHERE name = @preName;";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@preName", preName);
                cmd.Prepare();
                int affected = cmd.ExecuteNonQuery();
                Console.WriteLine($"Updated {affected} rows!");
            }
        }
        public void ReadStorage(string name)
        {
            public Storage ReadMovie(int movieId)
            {
                Storage storage = new Storage(0, null, null);

                DBConnection dbCon = DBConnection.Instance();
                string query;
                var cmd = new MySqlCommand();
                if (dbCon.IsConnected())
                {
                    query = "SELECT id, title FROM movie WHERE id = @id;";
                    cmd = new MySqlCommand(query, dbCon.Connection);
                    cmd.Parameters.AddWithValue("@id", movieId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("id");
                            string title = reader.GetString("title");
                            movie.Id = id;
                            movie.Title = title;
                        }
                    }

                    query = "SELECT p.id, p.name, mp.role FROM people p JOIN movie_people mp ON p.id = mp.person_id WHERE mp.movie_id = @movieId;";
                    cmd = new MySqlCommand(query, dbCon.Connection);
                    cmd.Parameters.AddWithValue("@movieId", movieId);

                    movie.Directors = new List<string>();
                    movie.Writers = new List<string>();
                    movie.Stars = new List<string>();

                    using (var reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            string role = reader.GetString("role");
                            string name = reader.GetString("name");

                            if (role == "director")
                            {
                                movie.Directors.Add(name);
                            }
                            else if (role == "writer")
                            {
                                movie.Writers.Add(name);
                            }
                            else if (role == "star")
                            {
                                movie.Stars.Add(name);
                            }
                        }
                    }
                }
                return movie;
            }
        }

        // need to make for people and movie_people tables
        // Methods to manage people
        public void AddPerson()
        {

        }
        public void DeletePerson()
        {

        }
        public void UpdatePerson()
        {

        }
        public void ReadPerson()
        {

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
