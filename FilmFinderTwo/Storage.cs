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

public class Storage
{
    // Properties
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Constructor
    public Storage(int id, int userId, string name, string description, DateTime createdAt, DateTime updatedAt)
    {
        Id = id;
        UserId = userId;
        Name = name;
        Description = description;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    // Methods
    public void DisplayStorageInfo()
    {
        Console.WriteLine($"Storage ID: {Id}");
        Console.WriteLine($"User ID: {UserId}");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Description: {Description}");
        Console.WriteLine($"Created at: {CreatedAt}");
        Console.WriteLine($"Updated at: {UpdatedAt}");
    }
}
