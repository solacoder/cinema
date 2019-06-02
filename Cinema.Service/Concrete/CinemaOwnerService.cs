using CinemaApp.Core;
using CinemaApp.Core.Models;
using CinemaApp.Service.Abstract;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CinemaApp.Service.Concrete
{
    public class CinemaOwnerService : ICinemaOwnerService
    {
        IUnitOfWork uow;

        public CinemaOwnerService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public bool Save(CinemaOwner obj, ref string message)
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

        private bool Add(CinemaOwner obj, ref string message)
        {
            bool state = false;

            // Check if there is an existing name
            if (!uow.CinemaOwners.IsExists(obj))
            {
                uow.CinemaOwners.Add(obj);
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

        private bool Update(long Id, CinemaOwner obj)
        {
            bool state = false;

            var objEx = uow.CinemaOwners.Get(obj.Id);
            objEx = obj;
            objEx.Id = Id;
            uow.CinemaOwners.Update(Id, objEx);
            int result = uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public bool Remove(CinemaOwner obj)
        {
            bool state = false;

            uow.CinemaOwners.Remove(obj);
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

            var obj = uow.CinemaOwners.Get(id);

            uow.CinemaOwners.Remove(obj);
            int result = uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public CinemaOwner GetById(long Id)
        {
            return uow.CinemaOwners.Get(Id);
        }

        public IEnumerable<CinemaOwner> GetAll()
        {
            return uow.CinemaOwners.GetAll().ToList();
        }

        public DataTablesResponse SearchApi(IDataTablesRequest requestModel)
        {
            IQueryable<CinemaOwner> query = uow.CinemaOwners.GetAll().AsQueryable();

            var totalCount = query.Count();

            // Apply filters
            if (requestModel.Search.Value != string.Empty)
            {
                var value = string.IsNullOrEmpty(requestModel.Search.Value) ? "" : requestModel.Search.Value.Trim();
                query = query.Where(p => p.Name.Contains(value) ||
                                          p.Email.Contains(value) ||
                                          p.Phone.Contains(value) 
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
                                      Email = tr.Email,
                                      Phone = tr.Phone,
                                      Cinemas = tr.Cinemas.Count()
                                  };

            DataTablesResponse response = DataTablesResponse.Create(requestModel, totalCount, filteredCount, transformedData);
            return response;
        }
    }

}

