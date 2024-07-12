using eProject3.Interfaces;
using eProject3.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace eProject3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservedController : ControllerBase
    {
        private readonly IReservedRepo repo;

        public ReservedController(IReservedRepo repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var reservations = await repo.GetReservations();
            return Ok(reservations);
        }
        [HttpPut("checkpay/{id}")]
        public async Task<IActionResult> CheckPay(int id)
        {
            var reservation = await repo.PayCheck(id);
            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(reservation);
        }
        [HttpPost]
        public async Task<ActionResult> Create(Reservation reservation)
        {
            try
            {
                var result = await repo.CreateReservation(reservation);
                return Ok(result);
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
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("update")]
        public async Task<ActionResult> Update(Reservation reservation)
        {
            try
            {
                var result = await repo.UpdateReservation(reservation);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("user")]
        public async Task<ActionResult> GetByUser([FromQuery] string name, [FromQuery] string phone)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phone))
            {
                return BadRequest("Name and phone are required.");
            }

            var reservations = await repo.GetReservationsByUserAsync(name, phone);

            if (reservations == null || !reservations.Any())
            {
                return NotFound("No reservations found for the given name and phone.");
            }

            return Ok(reservations);
        }
    }
}
