using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Core.Models
{
    public class MovieCategory
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }

        public ICollection<Movie> Movies { set; get; }
    }
}
