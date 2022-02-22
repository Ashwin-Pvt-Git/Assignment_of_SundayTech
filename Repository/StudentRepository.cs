using Assignment_of_SundayTech.Model;
using Dapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Assignment_of_SundayTech.Repository
{
    public class StudentRepository : IStudentInterface
    {
        private readonly IDapperInterface _objDapper;
        private readonly ILogger<IStudentInterface> _log;
        public StudentRepository(IDapperInterface dapper, ILogger<IStudentInterface> log)
        {
            _objDapper = dapper;
            _log = log;
        }
        
        public async Task<int> SaveStudentDetails(Student objStudent)
        {
            try
            {
                _log.LogInformation($"SaveStudentDetails method started with input: {JsonConvert.SerializeObject(objStudent)} at: {DateTime.Now}");
                DynamicParameters objInput = new DynamicParameters();
                objInput.Add("StudentName", objStudent.StudentName);
                objInput.Add("Standard", objStudent.Standard);
                objInput.Add("DateOfBirth", objStudent.DateOfBirth);
                objInput.Add("Gender", objStudent.Gender);
                objInput.Add("MobileNumber", objStudent.MobileNumber);
                objInput.Add("Address", objStudent.Address);
                var objOutput = (int)await _objDapper.Execute<int>("dbo.Db_SP_SaveStudentDetails", objInput, isSingleRecord: true);
                _log.LogInformation($"SaveStudentDetails method is completed at: {DateTime.Now}");
                return objOutput;
            }
            catch (Exception ex)
            {
                _log.LogError($"Error in SaveStudentDetails method at: {DateTime.Now}, exception: {ex.Message}");
                throw;
            }
        }

        public async Task<int> UpdateStudentDetails(int studentId, Student objStudent)
        {
            try
            {
                _log.LogInformation($"UpdateStudentDetails method started with input: {JsonConvert.SerializeObject(objStudent)} at: {DateTime.Now}");
                var objInput = new DynamicParameters();
                objInput.Add("StudentId", studentId);
                objInput.Add("StudentName", objStudent.StudentName);
                objInput.Add("Standard", objStudent.Standard);
                objInput.Add("DateOfBirth", objStudent.DateOfBirth);
                objInput.Add("Gender", objStudent.Gender);
                objInput.Add("MobileNumber", objStudent.MobileNumber);
                objInput.Add("Address", objStudent.Address);
                var objOutput = (int)await _objDapper.Execute<int>("dbo.Db_SP_UpdateStudentDetails", objInput, isSingleRecord: true);
                _log.LogInformation($"UpdateStudentDetails method is completed at: {DateTime.Now}");
                return objOutput;
            }
            catch (Exception ex)
            {
                _log.LogError($"Error in UpdateStudentDetails method at: {DateTime.Now}, exception: {ex.Message}");
                throw;
            }
        }

        public async Task<Student> GetStudentDetailsById(int studentId)
        {
            try
            {
                _log.LogInformation($"GetStudentDetailsById method started with input studentId: {studentId} at: {DateTime.Now}");
                string strQuery = "Select * From Students Where StudentId = " + studentId;
                var objStudentInfo = (Student)await _objDapper.Execute<Student>(strQuery, null, commandType: CommandType.Text, true);
                _log.LogInformation($"GetStudentDetailsById method is completed at: {DateTime.Now} with output: {JsonConvert.SerializeObject(objStudentInfo)}");
                return objStudentInfo;
            }
            catch (Exception ex)
            {
                _log.LogError($"Error in GetStudentDetailsById method at: {DateTime.Now}, exception: {ex.Message}");
                throw;
            }

        }

        public async Task<int> DeleteStudentDetails(int studentId)
        {
            try
            {
                _log.LogInformation($"DeleteStudentDetails method started with input studentId: {studentId} at: {DateTime.Now}");
                string strQuery = $"Delete From Students Where StudentId = " + studentId;
                var result = (int)await _objDapper.Execute<int>(strQuery, null, commandType: CommandType.Text, isSingleRecord: true);
                _log.LogInformation($"DeleteStudentDetails method is completed at: {DateTime.Now}");
                return result;
            }
            catch (Exception ex)
            {
                _log.LogError($"Error in DeleteStudentDetails method at: {DateTime.Now}, exception: {ex.Message}");
                throw;
            }
        }
    }

}

