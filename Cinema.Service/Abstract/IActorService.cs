using CinemaApp.Core.Models;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaApp.Service.Abstract
{
    public interface IActorService
    {
        bool Save(Actor obj, ref string message);
        bool Remove(Actor obj);
        bool Remove(long id);
        Actor GetById(long Id);
        IEnumerable<Actor> GetAll();
        DataTablesResponse SearchApi(IDataTablesRequest requestModel);
    }
}
