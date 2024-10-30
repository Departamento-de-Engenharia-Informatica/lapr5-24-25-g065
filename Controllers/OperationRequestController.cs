using DDDNetCore.Domain;
using DDDNetCore.DTOs.OperationRequest;
using DDDNetCore.DTOs.Patient;
using DDDNetCore.Services;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDDSample1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationRequestController : ControllerBase
    {
        private readonly OperationRequestService operationRequestService;

        public OperationRequestController(OperationRequestService service)
        {
            operationRequestService = service;
        }

        // GET: api/operationRequest
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OperationRequestDTO>>> GetAll()
        {
            var operationRequestList = await operationRequestService.GetAllAsync();
            return operationRequestList;
        }

        // GET: api/operationRequest/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<OperationRequestDTO>> GetByIdAsync(Guid id)
        {
            var operationRequest = await operationRequestService.GetByIdAsync(new OperationRequestID(id));

            if (operationRequest == null)
            {
                return NotFound();
            }

            return operationRequest;
        }


        //POST: api/operationRequest
        [HttpPost]
        public async Task<ActionResult<OperationRequestDTO>> AddAsync(OperationRequestDTO operationRequestDTO)
        {
            try
            {
                var opreq = await operationRequestService.AddAsync(operationRequestDTO);
                return CreatedAtAction(nameof(GetByIdAsync), new { id = opreq.ID }, opreq);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }


        // PUT: api/operationRequest/{priority}
        [HttpPut("{id}")]
        public async Task<ActionResult<OperationRequestDTO>> Update(Guid id, OperationRequestDTO operationRequestDTO)
        {
            if (id != operationRequestDTO.ID) return BadRequest("Operation Request priority mismatch");

            try
            {
                var operationRequest = await operationRequestService.UpdateAsync(operationRequestDTO);
                return operationRequestDTO == null ? NotFound() : Ok(operationRequest);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // DELETE: api/operationRequest/{id}/hard
        [HttpDelete("{id}/hard")]
        public async Task<ActionResult<OperationRequestDTO>> DeleteOperationRequestByID(Guid id)
        {

            try
            {
                var operationRequest = await operationRequestService.RemoveAsync(new OperationRequestID(id));
                return operationRequest == null ? NotFound() : Ok(operationRequest);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }


        /*
        // |=============================================|
        // | Following methods regarding Operation Types |
        // |=============================================|

        // GET: api/operation/type
        [HttpGet("type")]

        public async Task<ActionResult<IEnumerable<OperationType>>> GetTypes()
        {
            return await _service.GetAllTypeAsync();
        }


        // GET: api/operation/type/{id}
        [HttpGet("type/{id}")]
        public async Task<ActionResult<OperationType>> GetType(long id)
        {
            var type = await _service.GetTypeByIdAsync(id);

            if (type == null)
            {
                return NotFound();
            }

            return type;
        }
        // GET: api/operation/filter
        [Authorize(Policy ="AdminOnly")]
        [HttpGet("type/filter")]

        public async Task<ActionResult<IEnumerable<OperationTypeGetDTO>>> GetTypeFilter(OperationTypeSearch search)
        {
            var type = await _service.GetAllTypeFilterAsync(search);

            if (type == null)
            {
                return NotFound();
            }

            return type;
        }

        //POST: api/operation/type
        [Authorize(Policy = "AdminOnly")]
        [HttpPost("type")]
        public async Task<ActionResult<OperationTypeDTO>> PostType(OperationTypeDTO typeDTO)
        {

            // The following try/catch clause catches the cases where there is already a type with the same name
            try
            {
                var type = await _service.AddTypeAsync(typeDTO);
                return CreatedAtAction("GetType", new { id = type.Id }, type);
            }
            catch (FormatException)
            {
                return BadRequest("Input duration format must be HH:mm:ss");
            }
            catch (NotFoundResource)
            {
                return BadRequest("Specialized staff id doesn't exist");
            }
            catch (Exception)
            {
                return BadRequest("Operation type name already registered in the data base");
            }


        }

        // PUT: api/Operation/type/activate/{id}

        [HttpPut("type/activate/{id}")]

        public async Task<ActionResult<OperationType>> ActivateType(long id)
        {

            try
            {
                var prod = await _service.ActivateTypeAsync(id);

                if (prod == null)
                {
                    return NotFound();
                }
                return Ok(prod);
            }
            catch (InvalidDataException ex)
            {
                return BadRequest("This type is already active");
            }
        }


        // PUT: api/Operation/type/inactivate/{id}

        [HttpPut("type/deactivate/{id}")]

        public async Task<ActionResult<OperationType>> InactivateType(long id)
        {

            try
            {
                var prod = await _service.InactivateTypeAsync(id);

                if (prod == null)
                {
                    return NotFound();
                }
                return Ok(prod);
            }
            catch (InvalidDataException ex)
            {
                return BadRequest("This type is already inactive");
            }
        }

        // |=============================================|
        // | Following methods regard Operation Requests |
        // |=============================================|


        [Authorize(Policy="DoctorOnly")]
        [HttpGet("request/filter")]

        public async Task<ActionResult<IEnumerable<OperationRequestDTO>>> GetRequestFilter(OperationRequestSearch search)
        {
            var requests = await _service.GetAllRequestFilterAsync(search);

            if (requests == null)
            {
                return NotFound();
            }

            return requests;
        }*/

    }
}
