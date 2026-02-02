using ClosedXML.Excel;
using Common.BAL.Interfaces;
using Common.DAL.Interface;
using Common.Model.DTO;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.BAL.Services
{
    public class ExcelService:IExcelService
    {
        private readonly IStudentRepository _studentRepository;
        public ExcelService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public  Task<string>  ReadStudents(Stream Stream)
        {
            using var workbook = new XLWorkbook(Stream);
            var ws = workbook.Worksheet(1);

            DataTable dt = new DataTable();
            dt.Columns.Add("FirstName" , typeof(string));
            dt.Columns.Add("LastName" , typeof(string));
            dt.Columns.Add("Email" , typeof(string));
            dt.Columns.Add("MobileNumber" , typeof(string));    
            dt.Columns.Add("DateOfBirth" , typeof(string));
            dt.Columns.Add("FathersName" , typeof (string));
            dt.Columns.Add("MothersName" , typeof(string));
            dt.Columns.Add("Address" , typeof(string)) ;
            dt.Columns.Add("IsActive", typeof(bool));
            dt.Columns.Add("CreatedAT" ,typeof(DateTime));
            dt.Columns.Add("UpdatedAt", typeof(DateTime));
     
            foreach (var row in ws.RowsUsed().Skip(1))
            {
                dt.Rows.Add(
                    row.Cell(1).GetString(), 
                    row.Cell(2).GetString(),
                    row.Cell(3).GetString(),
                    row.Cell(4).GetString(), 
                    row.Cell(5).GetString(), 
                    row.Cell(6).GetString(), 
                    row.Cell(6).GetString(), 
                    row.Cell(8).GetString(),
                    1,
                    DateTime.UtcNow,
                    DateTime.UtcNow
                    );      
                      
            }
            return _studentRepository.BulkDataValidation( dt );
            
            
        }

        public void AddBulk(int BatchId)
        {
            _studentRepository.AddBulk(BatchId);
        }
       
    }
}
