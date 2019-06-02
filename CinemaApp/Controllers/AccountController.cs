using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CinemaApp.Core.Models;
using CinemaApp.Data;
using AutoMapper;
using CinemaApp.ViewModel;
using CinemaApp.Api.Helpers;
using DataTables.AspNet.Core;
using CinemaApp.Service.Abstract;
using DataTables.AspNet.AspNetCore;

namespace CinemaApp.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly CinemaContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, 
            IMapper mapper, 
            IUserService userService,
            CinemaContext appDbContext)
        {
            _userManager = userManager;
            _mapper = mapper;
            _appDbContext = appDbContext;
            _userService = userService;
        }

        // POST api/accounts
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserResource model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = _mapper.Map<AppUser>(model);

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            //await _appDbContext.Customers.AddAsync(new Customer { IdentityId = userIdentity.Id, Location = model.Location });
            //await _appDbContext.SaveChangesAsync();

            return new OkObjectResult("Account created");
        }

        [Route("GetOne/{userId}")]
        public async Task<IActionResult> GetOne(string userId)
        {
            var user = await _userService.GetAppUserAsync(userId);
            if (user != null)
                return new OkObjectResult(user);
            else
                return new NotFoundObjectResult(user);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]AppUser User)
        {
            var result = await _userService.UpdateAppUserAsync(User);
            if (result)
                return new OkObjectResult("Account Updated");
            else
                return new NotFoundObjectResult(result);
        }

        [HttpPost]
        [Route("passwordreset")]
        public async Task<IActionResult> PasswordReset([FromBody]string UserId, [FromBody]string CurrentPassword, [FromBody] string NewPassword)
        {
            var result = await _userService.ChangePasswordAsync(UserId, CurrentPassword, NewPassword);
            if (result)
                return new OkObjectResult("Password Reset Successful");
            else
                return StatusCode(StatusCodes.Status500InternalServerError);
        }

        public IActionResult Get(IDataTablesRequest requestModel)
        {
            DataTablesResponse response = _userService.SearchApi(requestModel);
            DataTablesResponse responseTransformed = DataTablesResponse.Create(requestModel, response.TotalRecords, response.TotalRecordsFiltered, response.Data);
            return new DataTablesJsonResult(responseTransformed, true);
        }
    }
}