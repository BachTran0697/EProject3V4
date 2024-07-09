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
        [HttpPost("cancel")]
        public async Task<IActionResult> CancelReservation([FromBody] CancelRequest request)
        {
            try
            {
                var response = await repo.CancelReservationAsync(request);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("confirm-cancel")]
        public async Task<IActionResult> ConfirmCancel([FromBody] ConfirmCancelRequest request)
        {
            try
            {
                var message = await repo.ConfirmCancelAsync(request);
                return Ok(new { message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
