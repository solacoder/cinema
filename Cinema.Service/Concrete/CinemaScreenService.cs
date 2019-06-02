using CinemaApp.Core;
using CinemaApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CinemaApp.Service.Abstract;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;

namespace CinemaApp.Service.Concrete
{
    public class CinemaScreenService : ICinemaScreenService
    {
        IUnitOfWork uow;

        public CinemaScreenService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public bool Save(CinemaScreen obj, ref string message)
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

        private bool Add(CinemaScreen obj, ref string message)
        {
            bool state = false;

            // Check if there is an existing name
            if (!uow.CinemaScreens.IsExists(obj))
            {
                uow.CinemaScreens.Add(obj);
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

        private bool Update(long Id, CinemaScreen obj)
        {
            bool state = false;

            var objEx = uow.CinemaScreens.Get(obj.Id);
            objEx = obj;
            objEx.Id = Id;
            uow.CinemaScreens.Update(Id, objEx);
            int result = uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public bool Remove(CinemaScreen obj)
        {
            bool state = false;

            uow.CinemaScreens.Remove(obj);
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

            var obj = uow.CinemaScreens.Get(id);

            uow.CinemaScreens.Remove(obj);
            int result = uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public CinemaScreen GetById(long Id)
        {
            return uow.CinemaScreens.Get(Id);
        }

        public IEnumerable<CinemaScreen> GetAll()
        {
            return uow.CinemaScreens.GetAll().ToList();
        }

        public IEnumerable<CinemaScreen> GetByCinemaId(long CinemaId)
        {
            return uow.CinemaScreens.GetAll().Where(m => m.CinemaId == CinemaId);
        }

        public DataTablesResponse SearchApi(IDataTablesRequest requestModel)
        {
            IQueryable<CinemaScreen> query = uow.CinemaScreens.GetAll().AsQueryable();

            var totalCount = query.Count();

            // Apply filters
            if (requestModel.Search.Value != string.Empty)
            {
                var value = string.IsNullOrEmpty(requestModel.Search.Value) ? "" : requestModel.Search.Value.Trim();
                query = query.Where(p => p.Cinema.Name.Contains(value) ||
                                          p.CinemaOwner.Name.Contains(value) ||
                                          p.Name.Contains(value) || 
                                          p.Cinema.Location.Contains(value)
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
                                      Cinema = tr.Cinema.Name,
                                      Location = tr.Cinema.Location,
                                      Capacity = tr.NoOfSeats
                                  };

            DataTablesResponse response = DataTablesResponse.Create(requestModel, totalCount, filteredCount, transformedData);
            return response;
        }
    }
}
