using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DDDSample1.Domain.Patients;
using DDDNetCore.DTOs.Patient;
using System.Linq;
using System.Collections.Generic;
using System;
using DDDNetCore.Services;
using Microsoft.AspNetCore.Cors;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigins")]
    public class PatientController : ControllerBase
    {
        private readonly PatientService _patientService;
        
private readonly AuthServicePatient _authServicePatient;

public PatientController(AuthServicePatient authServicePatient, PatientService patientService)
{
    _authServicePatient = authServicePatient;
    _patientService = patientService;
}

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

   [HttpGet]
public async Task<ActionResult> GetAllPatients()
{
    try
    {
        var patients = await _patientService.GetAllAsync();
        if (patients == null || !patients.Any())
        {
            return NotFound(new { Message = "No patients found." });
        }

        return Ok(new { Data = patients });
    }
    catch (Exception ex)
    {
        return StatusCode(500, new { Error = ex.Message });
    }
}
[HttpPost("register")]
public async Task<IActionResult> RegisterPatient()
{
    try
    {
        // Redirect to Google login
        var token = await _authServicePatient.AuthenticateUser();
        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized(new { Message = "Google authentication failed." });
        }

        // Extract user info (e.g., email) from the token
        var userEmail = await _authServicePatient.GetUserEmailFromTokenAsync(token);

        // Generate an email verification token
        var verificationToken = await _authServicePatient.GenerateEmailVerificationTokenAsync(userEmail);

        return Ok(new { Message = "Registration in progress. Please verify your email.", VerificationToken = verificationToken });
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"An error occurred during registration: {ex.Message}");
    }
}
[HttpPost("add")]
        public async Task<IActionResult> RegisterPatient([FromBody] CreatePatientDTO model)
        {
            if (model == null)
            {
                return BadRequest(new { Message = "Patient details are required." });
            }

            try
            {
                // Step 1: Register patient
                var patientDto = await _patientService.AddAsync(model);

                // Step 2: Send email verification (to be handled in the service)
                await _authServicePatient.SendEmailVerificationAsync(model.Email);

                return Ok(new { Message = "Patient registered successfully. Please verify your email." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while registering the patient: {ex.Message}");
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdatePatient(string id, [FromBody] UpdatePatientDTO model)
        {
            if (model == null)
            {
                return BadRequest(new { Message = "Patient update details are required." });
            }

            try
            {
                var updatedPatient = await _patientService.UpdateAsync(new PatientId(id), model);
                return Ok(new { Message = "Patient updated successfully.", Patient = updatedPatient });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the patient: {ex.Message}");
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePatient(string id)
        {
            try
            {
                await _patientService.DeleteAsync(new PatientId(id));
                return Ok(new { Message = "Patient deleted successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the patient: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatient(string id)
        {
            try
            {
                var patient = await _patientService.GetByIdAsync(new PatientId(id));
                if (patient == null)
                {
                    return NotFound(new { Message = "Patient not found." });
                }
                return Ok(patient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the patient: {ex.Message}");
            }
        }

        [HttpGet("byEmail")]
        public async Task<IActionResult> GetPatientByEmail([FromQuery] string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest(new { Message = "Email is required." });
            }

            try
            {
                var patient = await _patientService.GetPatientByEmailAsync(email);
                if (patient == null)
                {
                    return NotFound(new { Message = "Patient not found." });
                }

                return Ok(patient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the patient: {ex.Message}");
            }
        }
    


        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromQuery] string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new { Message = "Token is required for verification." });
            }

            try
            {
                // Step 3: Verify email using token
                var isVerified = await _authServicePatient.VerifyEmailAsync(token);
                if (!isVerified)
                {
                    return BadRequest(new { Message = "Invalid or expired token." });
                }

                // Step 4: After verification, create a new user automatically
                var user = await _authServicePatient.CreateUserFromVerifiedEmailAsync(token);
                if (user == null)
                {
                    return StatusCode(500, new { Message = "Error creating user after email verification." });
                }

                return Ok(new { Message = "Email verified and user created successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred during email verification: {ex.Message}");
            }
        }
    }
}





