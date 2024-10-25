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
            return await _service.GetAllAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StaffDto>> GetGetById(Guid id)
        {
            var prod = await _service.GetByIdAsync(new StaffId(id));

            if (prod == null)
            {
                return NotFound();
            }

            return prod;
        }

        // POST: api/Staffs
        [HttpPost]
        public async Task<ActionResult<StaffDto>> Create(CreatingStaffDto dto)
        {
            try
            {
                var staff = await _service.AddAsync(dto);

                return CreatedAtAction(nameof(GetGetById), new { id = staff.Id }, staff);
            }
            catch(BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }

        
        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<ActionResult<StaffDto>> Update(Guid id, StaffDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            try
            {
                var prod = await _service.UpdateAsync(dto);
                
                if (prod == null)
                {
                    return NotFound();
                }
                return Ok(prod);
            }
            catch(BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }
        
        // DELETE: api/Products/5
        [HttpDelete("{id}/hard")]
        public async Task<ActionResult<StaffDto>> HardDelete(Guid id)
        {
            try
            {
                var prod = await _service.DeleteAsync(new StaffId(id));

                if (prod == null)
                {
                    return NotFound();
                }

                return Ok(prod);
            }
            catch(BusinessRuleValidationException ex)
            {
               return BadRequest(new {Message = ex.Message});
            }
        }
    }
}