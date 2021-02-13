using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Websitecourse.Models
{
    public class CourseViewModel
    {
        public string CourseTitle { get; set; }
        public IEnumerable<ApplyToCourse> Items { get; set; }
    }
}