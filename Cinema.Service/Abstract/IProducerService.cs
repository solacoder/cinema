using System;
using System.Collections.Generic;
using System.Text;
using CinemaApp.Core.Models;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;

namespace CinemaApp.Service.Abstract
{
    public interface IProducerService
    {
        bool Save(Producer obj, ref string message);
        bool Remove(Producer obj);
        bool Remove(long id);
        Producer GetById(long Id);
        IEnumerable<Producer> GetAll();
        DataTablesResponse SearchApi(IDataTablesRequest requestModel);
    }
}
