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
    [Route("api/ShowTime")]
    public class ShowTimeController : Controller
    {
        private readonly IShowTimeService showTimeService;
        private readonly IMapper mapper;

        public ShowTimeController(IShowTimeService ShowTimeService, IMapper mapper)
        {
            this.showTimeService = ShowTimeService;
            this.mapper = mapper;
        }
        // GET: api/ShowTime
        [Route("GetAll")]
        public IActionResult Get()
        {
            var objs = showTimeService.GetAll().ToList();
            var result = mapper.Map<List<ShowTimeResource>>(objs);

            return new ObjectResult(result);
        }

        // GET: api/ShowTime/5
        [Route("GetOne/{id}")]
        public IActionResult Get(long id)
        {
            var model = showTimeService.GetById(id);
            var obj = mapper.Map<ShowTimeResource>(model);
            if(obj == null)
            {
                return NotFound();
            }
            return new ObjectResult(obj);
        }
        
        // POST: api/ShowTime
        [HttpPost]
        public IActionResult Post([FromBody]ShowTimeResource model)
        {
            if( model == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var obj = mapper.Map<ShowTime>(model);
            string message = string.Empty;
            if( showTimeService.Save(obj, ref message))
            {
                return Ok("Success");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = message });
            }
        }

        // PUT: api/ShowTime/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ShowTimeResource model)
        {
            if (model == null || id != model.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var obj = mapper.Map<ShowTime>(model);
            string message = string.Empty;
            if (showTimeService.Save(obj, ref message))
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
            ShowTime ShowTime = new ShowTime();
            ShowTime.Id = id;
            if (showTimeService.Remove(ShowTime))
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
            DataTablesResponse response = showTimeService.SearchApi(requestModel);
            DataTablesResponse responseTransformed = DataTablesResponse.Create(requestModel, response.TotalRecords, response.TotalRecordsFiltered, response.Data);
            return new DataTablesJsonResult(responseTransformed, true);
        }
    }
}
