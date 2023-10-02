using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
 * 
 * This is the Storage-class
 * 
 * \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
 */

namespace FilmFinder
{
    internal class Storage
    {
        // Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Constructor
        public Storage(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        // Methods
        public void DisplayStorageInfo()
        {
            Console.WriteLine($"Storage ID: {Id}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Description: {Description}");
        }
    }
}