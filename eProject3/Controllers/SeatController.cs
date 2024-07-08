using eProject3.Interfaces;
using eProject3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eProject3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        private ISeatRepo repo;

        public SeatController(ISeatRepo repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await repo.GetSeats());
        }

        [HttpGet("coach/{coachid}")]
        public async Task<ActionResult<IEnumerable<Seat>>> GetSeatsByCoachId(int coachId)
        {
            try
            {
                var seats = await repo.GetSeatsByCoachId(coachId);
                if (seats == null || !seats.Any())
                {
                    return NotFound("No seats found for the given coach ID");
                }

                return Ok(seats);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(Seat Seat)
        {
            try
            {
                var result = await repo.CreateSeat(Seat);
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
                var result = await repo.DeleteSeat(id);
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
        public async Task<ActionResult> Update(Seat Seat)
        {
            try
            {
                var result = await repo.UpdateSeat(Seat);
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
    }
}
