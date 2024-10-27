using DDDNetCore.DTOs.OperationType;
using DDDNetCore.DTOs.Patient;
using DDDSample1.Domain.OperationTypes;
using DDDSample1.Domain.Patients;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
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
        public async Task<ActionResult<IEnumerable<OperationTypeDto>>> GetAll()
        {
            return await operationTypeService.GetAllAsync();
        }

        // GET: api/OperationType/x
        [HttpGet("{id}")]
        public async Task<ActionResult<OperationTypeDto>> GetById(Guid id)
        {
            var operationType = await operationTypeService.GetByIdAsync(new OperationTypeID(id));

            if (operationType == null)
            {
                return NotFound();
            }

            return operationType;
        }






    }
}