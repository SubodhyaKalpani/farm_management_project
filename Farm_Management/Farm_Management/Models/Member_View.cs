using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farm_Management.Models
{
    public class Member_View
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public System.DateTime Bod { get; set; }
        public string District { get; set; }
        public string Telephone_No { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }
        public string Entered_User { get; set; }
        public System.DateTime Entered_Date { get; set; }
       
        public string password { get; set; }
        public string Confirmpassword { get; set; }
    }
}