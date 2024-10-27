using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DDDSample1.Domain.Specializations; // Ensure correct namespace for Specialization and DTOs
using DDDSample1.Domain.Shared; // Ensure the DTO namespace is correct

namespace DDDSample1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationController : ControllerBase
    {
        private readonly SpecializationService _service;

        public SpecializationController(SpecializationService service)
        {
            _service = service;
        }

        // GET: api/Specialization
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpecializationDto>>> GetSpecialization()
        {
            var specializations = await _service.GetAllAsync();
            return Ok(specializations); // Return the list of users
        }

        /*// GET: api/Specialization/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SpecializationDto>> GetSpecialization(Guid id)
        {
            var specialization = await _service.GetByIdAsync(new SpecializationId(id)); // Get user by ID

            if (specialization == null)
            {
                return NotFound();
            }

            return Ok(specialization); // Return the found user
        }*/

       /* // PUT: api/Specialization/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpecialization(Guid id, SpecializationDto dto)
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
        }*/

        // POST: api/Specialization
        [HttpPost]
        public async Task<ActionResult<SpecializationDto>> PostSpecialization(CreateSpecializationDto dto) // Changed to CreateUserDto
        {
            try
            {
                var specialization = await _service.AddAsync(dto);
                return CreatedAtAction(nameof(GetSpecialization), new { id = specialization.Id }, specialization); // Return created specialization
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

       /* // DELETE: api/Specialization/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecialization(Guid id)
        {
            var deletedSpecialization = await _service.DeleteAsync(new SpecializationId(id)); // Delete Specialization by ID

            if (deletedSpecialization == null)
            {
                return NotFound();
            }

            return NoContent(); // Return no content on successful deletion
        }
        */
    }
}
