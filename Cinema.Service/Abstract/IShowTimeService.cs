using CinemaApp.Core.Models;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaApp.Service.Abstract
{
    public interface IShowTimeService
    {
        bool Save(ShowTime obj, ref string message);
        bool Remove(ShowTime obj);
        bool Remove(long id);
        ShowTime GetById(long Id);
        IEnumerable<ShowTime> GetAll();
        DataTablesResponse SearchApi(IDataTablesRequest requestModel);
    }
}
