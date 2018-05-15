﻿using Car_Service.BLL.DTO;
using Car_Service.BLL.Infrastructure;
using Car_Service.BLL.Interfaces;
using Car_Service.DAL.Entities;
using Car_Service.Model.Interfaces;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System;

namespace Car_Service.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
                var result = await Database.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                // добавляем роль
                await Database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                // создаем профиль клиента
                await Database.SaveAsync();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        public async Task<ClaimsIdentity> Authenticate(string Email, string Password)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            ApplicationUser user = await Database.UserManager.FindAsync(Email, Password);
            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null)
            {
                claim = await Database.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ExternalBearer);
                claim.AddClaim(new Claim("id", user.Id));
            }  
            return claim;
        }
        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            /*foreach (string roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await Database.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);*/
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
