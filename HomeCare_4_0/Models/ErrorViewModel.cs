using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeCare_4_0.Models
{
    public class ErrorViewModel
    {
        public string code { get; set; }
        public string message_1 { get; set; }
        public string message_2 { get; set; }
        public HandleErrorInfo HandleError { get; set; }
    }
}