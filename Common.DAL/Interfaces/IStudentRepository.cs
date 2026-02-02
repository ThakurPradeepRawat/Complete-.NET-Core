using Common.Model.Entities;
using System.Collections.Generic;
using System.Data;

namespace Common.DAL.Interface
{
    public interface IStudentRepository
    {
        int Add(StudentModel student);
        void Update(StudentModel student);

        IEnumerable<StudentModel> GetById(int StudentID);

        IEnumerable<StudentModel> GetAll();

        void Delete(int StudentID);
       Task<string> BulkDataValidation(DataTable dt);
        void AddBulk(int BatchID);

    }
}
