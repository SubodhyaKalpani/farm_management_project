//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Farm_Management.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tmpcart
    {
        public int id { get; set; }
        public string Inv_No { get; set; }
        public string itm_id { get; set; }
        public string item { get; set; }
        public string category { get; set; }
        public Nullable<double> price { get; set; }
        public Nullable<int> qty { get; set; }
        public Nullable<double> total { get; set; }
        public Nullable<double> subtotal { get; set; }
        public Nullable<int> Discount { get; set; }
        public Nullable<double> vat { get; set; }
        public Nullable<double> Grandtotal { get; set; }
        public Nullable<int> status { get; set; }
        public string user_name { get; set; }
        public string img_path { get; set; }
    }
}