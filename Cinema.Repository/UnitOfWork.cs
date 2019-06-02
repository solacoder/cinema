using CinemaApp.Core;
using CinemaApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaApp.Core.Repositories;
using CinemaApp.Persistence.Repositories;

namespace Cinema.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CinemaContext _context;

        public UnitOfWork(CinemaContext context)
        {
            _context = context;

            Actors = new ActorRepository(_context);
            CinemaOwners = new CinemaOwnerRepository(_context);
            Cinemas = new CinemaRepository(_context);
            CinemaScreens = new CinemaScreenRepository(_context);
            MovieCategories = new MovieCategoryRepository(_context);
            Movies = new MovieRepository(_context);
            Producers = new ProducerRepository(_context);
            ShowTimes = new ShowTimeRepository(_context);
        }

        public IActorRepository Actors { get; private set; }
        public ICinemaOwnerRepository CinemaOwners { get; private set; }
        public ICinemaRepository Cinemas { get; private set; }
        public ICinemaScreenRepository CinemaScreens { get; private set; }
        public IMovieCategoryRepository MovieCategories { get; private set; }
        public IMovieRepository Movies { get; private set; }
        public IProducerRepository Producers { get; private set; }
        public IShowTimeRepository ShowTimes { get; private set; }

        public int Complete()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
