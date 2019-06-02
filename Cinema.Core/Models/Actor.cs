using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Core.Models
{
    public class Actor
    {
        public long Id { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public long MovieId { set; get; }
        public Movie Movie { set; get; }

    }
}
