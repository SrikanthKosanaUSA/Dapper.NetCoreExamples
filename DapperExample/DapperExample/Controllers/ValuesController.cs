using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DapperExample.Models;
using DapperExample.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DapperExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IConfiguration _config;

        public ValuesController(IConfiguration config)
        {
            _config = config;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Student Get(int id)
        {
            StudentRepo repo = new StudentRepo(_config);
            var student = repo.SearchStudent(id);
            return student;

            //return "value";
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Student> Get(int? id = null)
        {

            StudentRepo repo = new StudentRepo(_config);
            IEnumerable<Student> students = repo.ReadStudents();
            return students;
            // return new string[] { "value1", "value2" };
        }

       
        // POST api/values
        [HttpPost]
        public void Post(Student student)
        {
            StudentRepo repo = new StudentRepo(_config);
            repo.NewStudent(student);
        }

        // PUT api/values/5
        [HttpPut]
        public void Put(Student student)
        {
            StudentRepo repo = new StudentRepo(_config);
            repo.UpdateStudentDetails(student);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            StudentRepo repo = new StudentRepo(_config);
            repo.RemoveRecord(id);
        }
    }
}
