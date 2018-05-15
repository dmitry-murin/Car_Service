using Car_Service.BLL.DTO;
using Car_Service.BLL.Infrastructure;
using Car_Service.BLL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Car_Service.Controllers
{
    public class AccountController : ApiController
    {
        private IUserService _userService;
        private readonly string _defaultRole = "user";

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Register([FromBody]UserDTO model)
        {
            await SetInitialDataAsync();
            if (model!=null)
            {
                model.Role = _defaultRole;
                OperationDetails operationDetails = await _userService.Create(model);
                if (operationDetails.Succedeed)
                    return Ok();
                else
                    return BadRequest(operationDetails.Message);
            }
            return BadRequest();

        }
        private async Task SetInitialDataAsync()
        {
            await _userService.SetInitialData(new UserDTO
            {
                Email = "ydn@mail.ru",
                UserName = "ydn@mail.ru",
                Password = "12345Qaz",
                Role = "admin",
            }, new List<string> { "user", "admin" });
        }
    }
}
