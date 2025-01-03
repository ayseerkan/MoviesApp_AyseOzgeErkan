using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DAL
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(70)]
        public string Name { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public decimal? TotalRevenue { get; set; }

        // Foreign Key
        public int DirectorId { get; set; }
        // Navigation Property
        public Director Director { get; set; }

        // Navigation Property for Many-to-Many with Genre
        public List<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
    }
}
