using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Core.Models
{
    public class Cinema
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public string Location { set; get; }
        public long CinemaOwnerId { set; get; }
        public CinemaOwner CinemaOwner { set; get; }
        public ICollection<CinemaScreen> Screens { set; get; }
    }
}
