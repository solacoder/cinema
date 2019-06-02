using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaApp.Service.Abstract
{
    public interface ICinemaService
    {
        bool Save(CinemaApp.Core.Models.Cinema obj, ref string message);
        bool Remove(CinemaApp.Core.Models.Cinema obj);
        bool Remove(long id);
        CinemaApp.Core.Models.Cinema GetById(long Id);
        IEnumerable<CinemaApp.Core.Models.Cinema> GetByCinemaOwnerId(long Id);
        IEnumerable<CinemaApp.Core.Models.Cinema> GetAll();
        DataTablesResponse SearchApi(IDataTablesRequest requestModel);
    }
}
