using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CinemaApp.Core.Models;
using CinemaApp.Service.Abstract;
using AutoMapper;
using CinemaApp.ViewModel;
using DataTables.AspNet.Core;
using DataTables.AspNet.AspNetCore;

namespace CinemaApp.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/MovieCategory")]
    public class MovieCategoryController : Controller
    {
        private readonly IMovieCategoryService movieCategoryService;
        private readonly IMapper mapper;

        public MovieCategoryController(IMovieCategoryService MovieCategoryService, IMapper mapper)
        {
            this.movieCategoryService = MovieCategoryService;
            this.mapper = mapper;
        }
        // GET: api/MovieCategory
        [Route("GetAll")]
        public IActionResult Get()
        {
            var objs = movieCategoryService.GetAll().ToList();
            var result = mapper.Map<List<MovieCategoryResource>>(objs);

            return new ObjectResult(result);
        }

        // GET: api/MovieCategory/5
        [Route("GetOne/{id}")]
        public IActionResult Get(long id)
        {
            var model = movieCategoryService.GetById(id);
            var obj = mapper.Map<MovieCategoryResource>(model);
            if(obj == null)
            {
                return NotFound();
            }
            return new ObjectResult(obj);
        }
        
        // POST: api/MovieCategory
        [HttpPost]
        public IActionResult Post([FromBody]MovieCategoryResource model)
        {
            if( model == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var obj = mapper.Map<MovieCategory>(model);
            string message = string.Empty;
            if( movieCategoryService.Save(obj, ref message))
            {
                return Ok("Sucess");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT: api/MovieCategory/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]MovieCategoryResource model)
        {
            if (model == null || id != model.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var obj = mapper.Map<MovieCategory>(model);
            string message = string.Empty;
            if (movieCategoryService.Save(obj, ref message))
            {
                return Ok("Success");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            MovieCategory MovieCategory = new MovieCategory();
            MovieCategory.Id = id;
            if (movieCategoryService.Remove(MovieCategory))
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        public IActionResult Get(IDataTablesRequest requestModel)
        {
            DataTablesResponse response = movieCategoryService.SearchApi(requestModel);
            DataTablesResponse responseTransformed = DataTablesResponse.Create(requestModel, response.TotalRecords, response.TotalRecordsFiltered, response.Data);
            return new DataTablesJsonResult(responseTransformed, true);
        }
    }
}
