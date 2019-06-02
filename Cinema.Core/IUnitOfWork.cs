using CinemaApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IActorRepository Actors { get; }
        ICinemaOwnerRepository CinemaOwners { get; }
        ICinemaRepository Cinemas { get; }
        ICinemaScreenRepository CinemaScreens { get; }
        IMovieCategoryRepository MovieCategories { get; }
        IMovieRepository Movies { get; }
        IProducerRepository Producers { get; }
        IShowTimeRepository ShowTimes { get; }

        int Complete();
    }
}
