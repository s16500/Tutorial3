using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Tutorial3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tutorial3.DAL;
using System.Data;

namespace Tutorial3.Controllers
{
    [ApiController]
    [Route("api/students")]
    
    public class StudentsController : ControllerBase
    {
        private string ConnString = "Data Source=db-mssql.pjwstk.edu.pl;Initial Catalog=2019SBD;Integrated Security=True";
        private readonly IDbService _dbservice;
        List<Student> studentList;
        public StudentsController(IDbService dbService)

        {
            _dbservice = dbService;
        }


        [HttpGet]
        public IActionResult GetStudents()
        {

            using (var client = new SqlConnection(ConnString))
            using (var com = new SqlCommand())

            {

                com.Connection = client;
                com.CommandText = "select * from students";

                client.Open();
                var dr = com.ExecuteReader();


                studentList = new List<Student>();
                while (dr.Read())
                {
                    var st = new Student();
                    st.idStudent = Convert.ToInt32(dr["IdStudent"]);
                    st.firstName = dr["FirstName"].ToString();
                    st.lastName = dr["LastName"].ToString();
                    studentList.Add(st);

                }

            }
            return Ok(studentList);
        }
        /*
        [HttpPost]
        public IActionResult CreateIndex(Student newIndex)
        {
            newIndex.IndexNumber = $"s{new Random().Next(1, 20000)}"; 
            return Ok(newIndex);
        }*/

        
        [HttpPost]
        public IActionResult CreateStudent(Student newStudent)
        {
            using (SqlConnection con = new SqlConnection(ConnString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "insert into students (idStudent,firstName,lastName,indexNumber) values (@par1,@par2,@par3,@par4)";

                com.Parameters.AddWithValue("par1", newStudent.idStudent);
                com.Parameters.AddWithValue("par2", newStudent.firstName);
                com.Parameters.AddWithValue("par3", newStudent.lastName);
                com.Parameters.Add("par4",  SqlDbType.VarChar, 50).Value = $"s{new Random().Next(1, 20000)}";
              


                con.Open();
                int affectedRows = com.ExecuteNonQuery();
            }
            return Ok();

        }

        
    }
}