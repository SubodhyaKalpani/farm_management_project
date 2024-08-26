using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Farm_Management.Models
{
    public class StaffVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public System.DateTime Bod { get; set; }
        public int Province { get; set; }
        public string Telephone_No { get; set; }
        public string Email { get; set; }
        public string Job_Position { get; set; }
        public int Status { get; set; }
        public string Entered_User { get; set; }
        public System.DateTime Entered_Date { get; set; }
        public string Modify_User { get; set; }
        public Nullable<System.DateTime> Modify_Date { get; set; }
        [DisplayName("Upload Image")]
        public string Img_path { get; set; }

        public HttpPostedFileBase Img_File { get; set; }
    }
}