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
    public class MovieCategoryRepository : Repository<MovieCategory>, IMovieCategoryRepository
    {
        public MovieCategoryRepository(CinemaContext context)
            : base(context)
        { }

        public CinemaContext CinemaContext
        {
            get { return Context as CinemaContext; }
        }

        public bool IsExists(MovieCategory obj)
        {
            MovieCategory screen = null;
            try
            {
                screen = CinemaContext.MovieCategories.First<MovieCategory>(m => m.Name == obj.Name);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return screen != null ? true : false;
        }

        public override IEnumerable<MovieCategory> GetAll()
        {
            return CinemaContext.MovieCategories.Include(m => m.Movies);
        }
    } 
}
