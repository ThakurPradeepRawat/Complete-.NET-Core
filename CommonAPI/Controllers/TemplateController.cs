using Common.BAL.Interfaces;
using Common.BAL.Services;
using Common.Model.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CommonAPI.Controllers
{
    [ApiController]
    [Route("students")]
    public class TemplateController : ControllerBase
    {
        private readonly IExcelService _excelService;
        private readonly IStudentService _studentService;

        public TemplateController(
            IExcelService excelService,
             IStudentService studentService)
        {
            _excelService = excelService;
         
            _studentService = studentService;   
        }

        // ---------------- DOWNLOAD TEMPLATE ----------------
        [HttpGet("download-template")]
        public IActionResult DownloadTemplate()
        {
            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Template",
                "Student Data Template.xltx"
            );

            if (!System.IO.File.Exists(path))
                return NotFound("Template file not found");

            var bytes = System.IO.File.ReadAllBytes(path);

            return File(
                bytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.template",
                "Student_Data_Template.xltx"
            );
        }

        // ---------------- UPLOAD TEMPLATE ----------------
        [HttpPost("upload-template")]
        public async Task<IActionResult> UploadTemplate(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");


            using var stream = file.OpenReadStream();

            // Read Excel
            var students =
                await _excelService.ReadStudents(stream);
           
            return Content(students, "application/json");

        }

        // ------------- bulk upload --------------------------
        [HttpGet("bulk-upload")]
        public IActionResult bulkUpload(int batchId)
        {
            _excelService.AddBulk(batchId);
            
      
            return Created();
        }
    }
}