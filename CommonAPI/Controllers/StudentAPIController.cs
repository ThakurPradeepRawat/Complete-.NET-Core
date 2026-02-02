using Common.BAL.Interfaces;
using Common.Model.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CommonAPI.Controllers
{
    [ApiController]
    [Route("/students")]
    public class StudentAPIController : Controller
    {
        private readonly IStudentService _studentService;
        public StudentAPIController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        [HttpPost]

        public IActionResult Add([FromBody] CreateStudentRequestDTO student)
        {
            _studentService.AddStudent(student);
            return Created();
        }

        [HttpGet]
        

        public IActionResult Get()
        {
            var students = _studentService.GetAll();
            return Ok(students);
        }


        [HttpDelete]
        

        public IActionResult Delete(int StudentId)
        {
            _studentService.DeleteStudent(StudentId);
            return Ok();
        }

        [HttpPut]

        public IActionResult Update([FromBody] UpdateStudentRequestDTO student)
        {
            _studentService.UpdateStudent(student);
            return Ok();

        }
        [HttpGet("{StudentId}")]

        public IActionResult GetByID(int StudentId)
        {
           var student =  _studentService.GetByID(StudentId);
            return Ok(student);
        }

    }
}
