using CinemaApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Core.Repositories
{
    public interface IMovieRepository : IRepository<Movie>
    {
        bool IsExists(Movie obj);
    }
}
