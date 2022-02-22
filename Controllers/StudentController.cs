using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Assignment_of_SundayTech.Repository;
using Assignment_of_SundayTech.Model;
using Newtonsoft.Json;

namespace Assignment_of_SundayTech.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentInterface _studentInterface;
        private readonly ILogger<StudentController> _log;

        public StudentController(IStudentInterface studentInterface, ILogger<StudentController> log)
        {
            _log = log;
            _studentInterface = studentInterface;
        }

        [HttpPost("SaveStudentDetails")]
        public async Task<IActionResult> SaveStudentDetails([FromBody] Student objStudent)
        {
            try
            {
                _log.LogInformation($"Request send for SaveStudentDetails method with input: {JsonConvert.SerializeObject(objStudent)} at: {DateTime.Now}");
                var result = await _studentInterface.SaveStudentDetails(objStudent);
                _log.LogInformation($"Request completed for SaveStudentDetails method at: {DateTime.Now}");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _log.LogError($"Error in StudentController for SaveStudentDetails method at: {DateTime.Now}, exception: {ex.Message}");
                throw;
            }
        }

        [HttpPut("UpdateStudentDetails/{studentId}")]
        public async Task<IActionResult> UpdateStudentDetails(int studentId, [FromBody] Student objStudent)
        {
            try
            {
                _log.LogInformation($"Request send for UpdateStudentDetails method with input: {JsonConvert.SerializeObject(objStudent)} at: {DateTime.Now}");
                var objOutput = await _studentInterface.UpdateStudentDetails(studentId, objStudent);
                _log.LogInformation($"UpdateStudentDetails method is completed at: {DateTime.Now} with output: {objOutput}");
                return Ok(objOutput);
            }
            catch (Exception ex)
            {
                _log.LogError($"Error in StudentController for UpdateStudentDetails method at: {DateTime.Now}, exception: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetStudentDetailsById/{studentId}")]
        public async Task<Student> GetStudentDetailsById(int studentId)
        {
            try
            {
                _log.LogInformation($"Request send for GetStudentDetailsById method with input studentId: {studentId} at: {DateTime.Now}");
                var objStudentInfo = await _studentInterface.GetStudentDetailsById(studentId);
                _log.LogInformation($"GetStudentDetailsById method is completed at: {DateTime.Now} with output: {JsonConvert.SerializeObject(objStudentInfo)}");
                return objStudentInfo;
            }
            catch (Exception ex)
            {
                _log.LogError($"Error in StudentController for GetStudentDetailsById method at: {DateTime.Now}, exception: {ex.Message}");
                throw;
            }
        }

        [HttpDelete("DeleteStudentDetails/{studentId}")]
        public async Task<IActionResult> DeleteStudentDetails(int studentId)
        {
            try
            {
                _log.LogInformation($"Request send for DeleteStudentDetails method with input studentId: {studentId} at: {DateTime.Now}");
                var objOutput = await _studentInterface.DeleteStudentDetails(studentId);
                _log.LogInformation($"DeleteStudentDetails method is completed at: {DateTime.Now} with output: {objOutput}");
                return Ok(objOutput);
            }
            catch (Exception ex)
            {
                _log.LogError($"Error in StudentController for DeleteStudentDetails method at: {DateTime.Now}, exception: {ex.Message}");
                throw;
            }
        }
    }
}
