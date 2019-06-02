using CinemaApp.Service.Abstract;
using CinemaApp.Service.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApp.Api.Configuration
{
    public class ServiceConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped<IActorService, ActorService>();
            services.AddScoped<ICinemaService, CinemaService>();
            services.AddScoped<ICinemaOwnerService, CinemaOwnerService>();
            services.AddScoped<ICinemaScreenService, CinemaScreenService>();
            services.AddScoped<IMovieCategoryService, MovieCategoryService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IProducerService, ProducerService>();
            services.AddScoped<IShowTimeService, ShowTimeService>();
            services.AddScoped<IDashBoardService, DashboardService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
