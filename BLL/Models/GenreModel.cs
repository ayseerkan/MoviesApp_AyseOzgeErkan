using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DAL;

namespace BLL.Models
{
    public class GenreModel
    {
        public Genre Record { get; set; }

        public string Name => Record.Name;

        //[DisplayName("Movie Count")]
        //public string MovieCount => Record.MovieGenres?.Count.ToString();

        //public string Products => string.Join("<br>", Record.MovieGenres?.Select(mg => mg.Movie?.Name));

    }
}
