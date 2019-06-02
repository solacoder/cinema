using CinemaApp.Core.Models;
using CinemaApp.Service.Abstract;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Service.Concrete
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> ChangePasswordAsync(string UserId, string CurrentPassword, string NewPassword)
        {
            AppUser user = await _userManager.FindByIdAsync(UserId);
            IdentityResult result = await _userManager.ChangePasswordAsync(user, CurrentPassword, NewPassword);
            if (result.Succeeded)
                return true;
            else
                return false;
        }

        public async Task<Object> GetAppUserAsync(string UserId)
        {
            AppUser user = await _userManager.FindByIdAsync(UserId);
            var obj = new
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Id = user.Id,
                UserName = user.UserName
            };
            return obj;
        }

        public DataTablesResponse SearchApi(IDataTablesRequest requestModel)
        {
            IQueryable<AppUser> query = _userManager.Users;

            var totalCount = query.Count();

            // Apply filters
            if (requestModel.Search.Value != string.Empty)
            {
                var value = string.IsNullOrEmpty(requestModel.Search.Value) ? "" : requestModel.Search.Value.Trim();
                query = query.Where(p => p.Email.Contains(value) ||
                                          p.UserName.Contains(value) ||
                                          p.FirstName.Contains(value) ||
                                          p.LastName.Contains(value)
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
                                      UserName = tr.UserName,
                                      FirstName = tr.FirstName,
                                      LastName = tr.LastName,
                                      Email = tr.Email,
                                      Status = true
                                  };

            DataTablesResponse response = DataTablesResponse.Create(requestModel, totalCount, filteredCount, transformedData);
            return response;
        }

        public async Task<bool> UpdateAppUserAsync(AppUser User)
        {
            AppUser currentUser = await _userManager.FindByIdAsync(User.Id);
            currentUser.Email = User.Email;
            currentUser.FirstName = User.FirstName;
            currentUser.LastName = User.LastName;
            currentUser.UserName = User.UserName;
            IdentityResult result = await _userManager.UpdateAsync(currentUser);
            if (result.Succeeded)
                return true;
            else
                return false;
        }
    }
}
