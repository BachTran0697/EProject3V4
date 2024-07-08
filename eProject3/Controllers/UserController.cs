using eProject3.Interface;
using eProject3.Models;
using eProject3.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace eProject3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepo repo;

        public UserController(IUserRepo repo)
        {
            this.repo = repo;
        }
        [HttpGet]

        public async Task<ActionResult> GetAll()
        {
            return Ok(await repo.GetUsers());
        }
        [HttpGet("{identifier}")]

        public async Task<ActionResult> GetUserByIdentifier(string identifier)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(identifier))
                {
                    return BadRequest("Identifier cannot be empty.");
                }

                var user = await repo.GetUser(identifier);

                if (user == null)
                {
                    return NotFound("User not found.");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]

        public async Task<ActionResult> Create(User user)
        {
            try
            {
                var result = await repo.CreateUser(user);
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
                var result = await repo.DeleteUser(id);
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

        public async Task<ActionResult> UpdateUser(User user)
        {
            try
            {
                var result = await repo.UpdateUser(user);
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
