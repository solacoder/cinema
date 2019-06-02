using CinemaApp.Core;
using CinemaApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using CinemaApp.Service.Abstract;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;

namespace CinemaApp.Service.Concrete
{
    public class ShowTimeService: IShowTimeService
    {
        IUnitOfWork uow;

        public ShowTimeService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public bool Save(ShowTime obj, ref string message)
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

        private bool Add(ShowTime obj, ref string message)
        {
            bool state = false;

            // Check if there is an existing name
            if (!uow.ShowTimes.IsExists(obj))
            {
                uow.ShowTimes.Add(obj);
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

        private bool Update(long Id, ShowTime obj)
        {
            bool state = false;

            var objEx = uow.ShowTimes.Get(obj.Id);
            objEx = obj;
            objEx.Id = Id;
            uow.ShowTimes.Update(Id, objEx);
            int result = uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public bool Remove(ShowTime obj)
        {
            bool state = false;

            uow.ShowTimes.Remove(obj);
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

            var obj = uow.ShowTimes.Get(id);

            uow.ShowTimes.Remove(obj);
            int result = uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public ShowTime GetById(long Id)
        {
            return uow.ShowTimes.Get(Id);
        }

        public IEnumerable<ShowTime> GetAll()
        {
            return uow.ShowTimes.GetAll().ToList();
        }

        public DataTablesResponse SearchApi(IDataTablesRequest requestModel)
        {
            IQueryable<ShowTime> query = uow.ShowTimes.GetAll().AsQueryable();

            var totalCount = query.Count();

            // Apply filters
            if (requestModel.Search.Value != string.Empty)
            {
                var value = string.IsNullOrEmpty(requestModel.Search.Value) ? "" : requestModel.Search.Value.Trim();
                query = query.Where(p => p.Week.Contains(value) ||
                                          p.Cinema.Name.Contains(value) ||
                                          p.Movie.Name.Contains(value) ||
                                          p.CinemaScreen.Name.Contains(value)
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
                                      Week = tr.Week,
                                      Cinema = tr.Cinema.Name,
                                      Screen = tr.CinemaScreen.Name,
                                      Movie = tr.Movie.Name,
                                      MovieCategory = tr.Movie.MovieCategory.Name,
                                      ShowDate = tr.ShowDate.ToShortDateString(),
                                      ShowTime = tr.Time.ToShortTimeString()
                                  };

            DataTablesResponse response = DataTablesResponse.Create(requestModel, totalCount, filteredCount, transformedData);
            return response;
        }
    }
}
