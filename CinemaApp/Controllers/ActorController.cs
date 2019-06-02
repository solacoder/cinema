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
using Newtonsoft.Json;

namespace CinemaApp.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Actor")]
    public class ActorController : Controller
    {
        private readonly IActorService actorService;
        private readonly IMapper mapper;

        ActorController(IActorService actorService, IMapper mapper)
        {
            this.actorService = actorService;
            this.mapper = mapper;
        }
        // GET: api/Actor
        [HttpGet]
        public IActionResult Get()
        {
            var objs = actorService.GetAll().ToList();
            var result = mapper.Map<List<ActorResource>>(objs);

            return new ObjectResult(result);
        }

        // GET: api/Actor/5
        [HttpGet("{id}", Name = "GetActor")]
        public IActionResult Get(long id)
        {
            var model = actorService.GetById(id);
            var obj = mapper.Map<ActorResource>(model);
            if(obj == null)
            {
                return NotFound();
            }
            return new ObjectResult(obj);
        }
        
        // POST: api/Actor
        [HttpPost]
        public IActionResult Post(ActorResource model)
        {
            IActionResult objRst = null;
            if( model == null)
            {
                objRst = BadRequest();
            }
            if (!ModelState.IsValid)
            {
                objRst = BadRequest(ModelState);
            }

            var obj = mapper.Map<Actor>(model);
            string message = string.Empty;
            if( actorService.Save(obj, ref message))
            {
                objRst = Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return objRst;
        }

        // PUT: api/Actor/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ActorResource model)
        {
            IActionResult objRst = null;
            if (model == null || id != model.Id)
            {
                objRst = BadRequest();
            }
            if (!ModelState.IsValid)
            {
                objRst = BadRequest(ModelState);
            }

            var obj = mapper.Map<Actor>(model);
            string message = string.Empty;
            if (actorService.Save(obj, ref message))
            {
                objRst = Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return objRst;
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            Actor actor = new Actor();
            actor.Id = id;
            if (actorService.Remove(actor))
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
            DataTablesResponse response = actorService.SearchApi(requestModel);
            DataTablesResponse responseTransformed = DataTablesResponse.Create(requestModel, response.TotalRecords, response.TotalRecordsFiltered, response.Data);
            return new DataTablesJsonResult(responseTransformed, true);
        }
    }
}
