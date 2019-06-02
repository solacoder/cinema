using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CinemaApp.Core.Models;
using CinemaApp.Persistence.Repositories;
using CinemaApp.Service.Abstract;
using CinemaApp.Core;
using DataTables.AspNet.Core;
using DataTables.AspNet.AspNetCore;

namespace CinemaApp.Service.Concrete
{
    public class ProducerService : IProducerService
    {
        IUnitOfWork uow;

        public ProducerService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public bool Save(Producer obj, ref string message)
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

        private bool Add(Producer obj, ref string message)
        {
            bool state = false;

            // Check if there is an existing name
            if (!uow.Producers.IsExists(obj))
            {
                uow.Producers.Add(obj);
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

        private bool Update(long Id, Producer obj)
        {
            bool state = false;

            var objEx = uow.Producers.Get(obj.Id);
            objEx = obj;
            objEx.Id = Id;
            uow.Producers.Update(Id, objEx);
            int result = uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public bool Remove(Producer obj)
        {
            bool state = false;

            uow.Producers.Remove(obj);
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

            var obj = uow.Producers.Get(id);

            uow.Producers.Remove(obj);
            int result = uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public Producer GetById(long Id)
        {
            return uow.Producers.Get(Id);
        }

        public IEnumerable<Producer> GetAll()
        {
            return uow.Producers.GetAll().ToList();
        }

        public DataTablesResponse SearchApi(IDataTablesRequest requestModel)
        {
            IQueryable<Producer> query = uow.Producers.GetAll().AsQueryable();

            var totalCount = query.Count();

            // Apply filters
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.FirstName.Contains(value) ||
                                        p.LastName.Contains(value) 
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
                                      FirstName = tr.FirstName,
                                      LastName = tr.LastName
                                  };

            DataTablesResponse response = DataTablesResponse.Create(requestModel, totalCount, filteredCount, transformedData);
            return response;
        }
    }
}
