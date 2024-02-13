using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
 * 
 * This is the Movie-class
 * 
 * \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
 */

namespace FilmFinder
{
    public class Movie
    {
        // Properties
        public int Id { get; set; }
        public string Title { get; set; }
        public int Storage_id { get; set; }
        public string IMDbUrl { get; set; }

        // Constructor
        public Movie(int id, string title, int storage_id, string imdbUrl)
        {
            Id = id;
            Title = title;
            Storage_id = storage_id;
            IMDbUrl = imdbUrl;
        }

        // Methods
        public void DisplayMovieInfo()
        {
            Console.WriteLine($"Movie ID: {Id}");
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Storage_id: {Storage_id}");
            Console.WriteLine($"IMDb URL: {IMDbUrl}");
        }
    }
}


