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
    [Route("api/Producer")]
    public class ProducerController : Controller
    {
        private readonly IProducerService producerService;
        private readonly IMapper mapper;

        public ProducerController(IProducerService ProducerService, IMapper mapper)
        {
            this.producerService = ProducerService;
            this.mapper = mapper;
        }
        // GET: api/Producer
        [HttpGet]
        public IActionResult Get()
        {
            var objs = producerService.GetAll().ToList();
            var result = mapper.Map<List<ProducerResource>>(objs);

            return new ObjectResult(result);
        }

        // GET: api/Producer/5
        [HttpGet("{id}", Name = "GetProducer")]
        public IActionResult Get(long id)
        {
            var model = producerService.GetById(id);
            var obj = mapper.Map<ProducerResource>(model);
            if(obj == null)
            {
                return NotFound();
            }
            return new ObjectResult(obj);
        }
        
        // POST: api/Producer
        [HttpPost]
        public IActionResult Post(ProducerResource model)
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

            var obj = mapper.Map<Producer>(model);
            string message = string.Empty;
            if( producerService.Save(obj, ref message))
            {
                objRst = Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return objRst;
        }

        // PUT: api/Producer/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ProducerResource model)
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

            var obj = mapper.Map<Producer>(model);
            string message = string.Empty;
            if (producerService.Save(obj, ref message))
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
            Producer Producer = new Producer();
            Producer.Id = id;
            if (producerService.Remove(Producer))
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
            DataTablesResponse response = producerService.SearchApi(requestModel);
            DataTablesResponse responseTransformed = DataTablesResponse.Create(requestModel, response.TotalRecords, response.TotalRecordsFiltered, response.Data);
            return new DataTablesJsonResult(responseTransformed, true);
        }
    }
}
