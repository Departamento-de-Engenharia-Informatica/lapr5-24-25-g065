using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Patients;
using DDDNetCore.DTOs.Patient;

namespace DDDSample1.Controllers
{
    // Specifies that this controller will handle requests sent to "api/patient"
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        // Private field to hold the reference to the PatientService
        private readonly PatientService patientService;

        // Constructor to inject the PatientService dependency
        public PatientController(PatientService service)
        {
            patientService = service;
        }

        // GET: api/Patients
        /// <summary>
        /// Retrieves all patients.
        /// </summary>
        /// <returns>A list of PatientDto objects.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetAll()
        {
            // Call the service method to get all patients and return them with a 200 OK status
            var patients = await patientService.GetAllAsync();
            return Ok(patients);
        }

        // GET: api/Patients/{id}
        /// <summary>
        /// Retrieves a patient by their ID.
        /// </summary>
        /// <param name="id">The ID of the patient to retrieve.</param>
        /// <returns>A PatientDto object representing the patient.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDto>> GetById(Guid id)
        {
            // Use the service to find a patient by ID
            var patient = await patientService.GetByIdAsync(new PatientId(id));
            // Return 404 Not Found if the patient doesn't exist, otherwise return the patient data
            return patient == null ? NotFound() : Ok(patient);
        }

        // POST: api/Patients
        /// <summary>
        /// Creates a new patient.
        /// </summary>
        /// <param name="dto">The data transfer object containing patient details.</param>
        /// <returns>The created PatientDto object with a 201 Created status.</returns>
        [HttpPost]
        public async Task<ActionResult<PatientDto>> Create(CreatePatientDTO dto)
        {
            try
            {
                // Use the service to create a new patient and return the created resource
                var patient = await patientService.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = patient.Id }, patient);
            }
            catch (BusinessRuleValidationException ex)
            {
                // Return 400 Bad Request with an error message if business rules are violated
                return BadRequest(new { Message = ex.Message });
            }
        }

        // PUT: api/Patients/{id}
        /// <summary>
        /// Updates an existing patient's details.
        /// </summary>
        /// <param name="id">The ID of the patient to update.</param>
        /// <param name="dto">The data transfer object containing updated patient details.</param>
        /// <returns>The updated PatientDto object, or 404 if not found.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<PatientDto>> Update(Guid id, UpdatePatientDTO dto)
        {
            // Ensure that the ID in the URL matches the ID in the DTO
            if (id != dto.Id) return BadRequest("Patient ID mismatch.");

            try
            {
                // Attempt to update the patient's information using the service
                var patient = await patientService.UpdateAsync(dto);
                // Return 404 if the patient was not found, otherwise return the updated patient data
                return patient == null ? NotFound() : Ok(patient);
            }
            catch (BusinessRuleValidationException ex)
            {
                // Return 400 Bad Request with an error message if business rules are violated
                return BadRequest(new { Message = ex.Message });
            }
        }

        // DELETE: api/Patients/{id}/hard
        /// <summary>
        /// Permanently deletes a patient by their ID.
        /// </summary>
        /// <param name="id">The ID of the patient to delete.</param>
        /// <returns>The deleted PatientDto object, or 404 if not found.</returns>
        [HttpDelete("{id}/hard")]
        public async Task<ActionResult<PatientDto>> HardDelete(Guid id)
        {
            try
            {
                // Use the service to delete a patient
                var patient = await patientService.DeleteAsync(new PatientId(id));
                // Return 404 if the patient was not found, otherwise return the deleted patient data
                return patient == null ? NotFound() : Ok(patient);
            }
            catch (BusinessRuleValidationException ex)
            {
                // Return 400 Bad Request with an error message if business rules are violated
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
