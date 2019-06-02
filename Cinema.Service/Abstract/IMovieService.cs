using CinemaApp.Core.Models;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaApp.Service.Abstract
{
    public interface IMovieService
    {
        bool Save(Movie obj, ref string message);
        bool Remove(Movie obj);
        bool Remove(long id);
        Movie GetById(long Id);
        IEnumerable<Movie> GetAll();
        DataTablesResponse SearchApi(IDataTablesRequest requestModel);
    }
}
