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
    public class MovieService : IMovieService
    {
        IUnitOfWork uow;

        public MovieService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public bool Save(Movie obj, ref string message)
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

        private bool Add(Movie obj, ref string message)
        {
            bool state = false;

            // Check if there is an existing name
            if (!uow.Movies.IsExists(obj))
            {
                uow.Movies.Add(obj);
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

        private bool Update(long Id, Movie obj)
        {
            bool state = false;

            var objEx = uow.Movies.Get(obj.Id);
            objEx = obj;
            objEx.Id = Id;
            uow.Movies.Update(Id, objEx);
            int result = uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public bool Remove(Movie obj)
        {
            bool state = false;

            uow.Movies.Remove(obj);
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

            var obj = uow.Movies.Get(id);

            uow.Movies.Remove(obj);
            int result = uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public Movie GetById(long Id)
        {
            return uow.Movies.Get(Id);
        }

        public IEnumerable<Movie> GetAll()
        {
            return uow.Movies.GetAll().ToList();
        }

        public DataTablesResponse SearchApi(IDataTablesRequest requestModel)
        {
            IQueryable<Movie> query = uow.Movies.GetAll().AsQueryable();

            var totalCount = query.Count();

            // Apply filters
            if (requestModel.Search.Value != string.Empty)
            {
                var value = string.IsNullOrEmpty(requestModel.Search.Value) ? "" : requestModel.Search.Value.Trim();
                query = query.Where(p => p.Name.Contains(value) ||
                                        p.MovieCategory.Name.Contains(value) ||
                                        (p.Producer.FirstName + p.Producer.LastName).Contains(value)
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
                                      MovieCategory = tr.MovieCategory.Name,
                                      MovieCategoryId = tr.MovieCategoryId,
                                      //Producer = tr.Producer.FirstName + " " + tr.Producer.LastName,
                                      Duration = tr.Duration,
                                      ReleaseDate = tr.ReleaseDate.ToShortDateString()
                                  };

            DataTablesResponse response = DataTablesResponse.Create(requestModel, totalCount, filteredCount, transformedData);
            return response;
        }
    }
}
