using CinemaApp.Core.Models;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaApp.Service.Abstract
{
    public interface ICinemaScreenService
    {
        bool Save(CinemaScreen obj, ref string message);
        bool Remove(CinemaScreen obj);
        bool Remove(long id);
        CinemaScreen GetById(long Id);
        IEnumerable<CinemaScreen> GetAll();
        IEnumerable<CinemaScreen> GetByCinemaId(long CinemaId);
        DataTablesResponse SearchApi(IDataTablesRequest requestModel);
    }
}
