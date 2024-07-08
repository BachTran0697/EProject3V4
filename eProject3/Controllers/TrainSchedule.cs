using eProject3.Interfaces;
using eProject3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eProject3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainSchedule : ControllerBase
    {
        private ITrainScheduleRepo repo;

        public TrainSchedule(ITrainScheduleRepo repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await repo.GetSchedule());
        }

        [HttpPost]
        public async Task<ActionResult> Create(Train_Schedule Schedule)
        {
            try
            {
                var result = await repo.CreateSchedule(Schedule);
                if (result == null)
                {
                    return BadRequest("Cannot create");
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await repo.DeleteSchedule(id);
                if (result == null)
                {
                    return BadRequest("Cannot delete");
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("update")]
        public async Task<ActionResult> Update(Train_Schedule TrainSchedule)
        {
            try
            {
                var result = await repo.UpdateSchedule(TrainSchedule);
                if (result == null)
                {
                    return BadRequest("Cannot update");
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{fromStation}/{toStation}/{travelTime}")]
        public async Task<ActionResult<List<Train_Schedule>>> Booking(int fromStation, int toStation, DateTime travelTime)
        {
            try
            {
                var schedules = await repo.Booking(fromStation, toStation, travelTime);

                if (schedules == null || !schedules.Any())
                {
                    return NotFound("No trains available for the selected Station and time.");
                }

                return Ok(schedules);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request: " + ex.Message);
            }
        }

        [HttpGet("DayMaster")]
        public async Task<ActionResult> DayMaster()
        {
            return Ok(await repo.DayMaster());
        }
    }
}
