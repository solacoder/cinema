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
    [Route("api/CinemaScreen")]
    public class CinemaScreenController : Controller
    {
        private readonly ICinemaScreenService CinemaScreenService;
        private readonly IMapper mapper;

        public CinemaScreenController(ICinemaScreenService CinemaScreenService, IMapper mapper)
        {
            this.CinemaScreenService = CinemaScreenService;
            this.mapper = mapper;
        }
        // GET: api/CinemaScreen
        [Route("GetAll")]
        public IActionResult Get()
        {
            var objs = CinemaScreenService.GetAll().ToList();
            var result = mapper.Map<List<CinemaScreenResource>>(objs);

            return new ObjectResult(result);
        }

        [Route("GetByCinemaId/{CinemaId}")]
        public IActionResult GetByCinemaId(long CinemaId)
        {
            var objs = CinemaScreenService.GetByCinemaId(CinemaId);
            var result = mapper.Map<List<CinemaScreenResource>>(objs);
            return new ObjectResult(result);
        }

        [Route("GetOne/{id}")]
        public IActionResult Get(long id)
        {
            var model = CinemaScreenService.GetById(id);
            var obj = mapper.Map<CinemaScreenResource>(model);
            if(obj == null)
            {
                return NotFound();
            }
            return new ObjectResult(obj);
        }

        // POST: api/CinemaScreen
        [HttpPost]
        public IActionResult Post([FromBody]CinemaScreenResource model)
        {
            if( model == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var obj = mapper.Map<CinemaScreen>(model);
            string message = string.Empty;
            if( CinemaScreenService.Save(obj, ref message))
            {
                return Ok("Success");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT: api/CinemaScreen/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]CinemaScreenResource model)
        {
            if (model == null || id != model.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var obj = mapper.Map<CinemaScreen>(model);
            string message = string.Empty;
            if (CinemaScreenService.Save(obj, ref message))
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
            CinemaScreen CinemaScreen = new CinemaScreen
            {
                Id = id
            };
            if (CinemaScreenService.Remove(CinemaScreen))
            {
                return Ok("Success");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        public IActionResult Get(IDataTablesRequest requestModel)
        {
            DataTablesResponse response = CinemaScreenService.SearchApi(requestModel);
            DataTablesResponse responseTransformed = DataTablesResponse.Create(requestModel, response.TotalRecords, response.TotalRecordsFiltered, response.Data);
            return new DataTablesJsonResult(responseTransformed, true);
        }
    }
}
