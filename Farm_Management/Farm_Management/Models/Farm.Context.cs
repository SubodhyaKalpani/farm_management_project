﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class FARMEntities : DbContext
    {
        public FARMEntities()
            : base("name=FARMEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<PRODUCT_TYPE> PRODUCT_TYPE { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<PROVINCE> PROVINCEs { get; set; }
        public virtual DbSet<tblStockIn> tblStockIns { get; set; }
        public virtual DbSet<PRODUCT_DETAILS> PRODUCT_DETAILS { get; set; }
        public virtual DbSet<V_Product_Det> V_Product_Det { get; set; }
        public virtual DbSet<NAKATH> NAKATHs { get; set; }
        public virtual DbSet<tblprocntl> tblprocntls { get; set; }
        public virtual DbSet<tblCart> tblCarts { get; set; }
        public virtual DbSet<tblCompany> tblCompanies { get; set; }
        public virtual DbSet<tmpcart> tmpcarts { get; set; }
        public virtual DbSet<FARM_CHAT_ANSWERS> FARM_CHAT_ANSWERS { get; set; }
        public virtual DbSet<FARM_CHAT_QUESTIONS> FARM_CHAT_QUESTIONS { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
    
        public virtual ObjectResult<string> Proc_INCREMENT_ID(string tYPE)
        {
            var tYPEParameter = tYPE != null ?
                new ObjectParameter("TYPE", tYPE) :
                new ObjectParameter("TYPE", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("Proc_INCREMENT_ID", tYPEParameter);
        }
    
        public virtual ObjectResult<string> Proc_INCREMENT_ID_Update(string tYPE)
        {
            var tYPEParameter = tYPE != null ?
                new ObjectParameter("TYPE", tYPE) :
                new ObjectParameter("TYPE", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("Proc_INCREMENT_ID_Update", tYPEParameter);
        }
    
        public virtual int Proc_QTY_Update(string rEFNO, string pCODE, Nullable<int> qTY, string vENDOR, string iNVOICENO, string dESCRIPTION, Nullable<int> cOST, Nullable<int> uNITPRICE, Nullable<int> oURPRICE, Nullable<int> lOYALITYPRICE, Nullable<System.DateTime> eXDATE, Nullable<int> gROSS_TOT, Nullable<int> dISCOUNT, Nullable<int> nETOT, string cONTACTPERS, string aDDRES, string tELEPHONE, Nullable<System.DateTime> dUEDATE, Nullable<int> pAIDAMT, string pAYEDSTATUS, Nullable<int> fQTY, Nullable<int> pID)
        {
            var rEFNOParameter = rEFNO != null ?
                new ObjectParameter("REFNO", rEFNO) :
                new ObjectParameter("REFNO", typeof(string));
    
            var pCODEParameter = pCODE != null ?
                new ObjectParameter("PCODE", pCODE) :
                new ObjectParameter("PCODE", typeof(string));
    
            var qTYParameter = qTY.HasValue ?
                new ObjectParameter("QTY", qTY) :
                new ObjectParameter("QTY", typeof(int));
    
            var vENDORParameter = vENDOR != null ?
                new ObjectParameter("VENDOR", vENDOR) :
                new ObjectParameter("VENDOR", typeof(string));
    
            var iNVOICENOParameter = iNVOICENO != null ?
                new ObjectParameter("INVOICENO", iNVOICENO) :
                new ObjectParameter("INVOICENO", typeof(string));
    
            var dESCRIPTIONParameter = dESCRIPTION != null ?
                new ObjectParameter("DESCRIPTION", dESCRIPTION) :
                new ObjectParameter("DESCRIPTION", typeof(string));
    
            var cOSTParameter = cOST.HasValue ?
                new ObjectParameter("COST", cOST) :
                new ObjectParameter("COST", typeof(int));
    
            var uNITPRICEParameter = uNITPRICE.HasValue ?
                new ObjectParameter("UNITPRICE", uNITPRICE) :
                new ObjectParameter("UNITPRICE", typeof(int));
    
            var oURPRICEParameter = oURPRICE.HasValue ?
                new ObjectParameter("OURPRICE", oURPRICE) :
                new ObjectParameter("OURPRICE", typeof(int));
    
            var lOYALITYPRICEParameter = lOYALITYPRICE.HasValue ?
                new ObjectParameter("LOYALITYPRICE", lOYALITYPRICE) :
                new ObjectParameter("LOYALITYPRICE", typeof(int));
    
            var eXDATEParameter = eXDATE.HasValue ?
                new ObjectParameter("EXDATE", eXDATE) :
                new ObjectParameter("EXDATE", typeof(System.DateTime));
    
            var gROSS_TOTParameter = gROSS_TOT.HasValue ?
                new ObjectParameter("GROSS_TOT", gROSS_TOT) :
                new ObjectParameter("GROSS_TOT", typeof(int));
    
            var dISCOUNTParameter = dISCOUNT.HasValue ?
                new ObjectParameter("DISCOUNT", dISCOUNT) :
                new ObjectParameter("DISCOUNT", typeof(int));
    
            var nETOTParameter = nETOT.HasValue ?
                new ObjectParameter("NETOT", nETOT) :
                new ObjectParameter("NETOT", typeof(int));
    
            var cONTACTPERSParameter = cONTACTPERS != null ?
                new ObjectParameter("CONTACTPERS", cONTACTPERS) :
                new ObjectParameter("CONTACTPERS", typeof(string));
    
            var aDDRESParameter = aDDRES != null ?
                new ObjectParameter("ADDRES", aDDRES) :
                new ObjectParameter("ADDRES", typeof(string));
    
            var tELEPHONEParameter = tELEPHONE != null ?
                new ObjectParameter("TELEPHONE", tELEPHONE) :
                new ObjectParameter("TELEPHONE", typeof(string));
    
            var dUEDATEParameter = dUEDATE.HasValue ?
                new ObjectParameter("DUEDATE", dUEDATE) :
                new ObjectParameter("DUEDATE", typeof(System.DateTime));
    
            var pAIDAMTParameter = pAIDAMT.HasValue ?
                new ObjectParameter("PAIDAMT", pAIDAMT) :
                new ObjectParameter("PAIDAMT", typeof(int));
    
            var pAYEDSTATUSParameter = pAYEDSTATUS != null ?
                new ObjectParameter("PAYEDSTATUS", pAYEDSTATUS) :
                new ObjectParameter("PAYEDSTATUS", typeof(string));
    
            var fQTYParameter = fQTY.HasValue ?
                new ObjectParameter("FQTY", fQTY) :
                new ObjectParameter("FQTY", typeof(int));
    
            var pIDParameter = pID.HasValue ?
                new ObjectParameter("PID", pID) :
                new ObjectParameter("PID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Proc_QTY_Update", rEFNOParameter, pCODEParameter, qTYParameter, vENDORParameter, iNVOICENOParameter, dESCRIPTIONParameter, cOSTParameter, uNITPRICEParameter, oURPRICEParameter, lOYALITYPRICEParameter, eXDATEParameter, gROSS_TOTParameter, dISCOUNTParameter, nETOTParameter, cONTACTPERSParameter, aDDRESParameter, tELEPHONEParameter, dUEDATEParameter, pAIDAMTParameter, pAYEDSTATUSParameter, fQTYParameter, pIDParameter);
        }
    
        public virtual ObjectResult<PROC_SELL_Result> PROC_SELL(Nullable<System.DateTime> fDATE, Nullable<System.DateTime> lDATE)
        {
            var fDATEParameter = fDATE.HasValue ?
                new ObjectParameter("FDATE", fDATE) :
                new ObjectParameter("FDATE", typeof(System.DateTime));
    
            var lDATEParameter = lDATE.HasValue ?
                new ObjectParameter("LDATE", lDATE) :
                new ObjectParameter("LDATE", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PROC_SELL_Result>("PROC_SELL", fDATEParameter, lDATEParameter);
        }
    
        public virtual ObjectResult<Proc_PROFIT_RPT_Result> Proc_PROFIT_RPT(Nullable<System.DateTime> dATE, Nullable<System.DateTime> dATE1)
        {
            var dATEParameter = dATE.HasValue ?
                new ObjectParameter("DATE", dATE) :
                new ObjectParameter("DATE", typeof(System.DateTime));
    
            var dATE1Parameter = dATE1.HasValue ?
                new ObjectParameter("DATE1", dATE1) :
                new ObjectParameter("DATE1", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Proc_PROFIT_RPT_Result>("Proc_PROFIT_RPT", dATEParameter, dATE1Parameter);
        }
    }
}
