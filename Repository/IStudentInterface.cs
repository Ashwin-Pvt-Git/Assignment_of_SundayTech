using Assignment_of_SundayTech.Model;
using System.Threading.Tasks;

namespace Assignment_of_SundayTech.Repository
{
    public interface IStudentInterface
    {
        Task<int> SaveStudentDetails(Student objStudent);
        Task<int> UpdateStudentDetails(int studentId, Student objStudent);
        Task<Student> GetStudentDetailsById(int studentId);       
        Task<int> DeleteStudentDetails(int studentId);
    }
}
