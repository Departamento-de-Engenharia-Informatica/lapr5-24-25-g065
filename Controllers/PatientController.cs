using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodoApi.Services;
using DDDSample1.Domain.Patients;
using DDDNetCore.DTOs.Patient;
using System.Linq;
using System;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly AuthServicePatient _authServicePatient;
        private readonly PatientService _patientService;

        public PatientController(AuthServicePatient authServicePatient, PatientService patientService)
        {
            _authServicePatient = authServicePatient;
            _patientService = patientService;
        }

        // POST: api/Patients/authenticate
        [HttpPost("authenticate")]
        public async Task<ActionResult<string>> AuthenticateUser()
        {
            var token = await _authServicePatient.AuthenticateUser();
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Authentication failed.");
            }
            return Ok(new { AccessToken = token });
        }

        // POST: api/Patients/register
        [HttpPost("register")]
        public async Task<IActionResult> RegisterPatient([FromBody] CreatePatientDTO model)
        {
            if (model == null)
            {
                return BadRequest("Patient details are required.");
            }

            try
            {
                var patientId = await _patientService.AddAsync(model);
                return Ok(new { Message = "Patient registered successfully.", PatientId = patientId });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch
            {
                return StatusCode(500, "An error occurred while registering the patient.");
            }
        }

        // PUT: api/Patients/update/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdatePatient(string id, [FromBody] UpdatePatientDTO model)
        {
            if (model == null)
            {
                return BadRequest("Patient update details are required.");
            }

            try
            {
                var patientId = new PatientId(id);
                await _patientService.UpdateAsync(patientId, model);
                return Ok(new { Message = "Patient updated successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch
            {
                return StatusCode(500, "An error occurred while updating the patient.");
            }
        }

        // DELETE: api/Patients/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePatient(string id)
        {
            try
            {
                var patientId = new PatientId(id);
                await _patientService.DeleteAsync(patientId);
                return Ok(new { Message = "Patient deleted successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch
            {
                return StatusCode(500, "An error occurred while deleting the patient.");
            }
        }

        // GET: api/Patients/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatient(string id)
        {
            try
            {
                var patientId = new PatientId(id);
                var patient = await _patientService.GetByIdAsync(patientId);
                if (patient == null)
                {
                    return NotFound(new { Message = "Patient not found." });
                }
                return Ok(patient);
            }
            catch
            {
                return StatusCode(500, "An error occurred while retrieving the patient.");
            }
        }

        // GET: api/Patients/byEmail
        [HttpGet("byEmail")]
        public async Task<IActionResult> GetPatientByEmail([FromQuery] string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest(new { Message = "Email is required." });
            }

            try
            {
                var patient = await _patientService.SearchPatientsAsync(
                    name: null,
                    dateOfBirth: null,
                    medicalRecordNumber: null,
                    phoneNumber: null,
                    email: email,
                    pageNumber: 1,
                    pageSize: 1);

                if (patient == null || patient.Count == 0)
                {
                    return NotFound(new { Message = "Patient not found." });
                }

                return Ok(patient.First());
            }
            catch
            {
                return StatusCode(500, "An error occurred while retrieving the patient.");
            }
        }
    }
}
