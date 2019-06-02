using CinemaApp.Core.Models;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaApp.Service.Abstract
{
    public interface ICinemaOwnerService
    {
        bool Save(CinemaOwner obj, ref string message);
        bool Remove(CinemaOwner obj);
        bool Remove(long id);
        CinemaOwner GetById(long Id);
        IEnumerable<CinemaOwner> GetAll();
        DataTablesResponse SearchApi(IDataTablesRequest requestModel);
    }
}
