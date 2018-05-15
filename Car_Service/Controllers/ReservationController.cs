using Car_Service.BLL.DTO;
using Car_Service.BLL.Interfaces;
using Car_Service.Providers;
using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace Car_Service.Controllers
{
    public class ReservationController : ApiController
    {
        private IReservationService _reservationService;
        private readonly string _rootPath= "D:\\Car_Service\\Car_Service";
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }
        [HttpPost]
        [Route("api/reservation")]
        [Authorize]
        public async Task<IHttpActionResult> Post()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest();
            }
            var provider = new CustomMultipartFileStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);
            var reservation = provider.Reservation;
            Validate<ReservationDTO>(reservation);
            if (ModelState.IsValid && reservation != null)
            {
                var identity = (ClaimsIdentity)User.Identity;
                var userId = identity.Claims.FirstOrDefault(s=>s.Type=="id").Value;
                var result = await _reservationService.Create(reservation, userId, _rootPath);
                if (result.Succedeed)
                    return Ok();
                else return BadRequest(result.Message);
            }
            else return BadRequest(ModelState.Values.FirstOrDefault().Errors.FirstOrDefault().ErrorMessage);
  
        }
        [HttpGet]
        [Route("api/reservation/history")]
        [Authorize]
        public async Task<IHttpActionResult> History()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var curentUser = identity.Claims.FirstOrDefault(s => s.Type == "id").Value;
            return Ok(_reservationService.GetReservationHistory(curentUser));
        }
        [Route("api/reservation")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(/*_reservationService.GetReservationToday()*/);
        }
        [HttpGet]
        [Route("api/reservation")]
        [Authorize]
        public async Task<IHttpActionResult> GetReservation([FromUri] string date)
        {
            return Ok(_reservationService.GetReservation(DateTime.Parse(date).ToUniversalTime().Date));
        }
        [Route("api/reservation/confirm/{guid}")]
        [HttpGet]
        public IHttpActionResult ConfirmReservation(Guid guid)
        {
            var result = _reservationService.Confirm(guid);
            if (result.Succedeed)
                return Ok();
            else
                return BadRequest(result.Message);
        }
    }
}
