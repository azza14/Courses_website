using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Websitecourse.Models
{
    public class ContactModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        [AllowHtml, Required, DataType(DataType.MultilineText)]
        public string Message { get; set; }
    }
}