﻿namespace TiendaOnline.Web.Helpers
{
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;
    using TiendaOnline.Web.Data.Entities;

    public interface IUserHelper
    {
        Task<User> GetUserAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(User user, string roleName);

        Task<bool> IsUserInRoleAsync(User user, string roleName);
    }

}
