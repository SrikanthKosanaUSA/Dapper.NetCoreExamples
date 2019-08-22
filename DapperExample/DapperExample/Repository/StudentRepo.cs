using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DapperExample.Models;
using Dapper;

namespace DapperExample.Repository
{
    public class StudentRepo
    {
        private readonly IConfiguration _configuration;

        public StudentRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<Student> ReadStudents()
        {
            //int count = 0;

            //try
            //{
                using (var conn = new SqlConnection(_configuration.GetConnectionString("SQLConnection")))
                {
                    conn.Open();
                    var students = (conn.Query<Student>(@"Select * From tblStudent"));
                    return students;
                }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
        }

        public Student SearchStudent(int id)
        {
            try
            {
                using (var conn = new SqlConnection(_configuration.GetConnectionString("SQLConnection")))
                {
                    conn.Open();
                    var student = (conn.QuerySingle<Student>(@"Select * From tblStudent Where StudentID = @sid", new { sid = id }));
                    return student;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
           
        }

        public void NewStudent(Student student)
        {
            Student _student = new Student();
            _student = student;
            string sql = "INSERT INTO tblStudent (FirstName, LastName, Email, Branch, Phone) VALUES (@FN, @LN, @E, @B, @Ph);";
            try
            {
                using (var conn = new SqlConnection(_configuration.GetConnectionString("SQLConnection")))
                {
                    conn.Open();
                    int affectedRows = conn.Execute(sql, new { FN = _student.FirstName, LN = _student.LastName, E = _student.Email, B = _student.Branch, Ph = _student.Phone });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void UpdateStudentDetails(Student newstudentDetails)
        {
            Student _student = new Student();
            _student = newstudentDetails;
            string sql = "UPDATE tblStudent SET FirstName = @FN, LastName = @LN, Email = @E, Branch = @B, Phone = @Ph WHERE StudentID = @id;";

            using (var conn = new SqlConnection(_configuration.GetConnectionString("SQLConnection")))
            {
                conn.Open();
                int affectedRows = conn.Execute(sql, new { FN = _student.FirstName, LN = _student.LastName, E = _student.Email, B = _student.Branch, Ph = _student.Phone, id = _student.StudentID });
            }
        }
    }
}
