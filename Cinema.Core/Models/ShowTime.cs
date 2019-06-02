using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Core.Models
{
    public class ShowTime
    {
        public long Id { set; get; }
        public string Week { set; get; }
        public long CinemaOwnerId { set; get; }
        public CinemaOwner CinemaOwner { set; get; }
        public long CinemaId { set; get; }
        public Cinema Cinema { set; get; }
        public long CinemaScreenId { set; get; }
        public CinemaScreen CinemaScreen { set; get; }
        public long MovieId { set; get; }
        public Movie Movie { set; get; }
        public long MovieCategoryId { set; get; }
        public MovieCategory MovieCategory { set; get; }
        public DateTime ShowDate { set; get; }
        public DateTime Time { set; get; }
        public int NoOfViewers { set; get; }
        public string CreatedBy { set; get; }
    }
}
