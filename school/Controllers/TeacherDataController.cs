using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using school.Models;
using MySql.Data.MySqlClient;

namespace school.Controllers
{
    public class TeacherDataController : ApiController
    {
     private SchoolDBContext School=new SchoolDBContext();

        ///<summary>
        ///This Controller will access to the teacher table of school database
        ///return a list of teachers in the system with teachername 
        /// </summary>
        /// <example1>GET api/TeacherData/ListTeachers</example>
        /// <return>A List of teachers </return>
        /// <example1>GET api/TeacherData/ListTeachers/Linda</example>
        /// <return>Linda Chan </return>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{TeacherSearchKey?}")]
        public List<Teacher> ListTeachers(string TeacherSearchKey = null)
        {
            //Step 1.Creat an instance of a connection

            MySqlConnection conn = School.AccessDatabase();

            //Step 2.Open the connection between the web server and database

            conn.Open();

            //Step 3.Establish a new command  for our database

            MySqlCommand cmd = conn.CreateCommand();

            //Step 4.SQL QUERY

            cmd.CommandText = "Select * from Teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";

            //Step 5.excute the sql command

             
            cmd.Parameters.AddWithValue("@key", "%" + TeacherSearchKey + "%");
            cmd.Prepare();

            //Step 6.Gather Result Set of Query in to a variable

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Step 7. Create an empty list of Teachers

            List<Teacher> Teachers = new List<Teacher>();

            //Step 8.Loop through each row the result set

            while (ResultSet.Read())
            {
                //Step 9.Access column info by database column name as an index

                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string EmployeeNum = ResultSet["employeenumber"].ToString();
                string Hiredate = ResultSet["hiredate"].ToString();
                decimal Salary = Convert.ToDecimal(ResultSet["salary"]);

                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNum = EmployeeNum;
                NewTeacher.Hiredate = Hiredate;
                NewTeacher.Salary = Salary;

                //Step 10.Add teacher Name to the list
                 Teachers.Add(NewTeacher);             
            }
             //Step 11.Close the connection
             conn.Close();
            
            return Teachers;
        }

        ///<summary>
        ///return an individual teacher from databaae by specifying the primary key teacherid
        /// </summary>
        /// <param name="id">the teacherid</param>
        ///  <example>GET api/TeacherData/FindTeacher/{id}</example>
        /// <return>A teacher object</return>

        [HttpGet]
        public Teacher FindTeacher(int id) {
        
            Teacher NewTeacher=new Teacher();
          //Step 1.Creat an instance of a connection

            MySqlConnection conn = School.AccessDatabase();

          //Step 2.Open the connection between the web server and database

            conn.Open();

          //Step 3.Establish a new command  for our database

            MySqlCommand cmd = conn.CreateCommand();

          //Step 4.SQL QUERY

            string query = "select t.teacherfname ,t.teacherlname, c.classname FROM teachers t LEFT JOIN classes c ON t.teacherid=c.teacherid WHERE t.teacherid=@id ";

            //Step 5.excute the sql command

            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            //Step 6.Gather Result Set of Query in to a variable

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Step 7.Loop through each row the result set
            NewTeacher.ClassNames = new List<string>();
            while (ResultSet.Read())
            {
                //Step 8.Access column info by database column name as an index

                //int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string ClassName =  ResultSet["classname"].ToString();
                //string EmployeeNum = ResultSet["employeenumber"].ToString();
                //string Hiredate = ResultSet["hiredate"].ToString(); ;
                //decimal Salary = Convert.ToDecimal(ResultSet["salary"]);

                //NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                
                NewTeacher.ClassNames.Add(ClassName);
                //NewTeacher.EmployeeNum = EmployeeNum;
                //NewTeacher.Hiredate = Hiredate;
                //NewTeacher.Salary = Salary;
            }
                //Step 9.Close the connection
                conn.Close();
            

            return NewTeacher;
        }
    }
}

