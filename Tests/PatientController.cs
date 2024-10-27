/* Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Controllers;
using DDDSample1.Domain.Patients;
using DDDNetCore.DTOs.Patient;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Tests
{
    public class PatientControllerTests
    {
        private readonly Mock<PatientService> _patientServiceMock;
        private readonly PatientController _controller;

        public PatientControllerTests()
        {
            _patientServiceMock = new Mock<PatientService>();
            _controller = new PatientController(_patientServiceMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithPatients()
        {
            // Arrange
            var patients = new List<PatientDto> { new PatientDto { Id = Guid.NewGuid(), UserName = "test" } };
            _patientServiceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(patients);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedPatients = Assert.IsAssignableFrom<IEnumerable<PatientDto>>(okResult.Value);
            Assert.Equal(patients.Count, returnedPatients.Count());
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WithPatient()
        {
            // Arrange
            var patientId = Guid.NewGuid();
            var patient = new PatientDto { Id = patientId, UserName = "test" };
            _patientServiceMock.Setup(s => s.GetByIdAsync(It.IsAny<PatientId>())).ReturnsAsync(patient);

            // Act
            var result = await _controller.GetById(patientId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedPatient = Assert.IsType<PatientDto>(okResult.Value);
            Assert.Equal(patientId, returnedPatient.Id);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtActionResult_WithPatient()
        {
            // Arrange
            var dto = new CreatePatientDTO { UserName = "test" };
            var patient = new PatientDto { Id = Guid.NewGuid(), UserName = "test" };
            _patientServiceMock.Setup(s => s.AddAsync(dto)).ReturnsAsync(patient);

            // Act
            var result = await _controller.Create(dto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedPatient = Assert.IsType<PatientDto>(createdResult.Value);
            Assert.Equal(patient.Id, returnedPatient.Id);
        }

        [Fact]
        public async Task Update_ReturnsOkResult_WithPatient()
        {
            // Arrange
            var patientId = Guid.NewGuid();
            var dto = new UpdatePatientDTO { Id = patientId, UserName = "updated" };
            var patient = new PatientDto { Id = patientId, UserName = "updated" };
            _patientServiceMock.Setup(s => s.UpdateAsync(dto)).ReturnsAsync(patient);

            // Act
            var result = await _controller.Update(patientId, dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedPatient = Assert.IsType<PatientDto>(okResult.Value);
            Assert.Equal(patientId, returnedPatient.Id);
        }

        [Fact]
        public async Task HardDelete_ReturnsOkResult_WithPatient()
        {
            // Arrange
            var patientId = Guid.NewGuid();
            var patient = new PatientDto { Id = patientId, UserName = "deleted" };
            _patientServiceMock.Setup(s => s.DeleteAsync(It.IsAny<PatientId>())).ReturnsAsync(patient);

            // Act
            var result = await _controller.HardDelete(patientId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedPatient = Assert.IsType<PatientDto>(okResult.Value);
            Assert.Equal(patientId, returnedPatient.Id);
        }

        [Fact]
        public async Task SearchPatients_ReturnsOkResult_WithPatients()
        {
            // Arrange
            var patients = new List<PatientDto> { new PatientDto { Id = Guid.NewGuid(), UserName = "test" } };
            _patientServiceMock.Setup(s => s.SearchPatientsAsync(null, null, null, null, 1, 10)).ReturnsAsync(patients);

            // Act
            var result = await _controller.SearchPatients(null, null, null, null, 1, 10);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedPatients = Assert.IsAssignableFrom<IEnumerable<PatientDto>>(okResult.Value);
            Assert.Equal(patients.Count, returnedPatients.Count());
        }
    }
}
*/