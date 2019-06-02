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
    [Route("api/Cinema")]
    public class CinemaController : Controller
    {
        private readonly ICinemaService cinemaService;
        private readonly IMapper mapper;

        public CinemaController(ICinemaService CinemaService, IMapper mapper)
        {
            this.cinemaService = CinemaService;
            this.mapper = mapper;
        }
        // GET: api/Cinema
        [Route("GetAll")]
        public IActionResult Get()
        {
            var objs = cinemaService.GetAll().ToList();
            var result = mapper.Map<List<CinemaResource>>(objs);

            return new ObjectResult(result);
        }

        [Route("GetOne/{id}")]
        public IActionResult Get(long id)
        {
            var model = cinemaService.GetById(id);
            var obj = mapper.Map<CinemaResource>(model);
            if(obj == null)
            {
                return NotFound();
            }
            return new ObjectResult(obj);
        }

        [Route("GetByCinemaOwnerId/{id}")]
        public IActionResult GetByCinemaOwnerId(long id)
        {
            var model = cinemaService.GetByCinemaOwnerId(id);
            var obj = mapper.Map<List<CinemaResource>>(model);
            if (obj == null)
            {
                return NotFound();
            }
            return new ObjectResult(obj);
        }

        // POST: api/Cinema
        [HttpPost]
        public IActionResult Post([FromBody]CinemaResource model)
        {
            if( model == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var obj = mapper.Map<CinemaApp.Core.Models.Cinema>(model);
            string message = string.Empty;
            if( cinemaService.Save(obj, ref message))
            {
                return Ok("Success");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT: api/Cinema/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]CinemaResource model)
        {
            if (model == null || id != model.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var obj = mapper.Map<CinemaApp.Core.Models.Cinema>(model);
            string message = string.Empty;
            if (cinemaService.Save(obj, ref message))
            {
                return Ok();
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
            CinemaApp.Core.Models.Cinema cinema = new CinemaApp.Core.Models.Cinema
            {
                Id = id
            };
            if (cinemaService.Remove(cinema))
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
            DataTablesResponse response = cinemaService.SearchApi(requestModel);
            DataTablesResponse responseTransformed = DataTablesResponse.Create(requestModel, response.TotalRecords, response.TotalRecordsFiltered, response.Data);
            return new DataTablesJsonResult(responseTransformed, true);
        }
    }
}
