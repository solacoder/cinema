using CinemaApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CinemaApp.Core.Models;
using CinemaApp.Service.Abstract;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Service.Concrete
{
    public class CinemaService : ICinemaService
    {
        IUnitOfWork uow;

        public CinemaService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public bool Save(Core.Models.Cinema obj, ref string message)
        {
            if (obj.Id == 0)
            {
                return Add(obj, ref message);
            }
            else
            {
                return Update(obj.Id, obj);
            }
        }

        private bool Add(Core.Models.Cinema obj, ref string message)
        {
            bool state = false;

            // Check if there is an existing name
            if (!uow.Cinemas.IsExists(obj))
            {
                uow.Cinemas.Add(obj);
                int result = uow.Complete();
                if (result > 0)
                {
                    state = true;
                }
            }
            else
            {
                message = "Data Exists!";
            }

            return state;
        }

        private bool Update(long Id, Core.Models.Cinema obj)
        {
            bool state = false;

            var objEx = uow.Cinemas.Get(obj.Id);
            objEx = obj;
            objEx.Id = Id;
            uow.Cinemas.Update(Id, objEx);
            int result = uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public bool Remove(Core.Models.Cinema obj)
        {
            bool state = false;

            uow.Cinemas.Remove(obj);
            int result = uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public bool Remove(long id)
        {
            bool state = false;

            var obj = uow.Cinemas.Get(id);

            uow.Cinemas.Remove(obj);
            int result = uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public Core.Models.Cinema GetById(long Id)
        {
            return uow.Cinemas.Get(Id);
        }

        public IEnumerable<Core.Models.Cinema> GetAll()
        {
            return uow.Cinemas.GetAll().ToList();
        }

        public DataTablesResponse SearchApi(IDataTablesRequest requestModel)
        {
            IQueryable<CinemaApp.Core.Models.Cinema> query = uow.Cinemas.GetAll().AsQueryable();
                                                                

            var totalCount = query.Count();

            // Apply filters
            if (requestModel.Search.Value != string.Empty)
            {
                var value = string.IsNullOrEmpty(requestModel.Search.Value) ? "" : requestModel.Search.Value.Trim();
                query = query.Where(p => p.Name.Contains(value) ||
                                        p.CinemaOwner.Name.Contains(value) ||
                                        p.Location.Contains(value)
                                    );
            }

            var filteredCount = query.Count();

            // Sorting
            var orderColums = requestModel.Columns.Where(x => x.Sort != null);

            //paging
            var data = query.OrderBy(orderColums).Skip(requestModel.Start).Take(requestModel.Length);

            var transformedData = from tr in data
                                  select new
                                  {
                                      Id = tr.Id,
                                      Name = tr.Name,
                                      CinemaOwner = tr.CinemaOwner.Name,
                                      Location = tr.Location,
                                      Screens = tr.Screens.Count
                                  };

            DataTablesResponse response = DataTablesResponse.Create(requestModel, totalCount, filteredCount, transformedData);
            return response;
        }

        public IEnumerable<Core.Models.Cinema> GetByCinemaOwnerId(long Id)
        {
            return uow.Cinemas.GetAll().Where(m => m.CinemaOwnerId == Id);
        }
    }
}
