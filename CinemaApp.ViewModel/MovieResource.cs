using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaApp.ViewModel
{
    public class MovieResource
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public int MovieCategoryId { set; get; }
        public DateTime ReleaseDate { set; get; }
        public decimal Duration { set; get; }
        public int? ProducerId { set; get; }
        
    }
}
