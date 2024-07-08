using eProject3.Interfaces;
using eProject3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eProject3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservedController : ControllerBase
    {
        private IReservedRepo repo;

        public ReservedController(IReservedRepo repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await repo.GetReservations());
        }

        

        [HttpPost]
        public async Task<ActionResult> Create(Reservation reservation)
        {
            try
            {
                var result = await repo.CreateReservation(reservation);
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

        [HttpGet("{id}")]
        public async Task<ActionResult> ChangeStatus(int id)
        {
            try
            {
                var result = await repo.FinishReservation(id);
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
        public async Task<ActionResult> Update(Reservation Reservation)
        {
            try
            {
                var result = await repo.UpdateReservation(Reservation);
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
