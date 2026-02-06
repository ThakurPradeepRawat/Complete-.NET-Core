using Common.DAL.Interface;
using Common.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt;
using Common.BAL.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Common.BAL.Services
{
    public class UserValidationService:IUserValidation
    {
        private readonly IStudentRepository _studentRepository;
        public UserValidationService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public bool PassWordValidation(LoginDTO Login)
        {
            string HashPassword = _studentRepository.GetPassWord(Login.Email);
            bool IsValid = BCrypt.Net.BCrypt.Verify(Login.Password, HashPassword);
            return IsValid;
        }

        public void RegisterUser(LoginDTO Login)
        {
            Console.WriteLine(Login);
           string  Email = Login.Email;
           string PasswordHash = BCrypt.Net.BCrypt.HashPassword(Login.Password);
            _studentRepository.RagisterUser(Email, PasswordHash);
        }

            
    }
}
