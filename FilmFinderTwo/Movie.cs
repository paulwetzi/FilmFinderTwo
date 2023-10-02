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
        public List<string> Directors { get; set; }
        public List<string> Writers { get; set; }
        public List<string> Stars { get; set; }

        // Constructor
        public Movie(int id, string title, List<string> directors, List<string> writers, List<string> stars)
        {
            Id = id;
            Title = title;
            Directors = directors;
            Writers = writers;
            Stars = stars;
        }

        // Methods
        public void DisplayMovieInfo()
        {
            Console.WriteLine($"Movie ID: {Id}");
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine("Directors:");
            foreach (var director in Directors)
            {
                Console.WriteLine($"- {director}");
            }
            Console.WriteLine("Writers:");
            foreach (var writer in Writers)
            {
                Console.WriteLine($"- {writer}");
            }
            Console.WriteLine("Stars:");
            foreach (var star in Stars)
            {
                Console.WriteLine($"- {star}");
            }
        }
    }
}
