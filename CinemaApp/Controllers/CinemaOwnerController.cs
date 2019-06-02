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
    [Route("api/CinemaOwner")]
    public class CinemaOwnerController : Controller
    {
        private readonly ICinemaOwnerService cinemaOwnerService;
        private readonly IMapper mapper;

        public CinemaOwnerController(ICinemaOwnerService CinemaOwnerService, IMapper mapper)
        {
            this.cinemaOwnerService = CinemaOwnerService;
            this.mapper = mapper;
        }
        // GET: api/CinemaOwner
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var objs = cinemaOwnerService.GetAll().ToList();
            var result = mapper.Map<List<CinemaOwnerResource>>(objs);

            return new ObjectResult(result);
        }

        [Route("GetOne/{id}")]
        public IActionResult GetOne(long id)
        {
            var model = cinemaOwnerService.GetById(id);
            var obj = mapper.Map<CinemaOwnerResource>(model);
            if(obj == null)
            {
                return NotFound();
            }
            return new ObjectResult(obj);
        }
        
        // POST: api/CinemaOwner
        [HttpPost]
        public IActionResult Post([FromBody]CinemaOwnerResource model)
        {
            if( model == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var obj = mapper.Map<CinemaOwner>(model);
            string message = string.Empty;
            if( cinemaOwnerService.Save(obj, ref message))
            {
                return Ok("Success");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT: api/CinemaOwner/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]CinemaOwnerResource model)
        {
            if (model == null || id != model.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var obj = mapper.Map<CinemaOwner>(model);
            string message = string.Empty;
            if (cinemaOwnerService.Save(obj, ref message))
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
            CinemaOwner CinemaOwner = new CinemaOwner();
            CinemaOwner.Id = id;
            if (cinemaOwnerService.Remove(CinemaOwner))
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
            DataTablesResponse response = cinemaOwnerService.SearchApi(requestModel);
            DataTablesResponse responseTransformed = DataTablesResponse.Create(requestModel, response.TotalRecords, response.TotalRecordsFiltered, response.Data);
            return new DataTablesJsonResult(responseTransformed, true);
        }
    }
}
