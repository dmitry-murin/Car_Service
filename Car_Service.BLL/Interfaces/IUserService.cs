using Car_Service.BLL.DTO;
using Car_Service.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Car_Service.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserDTO userDto);
        Task<ClaimsIdentity> Authenticate(string Email, string Password);
        Task SetInitialData(UserDTO userDTO, List<string> list);
    }
}
