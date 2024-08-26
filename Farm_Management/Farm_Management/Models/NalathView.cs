using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Farm_Management.Models
{
    public class NalathView
    {
        public int ID { get; set; }
        public Nullable<int> YEAR { get; set; }
        public Nullable<int> MONTH { get; set; }
        public string STATUS { get; set; }
        public Nullable<System.DateTime> DATE { get; set; }
        [DisplayName("Upload Image")]
        public string Img_path { get; set; }

        public HttpPostedFileBase Img_File { get; set; }
    }
}