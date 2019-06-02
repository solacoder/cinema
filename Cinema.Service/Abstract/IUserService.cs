using CinemaApp.Core.Models;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Service.Abstract
{
    public interface IUserService
    {
        Task<Object> GetAppUserAsync(string Id);
        Task<bool> UpdateAppUserAsync(AppUser User);
        Task<bool> ChangePasswordAsync(string UserId, string OldPass, string NewPassword);
        DataTablesResponse SearchApi(IDataTablesRequest RequestModel);
    }
}
