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
    
    public partial class tblCart
    {
        public int id { get; set; }
        public string transno { get; set; }
        public string pcode { get; set; }
        public Nullable<decimal> price { get; set; }
        public Nullable<decimal> ourprice { get; set; }
        public Nullable<double> qty { get; set; }
        public Nullable<decimal> Linedisc { get; set; }
        public Nullable<decimal> total { get; set; }
        public Nullable<System.DateTime> sdate { get; set; }
        public string status { get; set; }
        public string Description { get; set; }
        public string cashier { get; set; }
        public Nullable<decimal> GrossTotalDiscount { get; set; }
        public Nullable<decimal> HiddenOurPrice { get; set; }
        public Nullable<decimal> LoyalityPrice { get; set; }
        public string PaymentMethod { get; set; }
        public Nullable<decimal> CashAmount { get; set; }
        public Nullable<decimal> CreditCardAmount { get; set; }
        public Nullable<decimal> CreditCardInterest { get; set; }
        public Nullable<decimal> CreditAmount { get; set; }
        public Nullable<decimal> PayingAmount { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public Nullable<decimal> NetTotal { get; set; }
        public Nullable<decimal> LineDiscPer { get; set; }
        public Nullable<decimal> LineDiscAmt { get; set; }
        public Nullable<decimal> TotalBillDiscPer { get; set; }
        public Nullable<decimal> TotalBillDiscAmount { get; set; }
        public Nullable<decimal> TotalBillDiscPerAmount { get; set; }
        public Nullable<decimal> BillPrice { get; set; }
        public string SinhalaDesc { get; set; }
        public Nullable<decimal> DifferentDisc { get; set; }
        public string PrintLanguage { get; set; }
        public Nullable<double> ItemCount { get; set; }
        public string Time { get; set; }
        public Nullable<decimal> TotalDiscounts { get; set; }
        public string Counter { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public Nullable<decimal> TotalCost { get; set; }
    }
}
