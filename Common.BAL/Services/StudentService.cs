using Common.BAL.Interfaces;
using Common.DAL.Interface;
using Common.Model.DTO;
using Common.Model.Entities;
using Common.BAL.BAL.Exceptions.Derived;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Common.BAL.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public int AddStudent(CreateStudentRequestDTO dto)
        {
         
            var student = new StudentModel
            {
         
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                MobileNumber = dto.MobileNumber,
                DateOfBirth = dto.DateOfBirth,
                FathersName = dto.FathersName,
                MothersName = dto.MothersName,
                Address = dto.Address,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow


            };
            return _studentRepository.Add(student);

        }
        public IEnumerable<StudentModel> GetAll()
        {
            return _studentRepository.GetAll();
        }

        public void DeleteStudent(int studentId)
        {
            _studentRepository.Delete(studentId);


        }

        public void UpdateStudent(UpdateStudentRequestDTO dto)
        {
            var student = new StudentModel
            {
                StudentID = dto.StudentID,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                MobileNumber = dto.MobileNumber,
                DateOfBirth = dto.DateOfBirth,
                FathersName = dto.FathersName,
                MothersName = dto.MothersName,
                Address = dto.Address,
                UpdatedAt = DateTime.UtcNow

            };
            _studentRepository.Update(student);

        }

        public IEnumerable<StudentModel> GetByID(int StudentID)
        {
            var student = _studentRepository.GetById(StudentID);
            return student;
        }

       
    }
}
