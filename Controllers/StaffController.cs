using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Staffs;
using DDDNetCore.DTOs.Staff;

namespace DDDSample1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly StaffService _service;

        public StaffController(StaffService service)
        {
            _service = service;
        }

        // GET: api/Staff
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffDto>>> GetAll()
        {
            var staffList = await _service.GetAllAsync();
            return Ok(staffList); // Use Ok() to return a 200 status with the result
        }

        // GET: api/Staff/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StaffDto>> GetById(Guid id)
        {
            var staff = await _service.GetByIdAsync(new StaffId(id));

            if (staff == null)
            {
                return NotFound(); // Return 404 if staff not found
            }

            return Ok(staff); // Use Ok() to return a 200 status with the result
        }

        // POST: api/Staff
        [HttpPost]
        public async Task<ActionResult<StaffDto>> Create([FromBody] CreatingStaffDto dto) // Use FromBody to specify where to get the data from
        {
            try
            {
                var staff = await _service.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = staff.Id }, staff); // Use GetById for consistency
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new { Message = ex.Message }); // Return 400 with error message
            }
        }

        // PUT: api/Staff/5
        [HttpPut("{id}")]
        public async Task<ActionResult<StaffDto>> Update(Guid id, [FromBody] StaffDto dto) // Use FromBody to specify where to get the data from
        {
            if (id != dto.Id)
            {
                return BadRequest("ID mismatch."); // Provide more specific error message
            }

            try
            {
                var updatedStaff = await _service.UpdateAsync(dto);

                if (updatedStaff == null)
                {
                    return NotFound(); // Return 404 if staff not found
                }

                return Ok(updatedStaff); // Return updated staff with 200 status
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new { Message = ex.Message }); // Return 400 with error message
            }
        }

        // DELETE: api/Staff/5/hard
        [HttpDelete("{id}/hard")]
        public async Task<ActionResult<StaffDto>> HardDelete(Guid id)
        {
            try
            {
                var deletedStaff = await _service.DeleteAsync(new StaffId(id));

                if (deletedStaff == null)
                {
                    return NotFound(); // Return 404 if staff not found
                }

                return Ok(deletedStaff); // Return deleted staff with 200 status
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new { Message = ex.Message }); // Return 400 with error message
            }
        }
[HttpGet("search")]
        public async Task<ActionResult<IEnumerable<StaffDto>>> SearchStaff(
    [FromQuery] string name = null,
    [FromQuery] string licenseNumber = null,
    [FromQuery] string phoneNumber = null,
    [FromQuery] string email = null, 
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10)
        {
            var staffs = await _service.SearchStaffsAsync(name,licenseNumber, phoneNumber, email,pageNumber, pageSize);
            return Ok(staffs);
        }
    }
}
