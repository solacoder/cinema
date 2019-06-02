using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Core.Models
{
    public class CinemaOwner
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public string Email { set; get; }
        public string Address { set; get; }
        public string Phone { set; get; }
        public ICollection<Cinema> Cinemas { set; get; }
    }
}
