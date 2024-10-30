using DDDNetCore.DTOs.OperationType;
using dddsample1.domain;
using DDDSample1.Domain.OperationType;
using DDDSample1.Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDDSample1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationTypeController : ControllerBase
    {
        private readonly OperationTypeService operationTypeService;

        public OperationTypeController(OperationTypeService operationTypeService)
        {
            this.operationTypeService = operationTypeService;
        }

        // GET: api/OperationType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OperationTypeDTO>>> GetAllAsync()
        {
            var operationTypes = await operationTypeService.GetAllAsync();
            return Ok(operationTypes);
        }

        // GET: api/OperationType/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<OperationTypeDTO>> GetByIdAsync(Guid id)
        {
            var operationType = await operationTypeService.GetByIdAsync(new OperationTypeID(id));

            if (operationType == null)
            {
                return NotFound();
            }

            return operationType;
        }

        // POST: api/OperationType
        [HttpPost]
        public async Task<ActionResult<OperationTypeDTO>> CreateAsync(OperationTypeDTO dto)
        {
            try
            {
                var operationType = await operationTypeService.AddAsync(dto);
                return CreatedAtAction(nameof(GetByIdAsync), new { id = operationType.ID }, operationType);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        // PUT: api/OperationType/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<OperationTypeDTO>> Update(Guid id, OperationTypeDTO dto)
        {
            if (id != dto.ID) return BadRequest("Operation Type ID mismatch.");

            try
            {
                var operationType = await operationTypeService.UpdateAsync(dto);
                return operationType == null ? NotFound() : Ok(operationType);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // DELETE: api/OperationType/{id}/hard
        [HttpDelete("{id}/hard")]
        public async Task<ActionResult<OperationTypeDTO>> HardDelete(Guid id)
        {
            try
            {
                var operationType = await operationTypeService.DeleteAsync(new OperationTypeID(id));
                return operationType == null ? NotFound() : Ok(operationType);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
} 