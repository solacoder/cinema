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
    public class MovieCategoryService : IMovieCategoryService
    {
        IUnitOfWork uow;

        public MovieCategoryService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public bool Save(MovieCategory obj, ref string message)
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

        private bool Add(MovieCategory obj, ref string message)
        {
            bool state = false;

            // Check if there is an existing name
            if (!uow.MovieCategories.IsExists(obj))
            {
                uow.MovieCategories.Add(obj);
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

        private bool Update(long Id, MovieCategory obj)
        {
            bool state = false;

            var objEx = uow.MovieCategories.Get(obj.Id);
            objEx = obj;
            objEx.Id = Id;
            uow.MovieCategories.Update(Id, objEx);
            int result = uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public bool Remove(MovieCategory obj)
        {
            bool state = false;

            uow.MovieCategories.Remove(obj);
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

            var obj = uow.MovieCategories.Get(id);

            uow.MovieCategories.Remove(obj);
            int result = uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public MovieCategory GetById(long Id)
        {
            return uow.MovieCategories.Get(Id);
        }

        public IEnumerable<MovieCategory> GetAll()
        {
            return uow.MovieCategories.GetAll().ToList();
        }

        public DataTablesResponse SearchApi(IDataTablesRequest requestModel)
        {
            IQueryable<MovieCategory> query = uow.MovieCategories.GetAll().AsQueryable();

            var totalCount = query.Count();

            // Apply filters
            if (requestModel.Search.Value != string.Empty)
            {
                var value = string.IsNullOrEmpty(requestModel.Search.Value) ? "" : requestModel.Search.Value.Trim();
                query = query.Where(p => p.Name.Contains(value) ||
                                        p.Description.Contains(value)
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
                                      Description = tr.Description,
                                      Movies = tr.Movies.Count
                                  };

            DataTablesResponse response = DataTablesResponse.Create(requestModel, totalCount, filteredCount, transformedData.ToList());
            return response;
        }

    }
}
