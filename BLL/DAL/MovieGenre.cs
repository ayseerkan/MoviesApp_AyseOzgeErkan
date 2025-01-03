using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DAL
{
    public class MovieGenre
    {
        public int Id { get; set; }

        // Foreign Keys
        public int MovieId { get; set; }
        public int GenreId { get; set; }

        // Navigation Properties
        public Movie Movie { get; set; }
        public Genre Genre { get; set; }
    }
}
