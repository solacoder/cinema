using CinemaApp.Core.Models;
using CinemaApp.Core.Repositories;
using CinemaApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Persistence.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(CinemaContext context)
            : base(context)
        { }

        public CinemaContext CinemaContext
        {
            get { return Context as CinemaContext; }
        }

        public bool IsExists(Movie obj)
        {
            Movie screen = null;
            try
            {
                screen = CinemaContext.Movies.First<Movie>(m => m.Name == obj.Name);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return screen != null ? true : false;
        }

        public override IEnumerable<Movie> GetAll()
        {
            return CinemaContext.Movies.Include(m => m.MovieCategory);
        }
    }
}
