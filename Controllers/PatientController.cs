using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Patients;
using DDDNetCore.DTOs.Patient;

namespace DDDSample1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly PatientService patientService;

        public PatientController(PatientService service)
        {
            patientService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetAll()
        {
            var patients = await patientService.GetAllAsync();
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDto>> GetById(Guid id)
        {
            var patient = await patientService.GetByIdAsync(new PatientId(id));
            return patient == null ? NotFound() : Ok(patient);
        }

        [HttpPost]
        public async Task<ActionResult<PatientDto>> Create(CreatePatientDTO dto)
        {
            try
            {
                var patient = await patientService.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = patient.Id }, patient);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PatientDto>> Update(Guid id, UpdatePatientDTO dto)
        {
            if (id != dto.Id) return BadRequest("Patient ID mismatch.");

            try
            {
                var patient = await patientService.UpdateAsync(dto);
                return patient == null ? NotFound() : Ok(patient);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id}/hard")]
        public async Task<ActionResult<PatientDto>> HardDelete(Guid id)
        {
            try
            {
                var patient = await patientService.DeleteAsync(new PatientId(id));
                return patient == null ? NotFound() : Ok(patient);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
