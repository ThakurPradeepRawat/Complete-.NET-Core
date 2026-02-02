using Common.DAL.Base;
using Common.DAL.DataBase;
using Common.DAL.Exceptions;
using Common.DAL.Interface;
using Common.DAL.Mapper;
using Common.Model.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.PortableExecutable;

namespace Common.DAL.Repository
{
    public class StudentRepository : RepositoryBase, IStudentRepository 
    {
        private readonly SqlConnectionFactory _connectionFactory;

        public StudentRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public int Add(StudentModel student)
        {
            return ExuteDb(() =>
        {
            SqlConnection con = _connectionFactory.CreateConnection();
            SqlCommand cmd = new SqlCommand("sp_AddStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
            cmd.Parameters.AddWithValue("@LastName", student.LastName);
            cmd.Parameters.AddWithValue("@Email", student.Email);
            cmd.Parameters.AddWithValue("@MobileNumber", student.MobileNumber);
            cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
            cmd.Parameters.AddWithValue("@FathersName", student.FathersName);
            cmd.Parameters.AddWithValue("@MothersName", student.MothersName);
            cmd.Parameters.AddWithValue("@Address", student.Address);
            cmd.Parameters.AddWithValue("@IsActive", student.IsActive);
            cmd.Parameters.AddWithValue("@CreatedAt", student.CreatedAt);
            cmd.Parameters.AddWithValue("@UpdateAt", student.UpdatedAt);
            con.Open();
            var dr = cmd.ExecuteReader();
     
            while (dr.Read())
            {
                if ((int)dr["Success"] == 0)
                {
                    throw new EmailException(dr["Message"].ToString());
                }
              
            }
            con.Close();
            return 6;

        }, "Failed to add Student");
        }

        public void Update(StudentModel student)
        {
            ExuteDb(() =>
            {
                SqlConnection con = _connectionFactory.CreateConnection();
                SqlCommand cmd = new SqlCommand("sp_updateStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentID", student.StudentID);
                cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                cmd.Parameters.AddWithValue("@LastName", student.LastName);
                cmd.Parameters.AddWithValue("@Email", student.Email);
                cmd.Parameters.AddWithValue("@MobileNumber", student.MobileNumber);
                cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
                cmd.Parameters.AddWithValue("@FathersName", student.FathersName);
                cmd.Parameters.AddWithValue("@MothersName", student.MothersName);
                cmd.Parameters.AddWithValue("@Address", student.Address);
                cmd.Parameters.AddWithValue("@UpdateAt", student.UpdatedAt);
                con.Open();
                int rowUpdates = cmd.ExecuteNonQuery();
                con.Close();
            }, "Student Not Updated");
        
        }

        public void Delete(int StudentId)
        {
            ExuteDb(() =>
            {
                SqlConnection con = _connectionFactory.CreateConnection();
                SqlCommand cmd = new SqlCommand("sp_DeleteStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentID", StudentId);
                con.Open();
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if ((int)dr["Success"] == 0)
                    {
                        throw new StudentIdException(dr["message"].ToString());
                    }
                }
                
                con.Close();
            },"Student Not Deleted"); 
        }

        public IEnumerable<StudentModel> GetById(int StudentId)
        {
           return ExuteDb(() =>
            {
                SqlConnection con = _connectionFactory.CreateConnection();
                SqlCommand cmd = new SqlCommand("sp_GetById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentID", StudentId);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                var student = new List<StudentModel>();
                while (dr.Read())
                {
                    student.Add(StudentMapper.Map(dr));
                }
                con.Close();
                return student;
            } ,"Student Data not Fetched");



        }

        public IEnumerable<StudentModel> GetAll()
        {
            return ExuteDb(() =>
            {
                var students = new List<StudentModel>();
                SqlConnection con = _connectionFactory.CreateConnection();
                SqlCommand cmd = new SqlCommand("sp_GetAll", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    students.Add(StudentMapper.Map(reader));
                }
                con.Close();

                return students;
            }, "Studens Data not Fetched");


        }
        public async Task <string> BulkDataValidation(DataTable dt)
        {
            string json;
            SqlConnection con = _connectionFactory.CreateConnection();
            SqlCommand cmd = new SqlCommand("sp_ValidateStudents", con);
            cmd.CommandType= CommandType.StoredProcedure;
            var batchParam = new SqlParameter("@BatchID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(batchParam);
            var param = cmd.Parameters.AddWithValue("@Students", dt);
            param.SqlDbType = SqlDbType.Structured;
            param.TypeName = "StudentTableType";
            await con.OpenAsync();
            json = (string) await cmd.ExecuteScalarAsync();
            return json;

        }
        public void AddBulk(int BatchID)
        {
            SqlConnection con = _connectionFactory.CreateConnection();
            SqlCommand cmd = new SqlCommand("sp_SubmitBulkData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BatchID", BatchID);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }


    }
}
