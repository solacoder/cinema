using CinemaApp.Core;
using CinemaApp.Service.Abstract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CinemaApp.Service.Concrete
{
    public class DashboardService : IDashBoardService
    {
        IUnitOfWork uow;

        public DashboardService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public Hashtable GetDashBoardData()
        {
            Hashtable table = new Hashtable();
            long cinemaOwnwers = this.uow.CinemaOwners.Count();
            long cinemas = this.uow.Cinemas.Count();
            long screens = this.uow.CinemaScreens.Count();
            long movieCategories = this.uow.MovieCategories.Count();
            long movies = this.uow.Movies.Count();
            long showTimes = this.uow.ShowTimes.Count();

            table.Add("CinemaOwners", cinemaOwnwers);
            table.Add("Cinemas", cinemas);
            table.Add("Screens", screens);
            table.Add("MovieCategories", movieCategories);
            table.Add("Movies", movies);
            table.Add("ShowTimes", showTimes);

            return table;
        }
    }
}
