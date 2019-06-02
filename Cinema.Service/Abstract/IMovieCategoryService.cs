using CinemaApp.Core.Models;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaApp.Service.Abstract
{
    public interface IMovieCategoryService
    {
        bool Save(MovieCategory obj, ref string message);
        bool Remove(MovieCategory obj);
        bool Remove(long id);
        MovieCategory GetById(long Id);
        IEnumerable<MovieCategory> GetAll();
        DataTablesResponse SearchApi(IDataTablesRequest requestModel);
    }
}
