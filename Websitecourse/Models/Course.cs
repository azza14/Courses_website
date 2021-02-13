using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Websitecourse.Models
{
    public class Course
    {
        public int CourseID { get; set; }

        [Required, Display(Name = "Course Title")]
        public string CourseTitle { get; set; }

        [AllowHtml, Required, Display(Name = "Course Content")]
        public string CourseContent { get; set; }

        [Required, Display(Name = "Course Date"), 
                  DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}"), DataType(DataType.Date)]
        public DateTime CourseDate { get; set; }

        [Required, Display(Name = "Course Duration")]
        public string CourseDuration { get; set; }

        [Display(Name = "Course Image")]
        public string CourseImage { get; set; }
        //ther R==>1 ---->M
        public int FieldId { get; set; }
        public virtual Field Field { get; set; }

        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }


    }
}