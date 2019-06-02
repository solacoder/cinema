using Cinema.Persistence;
using CinemaApp.Core;
using CinemaApp.Core.Repositories;
using CinemaApp.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApp.Api.Configuration
{
    public class RepositoryConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IActorRepository, ActorRepository>();
            services.AddScoped<ICinemaRepository, CinemaRepository>();
            services.AddScoped<ICinemaOwnerRepository, CinemaOwnerRepository>();
            services.AddScoped<ICinemaScreenRepository, CinemaScreenRepository>();
            services.AddScoped<IMovieCategoryRepository, MovieCategoryRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IProducerRepository, ProducerRepository>();
            services.AddScoped<IShowTimeRepository, ShowTimeRepository>();
        }
    }
}
