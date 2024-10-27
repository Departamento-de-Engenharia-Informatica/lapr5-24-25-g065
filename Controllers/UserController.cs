using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DDDSample1.Domain.Users; // Ensure correct namespace for User and DTOs
using DDDSample1.Domain.Shared; // Ensure the DTO namespace is correct

namespace DDDSample1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _service;

        public UsersController(UserService service)
        {
            _service = service;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _service.GetAllAsync();
            return Ok(users); // Return the list of users
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(Guid id)
        {
            var user = await _service.GetByIdAsync(new UserId(id)); // Get user by ID

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user); // Return the found user
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, UserDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            try
            {
                var updatedUser = await _service.UpdateAsync(dto);

                if (updatedUser == null)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<UserDto>> PostUser(CreateUserDto dto) // Changed to CreateUserDto
        {
            try
            {
                var user = await _service.AddAsync(dto);
                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user); // Return created user
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var deletedUser = await _service.DeleteAsync(new UserId(id)); // Delete user by ID

            if (deletedUser == null)
            {
                return NotFound();
            }

            return NoContent(); // Return no content on successful deletion
        }
    }
}
