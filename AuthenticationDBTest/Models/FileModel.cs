using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthenticationDBTest.Models
{
    public class FileModel
    {
        public int empId { get; set; }
        public HttpPostedFileBase[] file { get; set; }
    }
}