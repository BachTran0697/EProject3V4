using eProject3.Interfaces;
using eProject3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eProject3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainScheduleDetail : ControllerBase
    {
        private ITrainScheduleDetaiRepo repo;

        public TrainScheduleDetail(ITrainScheduleDetaiRepo repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await repo.GetScheduleDetail());
        }
    }
}
