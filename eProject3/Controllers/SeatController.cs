using eProject3.Interfaces;
using eProject3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eProject3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        private readonly ISeatRepo repo;

        public SeatController(ISeatRepo repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await repo.GetSeats());
        }

        [HttpGet("coach/{coachId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetSeatsByCoachId(int coachId)
        {
            try
            {
                var seats = await repo.GetSeatsByCoachId(coachId);
                if (seats == null || !seats.Any())
                {
                    return NotFound("No seats found for the given coach ID");
                }

                var seatDetails = seats.Select(s => new
                {
                    s.Id,
                    s.SeatNumber,
                    Status = s.SeatDetails.OrderByDescending(sd => sd.Id).FirstOrDefault()?.Status
                });

                return Ok(seatDetails);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(Seat seat)
        {
            try
            {
                var result = await repo.CreateSeat(seat);
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
        public async Task<ActionResult> Update(Seat seat)
        {
            try
            {
                var result = await repo.UpdateSeat(seat);
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
