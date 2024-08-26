using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Farm_Management.Models
{
    public class ProductView
    {
        public int PId { get; set; }
        public string pcode { get; set; }
        public string barcode { get; set; }
        public string pdesc { get; set; }
        public Nullable<decimal> price { get; set; }
        public Nullable<double> qty { get; set; }
        public Nullable<double> reorder { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public Nullable<int> Brand_Id { get; set; }
        public Nullable<int> Category_Id { get; set; }
        public string Province { get; set; }
        public string Product_Type { get; set; }

        [DisplayName("Upload Image")]
        public string Img_path { get; set; }

        public HttpPostedFileBase Img_File { get; set; }

        
    }
}