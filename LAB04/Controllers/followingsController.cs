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
    public class followingsController : ApiController
    {
        BigSchoolContext db = new BigSchoolContext();
        [HttpPost]
        public IHttpActionResult Follow(Following follow)
        {
            var userID = User.Identity.GetUserId();
            if (userID == null)
            {
                return BadRequest("Please login first!");
            }
            if (userID == follow.FolloweeId)
            {
                return BadRequest("Can not follow myself!");
            }
            Following find = db.Followings.FirstOrDefault(n => n.FollowerId == userID && n.FolloweeId == follow.FolloweeId);
            if (find != null)
            {
                //return BadRequest("The already following exists!");
                db.Followings.Remove(db.Followings.SingleOrDefault(p => p.FollowerId == userID && p.FolloweeId == follow.FolloweeId));
                db.SaveChanges();
                return Ok("cancel");
            }
            follow.FollowerId = userID;
            db.Followings.Add(follow);
            db.SaveChanges();
            return Ok();
        }
    }
}
