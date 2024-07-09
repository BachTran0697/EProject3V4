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
    }
}
