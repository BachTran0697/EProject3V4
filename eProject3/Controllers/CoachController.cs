using eProject3.Interfaces;
using eProject3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eProject3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoachController : ControllerBase
    {
        private ICoachRepo repo;

        public CoachController(ICoachRepo repo)
        {
            this.repo = repo;
        }
        [HttpGet("train/{trainid}")]
        public async Task<ActionResult<IEnumerable<Coach>>> GetCoachesByTrainId(int trainId)
        {
            try
            {
                var coaches = await repo.GetCoachesByTrainId(trainId);
                if (coaches == null || !coaches.Any())
                {
                    return NotFound("No coaches found for the given train ID");
                }

                return Ok(coaches);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await repo.GetCoachs());
        }

        [HttpPost]
        public async Task<ActionResult> Create(Coach coach)
        {
            try
            {
                var result = await repo.CreateCoach(coach);
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
                var result = await repo.DeleteCoach(id);
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

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Coach coach)
        {
            if (id != coach.Id)
            {
                return BadRequest("Coach ID mismatch");
            }

            try
            {
                var existingCoach = await repo.GetCoachById(id);
                if (existingCoach == null)
                {
                    return NotFound("Coach not found");
                }

                existingCoach.TrainId = coach.TrainId;
                existingCoach.CoachNumber = coach.CoachNumber;
                existingCoach.ClassType = coach.ClassType;
                existingCoach.SeatsNumber = coach.SeatsNumber;
                existingCoach.Seats_vacant = coach.Seats_vacant;
                existingCoach.Seats_reserved = coach.Seats_reserved;

                var updatedCoach = await repo.UpdateCoach(existingCoach);

                return Ok(updatedCoach);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
