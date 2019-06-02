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
    [Route("api/Movie")]
    public class MovieController : Controller
    {
        private readonly IMovieService movieService;
        private readonly IMapper mapper;

        public MovieController(IMovieService MovieService, IMapper mapper)
        {
            this.movieService = MovieService;
            this.mapper = mapper;
        }
        // GET: api/Movie
        [Route("GetAll")]
        public IActionResult Get()
        {
            var objs = movieService.GetAll().ToList();
            var result = mapper.Map<List<MovieResource>>(objs);

            return new ObjectResult(result);
        }

        // GET: api/Movie/5
        [Route("GetOne/{id}")]
        public IActionResult Get(long id)
        {
            var model = movieService.GetById(id);
            var obj = mapper.Map<MovieResource>(model);
            if(obj == null)
            {
                return NotFound();
            }
            return new ObjectResult(obj);
        }
        
        // POST: api/Movie
        [HttpPost]
        public IActionResult Post([FromBody]MovieResource model)
        {
            if( model == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var obj = mapper.Map<Movie>(model);
            string message = string.Empty;
            if( movieService.Save(obj, ref message))
            {
                return Ok("Success");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT: api/Movie/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]MovieResource model)
        {
            if (model == null || id != model.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var obj = mapper.Map<Movie>(model);
            string message = string.Empty;
            if (movieService.Save(obj, ref message))
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
            Movie Movie = new Movie();
            Movie.Id = id;
            if (movieService.Remove(Movie))
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
            DataTablesResponse response = movieService.SearchApi(requestModel);
            DataTablesResponse responseTransformed = DataTablesResponse.Create(requestModel, response.TotalRecords, response.TotalRecordsFiltered, response.Data);
            return new DataTablesJsonResult(responseTransformed, true);
        }
    }
}
