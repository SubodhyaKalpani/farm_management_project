using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Farm_Management.Models;

namespace Farm_Management.Controllers
{
    public class GRNController : Controller
    {
        // GET: GRN
        FARMEntities db = new FARMEntities();
        public ActionResult Index()
        {
            return View(db.tblStockIns.ToList());
        }

        // GET: GRN/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GRN/Create
        public ActionResult Create()
        {
            ViewBag.INV = db.Proc_INCREMENT_ID("I").FirstOrDefault();
            ViewBag.REF = db.Proc_INCREMENT_ID("R").FirstOrDefault();
            ViewBag.product = new SelectList(db.Products.ToList(), "PId", "pdesc");
            //ViewBag.code = new SelectList(db.mdcodes.Where(x => x.Code == 1).ToList(), "Id", "Name");
            return View();
        }

        // POST: GRN/Create
        [HttpPost]
        public ActionResult Create(tblStockIn stc)
        {
            try
            {

                //var pcod = db.tblStockIns.Where(x => x.id == stc.id).FirstOrDefault();
                int pr = Convert.ToInt32(stc.pcode);

                var pd = db.Products.Where(x => x.PId == pr).FirstOrDefault();

                //int vid = Convert.ToInt32(pd.Vendor_Id);

                //var ven = db.tblVendors.Where(x => x.id == vid).FirstOrDefault();

                //tblStockIn st = new tblStockIn();
                //{
                //    //st.refno = stc.refno;
                //    //st.pcode = pd.pcode;
                //    //st.qty = stc.qty;
                //    //st.sdate = DateTime.Now;
                //    //st.stockinby = "In";
                //    //st.status = "1";
                //    //st.vendor = ven.vendor;
                //    //st.InvoiceNo = stc.InvoiceNo;
                //    //st.Description = stc.Description;







                //    st.DueDate = stc.DueDate;
                //    st.PaidAmount = stc.PaidAmount;
                //    st.IsFullPayed = stc.IsFullPayed;
                //    st.FreeQty = stc.FreeQty;
                //    db.tblStockIns.Add(st);
                //    db.SaveChanges();



                //    //db.Proc_QTY_Update(pr, pqty);


                //}
                int cost = Convert.ToInt32(pd.Cost);
                //TotalCost = stc.qty * Convert.ToInt32(pd.Cost);
                int UnitPrice = Convert.ToInt32(pd.price);
                int OurPrice = Convert.ToInt32(stc.OurPrice);

                int LoyalityPrice =0;
                //int pqty = Convert.ToInt32(stc.qty);
                //int code = Convert.ToInt32(pd.pcode);
                int qty = Convert.ToInt32(stc.qty);
                int discoun = Convert.ToInt32(stc.Discounts);
                int ntot = Convert.ToInt32(stc.NetTotal);
                int pamt = Convert.ToInt32(stc.PaidAmount);
                string payed = stc.IsFullPayed.ToString();
                int fqty = Convert.ToInt32(stc.FreeQty);


                db.Proc_QTY_Update(stc.refno, pd.pcode, qty, null, stc.InvoiceNo, stc.Description, cost, UnitPrice, OurPrice, LoyalityPrice, stc.ExpireDate, 0, discoun, ntot, null, null, null, stc.DueDate, pamt, payed, fqty, pd.PId);
                db.Proc_INCREMENT_ID_Update("R");
                db.Proc_INCREMENT_ID_Update("I");





            }

            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

            }
            return RedirectToAction("Index");
        }

        // GET: GRN/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GRN/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: GRN/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GRN/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
