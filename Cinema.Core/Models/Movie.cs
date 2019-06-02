using System;

namespace CinemaApp.Core.Models
{
    public class Movie
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public long MovieCategoryId {set; get;}
        public MovieCategory MovieCategory { set; get; }
        public DateTime ReleaseDate { set; get; }
        public decimal Duration { set; get; }
        public long? ProducerId { set; get; }
        public Producer Producer { set; get; }
    }
}