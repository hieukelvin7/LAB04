using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LAB04.Models;
using Microsoft.AspNet.Identity;

namespace LAB04.Controllers
{
    public class AttendenceController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Attend (Course attendanceDto)
        {
            var userID = User.Identity.GetUserId();
            BigSchoolContext context = new BigSchoolContext();
            if (context.Attendences.Any(p => p.Attendence1 == userID && p.CourseId == attendanceDto.Id))
            {
                //return BadRequest("Khoa hoc da ton tai");
                context.Attendences.Remove(context.Attendences.SingleOrDefault(p => p.Attendence1 == userID && p.CourseId == attendanceDto.Id));
                context.SaveChanges ();
                return Ok("cancel");
            }
            var attendance = new Attendence() { CourseId = attendanceDto.Id, Attendence1 = User.Identity.GetUserId() };
            context.Attendences.Add(attendance);
            context.SaveChanges();
            return Ok();
        }
    }
}
