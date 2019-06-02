using CinemaApp.Core;
using CinemaApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using CinemaApp.Service.Abstract;
using DataTables.AspNet.Core;
using DataTables.AspNet.AspNetCore;

namespace CinemaApp.Service.Concrete
{
    public class ActorService : IActorService
    {
        IUnitOfWork uow;

        public ActorService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public bool Save(Actor obj, ref string message)
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

        private bool Add(Actor obj, ref string message)
        {
            bool state = false;

            // Check if there is an existing name
            if (!uow.Actors.IsExists(obj))
            {
                uow.Actors.Add(obj);
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

        private bool Update(long Id, Actor obj)
        {
            bool state = false;

            var objEx = uow.Actors.Get(obj.Id);
            objEx = obj;
            objEx.Id = Id;
            uow.Actors.Update(Id, objEx);
            int result = uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public bool Remove(Actor obj)
        {
            bool state = false;

            uow.Actors.Remove(obj);
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

            var obj = uow.Actors.Get(id);

            uow.Actors.Remove(obj);
            int result = uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public Actor GetById(long Id)
        {
            return uow.Actors.Get(Id);
        }

        public IEnumerable<Actor> GetAll()
        {
            return uow.Actors.GetAll().ToList();
        }

        public DataTablesResponse SearchApi(IDataTablesRequest requestModel)
        {
            IQueryable<Actor> query = uow.Actors.GetAll().AsQueryable();

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
