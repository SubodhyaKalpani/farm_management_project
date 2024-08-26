using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farm_Management.Models
{
    public class ProductDetailsL
    {
        public int PD_ID { get; set; }
        public string PRODUCT { get; set; }
        public string DESCR { get; set; }
        public Nullable<System.DateTime> DATE { get; set; }
    }
}