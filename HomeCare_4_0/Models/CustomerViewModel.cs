using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeCare_4_0.Models
{
    public class CustomerViewModel
    {
        public long? ID { get; set; }

       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string Telephone { get; set; }
        public string FullTelephone { get; set; }
        public string Address { get; set; }
        public string VipStatus { get; set; }
    }
  
}