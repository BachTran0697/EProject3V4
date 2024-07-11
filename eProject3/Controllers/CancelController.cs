using eProject3.Interfaces;
using eProject3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eProject3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CancelController : ControllerBase
    {
        private ICancelRepo repo;

        public CancelController(ICancelRepo repo)
        {
            this.repo = repo;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var cancelledReservations = await repo.GetCancellations();
                return Ok(cancelledReservations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [HttpPost("cancel/{ticketcode}/{email}/{phone}")]
        public async Task<IActionResult> CancelReservationAsync(string ticketcode, string email = null, string phone = null)
        {
            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(phone))
            {
                return BadRequest(new { message = "Email or phone must be provided." });
            }
            try
            {
                var response = await repo.CancelReservationAsync(ticketcode, email, phone);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
