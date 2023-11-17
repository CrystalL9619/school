using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Google.Protobuf.WellKnownTypes;
using school.Models;


namespace school.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }
        //GET: /Teacher/List? TeacherSearchKey = { value }
        /// <summary>
        /// This Controller will return a list of teachers or a individual teacher in the system with teachername
        /// </summary>
        /// <param name="TeacherSearchKey"></param>
        /// <example>/Teacher/List</example>
        /// <returns>a list of teachers</returns>
        /// <example>/Teacher/List?TeacherSearchKey={value}</example>
        /// <returns>a teacher whose name contains {value}</returns>
        public ActionResult List(string TeacherSearchKey)
        {
            TeacherDataController controller = new TeacherDataController(); 
            List<Teacher> Teachers = controller.ListTeachers(TeacherSearchKey);
           
            return View(Teachers);
        }
        //GET: /Teacher/Shoe/{id}
        /// <summary>
        /// This Controller will return an individual teacher in the system with Class(es) he/she teachs
        /// </summary>
        /// <param name="id"></param>
        /// <example>/Teacher/Show/{id}</example>
        /// <returns>a teacher's name whose teacher id is {id} with the class(es) he/she teachs</returns>
        public ActionResult Show(int id) {
        TeacherDataController controller=new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);
            return View(NewTeacher);
        
        }
    }
}