using System;
using System.Collections.Generic;
using Car_Service.BLL.DTO;
using System.Web.Http;
using System.Linq;
using Car_Service.BLL.Interfaces;
using System.Collections;
using Car_Service.BLL.Infrastructure;
using System.Threading.Tasks;

namespace Car_Service.Controllers
{
    public class WorkerController : ApiController
    {
        private IWorkerService _workerService;
        public WorkerController(IWorkerService workerService)
        {
            _workerService = workerService;
        }

        public IHttpActionResult Get()
        {
            return Ok(_workerService.GetWorker());
        }

        [Route("api/worker/{workerId}")]
        [HttpGet]
        public IHttpActionResult Get(int workerId)
        {
            return Ok(_workerService.GetWorker().Find(s => s.Id == workerId));
        }

        [Authorize(Roles = "admin")]
        public IHttpActionResult Post([FromBody] WorkerDTO worker)
        {
            try
            {
                if (ModelState.IsValid && worker != null)
                {
                    OperationDetails result = _workerService.AddWorker(worker);
                    if (result.Succedeed)
                        return Ok();
                    else
                        return BadRequest(result.Message);
                }
                else return BadRequest(ModelState.Values.FirstOrDefault().Errors.FirstOrDefault().ErrorMessage);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("api/worker/{workerId}/workTime")]
        [HttpGet]
        public IHttpActionResult GetWorkDate(int workerId)
        {
            try
            {
                var result = _workerService.WorkerTimes(workerId);
                if (result == null)
                    return BadRequest("Рабочий не найден");
                else
                    return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        [Route("api/worker/{workerId}/workTime")]
        [HttpPost]
        public IHttpActionResult SetWorkTime([FromBody] WorkTimeDTO workTime, int workerId)
        {
            try
            {
                if (ModelState.IsValid && workTime != null)
                {
                    workTime.UserId = workerId;
                    OperationDetails result = _workerService.AddWorkTime(workTime);
                    if (result.Succedeed)
                        return Ok();
                    else
                        return BadRequest(result.Message);
                }
                else return BadRequest(ModelState.Values.FirstOrDefault().Errors.FirstOrDefault().ErrorMessage);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize]
        [Route("api/worker/{workerId}/reservationTime")]
        [HttpGet]
        public IHttpActionResult GetReservationDate(int workerId)
        {
            try
            {
                var result = _workerService.ReservationTimes(workerId);
                if (result == null)
                    return BadRequest("Рабочий не найден");
                else
                    return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
