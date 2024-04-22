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
        public int StorageId { get; set; }
        public string Title { get; set; }
        public string IMDbUrl { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Constructor
        public Movie(int id, int storageId,string title, string imdbUrl, DateTime? createdAt = null, DateTime? updatedAt = null)
        {
            Id = id;
            StorageId = storageId;
            Title = title;
            IMDbUrl = imdbUrl;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        // Methods
        public void DisplayMovieInfo()
        {
            Console.WriteLine($"Movie ID: {Id}");
            Console.WriteLine($"Storage Id: {StorageId}");
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"IMDb URL: {IMDbUrl}");
            Console.WriteLine($"Created at: {CreatedAt}");
            Console.WriteLine($"Updated at: {UpdatedAt}");
        }
    }
}


