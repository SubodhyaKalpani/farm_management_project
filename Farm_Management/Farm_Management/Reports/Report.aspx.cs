using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Farm_Management.Models;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Data.Entity;

namespace Farm_Management.Reports
{
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var id = Request.QueryString["id"];

            if (!Page.IsPostBack)
            {
                if (id == "1")
                {
                    string searchText = string.Empty;
                    string searchTextTwo = string.Empty;


                    string st = "0";
                    string st1 = "0";



                    if (Request.QueryString["searchText"] != null && Request.QueryString["searchText1"] != null)
                    {
                        searchText = Request.QueryString["searchText"].ToString();
                        searchTextTwo = Request.QueryString["searchText1"].ToString();


                        st = searchText;
                        st1 = searchTextTwo;


                    }



                    using (FARMEntities db = new FARMEntities())
                    {
                        DateTime dt = Convert.ToDateTime(st);
                        DateTime dt1 = Convert.ToDateTime(st1);

                        GLListReportViewer.LocalReport.ReportPath = Server.MapPath("~/Reports/Report_Sell.rdlc");
                        GLListReportViewer.LocalReport.DataSources.Clear();

                        ReportDataSource rdc1 = new ReportDataSource("DataSet_Sell", db.PROC_SELL(dt, dt1).ToList());




                        GLListReportViewer.LocalReport.DataSources.Add(rdc1);


                    }




                    GLListReportViewer.LocalReport.Refresh();
                    GLListReportViewer.DataBind();
                }

               else if (id == "2")
                {
                    string searchText = string.Empty;
                    string searchTextTwo = string.Empty;


                    string st = "0";
                    string st1 = "0";



                    if (Request.QueryString["searchText"] != null && Request.QueryString["searchText1"] != null)
                    {
                        searchText = Request.QueryString["searchText"].ToString();
                        searchTextTwo = Request.QueryString["searchText1"].ToString();


                        st = searchText;
                        st1 = searchTextTwo;


                    }



                    using (FARMEntities db = new FARMEntities())
                    {
                        DateTime dt = Convert.ToDateTime(st);
                        DateTime dt1 = Convert.ToDateTime(st1);

                        GLListReportViewer.LocalReport.ReportPath = Server.MapPath("~/Reports/Report_Profit.rdlc");
                        GLListReportViewer.LocalReport.DataSources.Clear();

                        ReportDataSource rdc1 = new ReportDataSource("DataSet_Profit", db.Proc_PROFIT_RPT(dt, dt1).ToList());




                        GLListReportViewer.LocalReport.DataSources.Add(rdc1);


                    }




                    GLListReportViewer.LocalReport.Refresh();
                    GLListReportViewer.DataBind();
                }
            }
        }
    }
}