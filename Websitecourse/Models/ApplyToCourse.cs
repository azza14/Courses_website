using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Websitecourse.Models
{
    public class ApplyToCourse
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime ApplyDate { get; set; }

        public int CourseId { get; set; }

        public string UserId { get; set; }

        public virtual Course course { get; set; }

        public ApplicationUser user { get; set; }
    }
}