using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Websitecourse.Models;

namespace Websitecourse.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            List<Field> list = db.Fields.ToList();
            return View(list);
        }
        public ActionResult Details(int courseId)
        {
            Course course = db.Courses.Find(courseId);
            if (course == null)
            {
                return base.HttpNotFound();
            }
            Session["CourseId"] = courseId;
            return View(course);
        }
        // GET: GetCoursesBYStudent/Delete/5
        public ActionResult Delete(int id)
        {
            var course = db.ApplyToCourses.Find(id);
            if (course == null)
            {
                HttpNotFound();
            }
            return View(course);
        }
        //Post: GetCoursesBYStudent/Delete/5
        [HttpPost]
        public ActionResult Delete(ApplyToCourse model)
        {
            var course = db.ApplyToCourses.Find(model.Id);
            db.ApplyToCourses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("GetCoursesBYStudent");
        }
        // GET: GetCoursesBYStudent/Edit/5
        public ActionResult Edit(int id)
        {
            var course = db.ApplyToCourses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: GetCoursesBYStudent/Edit/5
        [HttpPost]
        public ActionResult Edit(ApplyToCourse course)
        {
            if (ModelState.IsValid)
            {
                if (course != null)
                {
                    course.ApplyDate = DateTime.Now;
                    db.Entry(course).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("GetCoursesBYStudent");
            }
            return View(course);

        }
        [Authorize]
        [HttpGet]
        public ActionResult Apply()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Apply(string message)
        {
            var userId = User.Identity.GetUserId();
            int courseId = (int)Session["CourseId"];
            var check = db.ApplyToCourses.Where(m => m.CourseId == courseId && m.UserId == userId).ToList();
            if (check.Count < 1)
            {
                ApplyToCourse course = new ApplyToCourse();
                course.CourseId = courseId;
                course.UserId = userId;
                course.Message = message;
                course.ApplyDate = DateTime.Now;
                db.ApplyToCourses.Add(course);
                db.SaveChanges();
                ViewBag.Result = "Applyed is done";
            }
            else
            {
                ViewBag.Result = "you can not be apply the same course";
            }
            return View();

        }

        [Authorize]
        public ActionResult GetCoursesBYStudent()
        {
            string studentId = User.Identity.GetUserId();
            var course = db.ApplyToCourses.Where(m => m.UserId == studentId);

            return View(course.ToList());
        }
        [Authorize]
        public ActionResult DetailsOfCourse(int id)
        {
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }
        [Authorize]
        public ActionResult GetCoursesBYTeacher()
        {
            string teacherId = User.Identity.GetUserId();
            var myCourses = from app in db.ApplyToCourses
                            join c in db.Courses
                            on app.CourseId equals c.CourseID
                            where c.User.Id == teacherId
                            select app;
            var grouped = from m in myCourses
                          group m by m.course.CourseTitle
                          into gr
                          select new CourseViewModel
                          {
                              CourseTitle = gr.Key,
                              Items = gr
                          };

            return View(grouped.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Search() {
            return View();
        }
        [HttpPost]
        public ActionResult Search(string searchName)
        {
            var Result = db.Courses.Where(m => m.CourseTitle.Contains(searchName)
            || m.CourseContent.Contains(searchName)
            || m.Field.FieldName.Contains(searchName)
            || m.Field.FieldDescription.Contains(searchName)).ToList();

            return base.View(Result);
    }
    [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(ContactModel model)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(model.Email);
            mail.To.Add(new MailAddress("azzaashry14@gmail.com"));
            mail.Subject = model.Subject;
            mail.IsBodyHtml = true;
            string body ="Sender Name "+ model.Name +
                            "<br/>Sender Email "+ model.Email+
                            "<br/>Subject" + model.Subject+
                            "<br/>Body Message"+model.Message ;
            mail.Body = body;
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
              smtpClient.UseDefaultCredentials = false;
             smtpClient.Credentials = new NetworkCredential("azzaashry14@gmail.com", "sabreen1991");
             smtpClient.EnableSsl = true;
             smtpClient.Send(mail);
            return RedirectToAction("Index");
        }
    }
}