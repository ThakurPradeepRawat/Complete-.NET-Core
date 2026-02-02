using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model.DTO;

namespace Common.BAL.Interfaces
{
   public  interface IExcelService 
    {
        public Task<string> ReadStudents(Stream stream);
        void AddBulk(int BatchId);
    }
}
