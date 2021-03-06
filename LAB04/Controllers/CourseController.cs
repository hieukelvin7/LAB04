using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LAB04.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace LAB04.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course
        BigSchoolContext context = new BigSchoolContext();
        public ActionResult Create()
        {
            
            Course objCourse = new Course();
            objCourse.ListCategory= context.Categories.ToList();
            return View(objCourse);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course objCourse)
        {
            ModelState.Remove("LecturerId");
            if (!ModelState.IsValid)
            {
                objCourse.ListCategory = context.Categories.ToList();
                return View("Create", objCourse);
            }
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            objCourse.LecturerId = user.Id;
            context.Courses.Add(objCourse);
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Attending()
        {
            ApplicationUser currentUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>()
                                           .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var listAttendances = context.Attendences.Where(p => p.Attendence1 == currentUser.Id).ToList();
            var courses = new List<Course>();
            foreach (Attendence temp in listAttendances)
            {
                Course obj = temp.Course;
                obj.LectureName = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>()
                                   .FindById(obj.LecturerId).Name;
                courses.Add(obj);
            }
            return View(courses);
        }

        public ActionResult Mine()
        {
            ApplicationUser currentUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>()
                                           .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var courses = context.Courses.Where(c => c.LecturerId == currentUser.Id && c.DateTime > DateTime.Now).ToList();
            foreach (Course i in courses)
            {
                i.LectureName = currentUser.Name;
            }
            return View(courses);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {

            Course c = context.Courses.SingleOrDefault(p => p.Id == id);
            c.ListCategory = context.Categories.ToList();
            return View(c);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Edit(Course c)
        {

            Course edit = context.Courses.SingleOrDefault(p => p.Id == c.Id);
            if (edit != null)
            {
                context.Courses.AddOrUpdate(c);
                context.SaveChanges();
                
            }
            return RedirectToAction("Mine");
        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            
            Course delete = context.Courses.SingleOrDefault(p => p.Id == id);
            return View(delete);
        }
        [HttpPost]
        public ActionResult DeleteCourse(int id)
        {
           
            Course delete = context.Courses.SingleOrDefault(p => p.Id == id);
            if (delete != null)
            {
                context.Courses.Remove(delete);
                context.SaveChanges();

            }
            return RedirectToAction("Mine");
        }
        public ActionResult LectureIamGoing()
        {
            ApplicationUser currentUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>()
                                           .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var listFollowee = context.Followings.Where(p => p.FollowerId == currentUser.Id).ToList();
            var listAttendances = context.Attendences.Where(p => p.Attendence1 == currentUser.Id).ToList();
            var courses = new List<Course>();
            foreach (var course in listAttendances)
            {
                foreach (var item in listFollowee)
                {
                    if (item.FolloweeId == course.Course.LecturerId)
                    {
                        Course obj = course.Course;
                        obj.LectureName = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>()
                                   .FindById(obj.LecturerId).Name;
                        courses.Add(obj);
                    }
                }
            }
            return View(courses);
        }
       
    }
}