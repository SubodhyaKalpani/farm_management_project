using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Farm_Management.Models;

namespace Farm_Management.Controllers
{
    public class ProductDetailsController : Controller
    {
        // GET: ProductDetails
        FARMEntities db = new FARMEntities();
        public ActionResult Index()
        {
            List<ProductDetailsL> Bedt = new List<ProductDetailsL>();


            List<PRODUCT_DETAILS> OList = db.PRODUCT_DETAILS.ToList();
            foreach (var DbData in OList)
            {


                ProductDetailsL Wareh = new ProductDetailsL()
                {
                    PD_ID = DbData.PD_ID,
                    PRODUCT = db.Products.Where(x => x.PId == DbData.P_ID).FirstOrDefault().pdesc
                    

                };
                Bedt.Add(Wareh);
            }
            return View(Bedt);

           // return View(db.PRODUCT_DETAILS.ToList());
        }

        // GET: ProductDetails/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductDetails/Create
        public ActionResult Create()
        {
            ViewBag.Product = new SelectList(db.Products.ToList(), "PId", "pdesc");
            return View();
        }

        // POST: ProductDetails/Create
        [HttpPost]
        public ActionResult Create(PRODUCT_DETAILS stc)
        {
            try
            {
                var havepr = db.PRODUCT_DETAILS.Where(x => x.P_ID == stc.P_ID).FirstOrDefault();

                if(havepr != null)
                {
                    PRODUCT_DETAILS stc1 = db.PRODUCT_DETAILS.Where(x => x.P_ID == stc.P_ID).FirstOrDefault();
                    db.PRODUCT_DETAILS.Remove(stc1);
                    db.SaveChanges();
                }
                
                PRODUCT_DETAILS st = new PRODUCT_DETAILS();
                {
                    st.P_ID = stc.P_ID;
                    st.DESCR = stc.DESCR;
                    st.DATE = System.DateTime.Now;
                   
                    db.PRODUCT_DETAILS.Add(st);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductDetails/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Product = new SelectList(db.Products.ToList(), "PId", "pdesc");
            return View(db.PRODUCT_DETAILS.Where(x=>x.PD_ID == id).FirstOrDefault());
        }

        // POST: ProductDetails/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, PRODUCT_DETAILS pd)
        {
            try
            {
                PRODUCT_DETAILS st = db.PRODUCT_DETAILS.Where(x => x.P_ID == id).FirstOrDefault();
                {

                    st.DESCR = pd.DESCR;
                    

                    db.Entry(st).State = EntityState.Modified;
                    db.SaveChanges();

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductDetails/Delete/5
        public ActionResult Delete(int id)
        {
            PRODUCT_DETAILS stc1 = db.PRODUCT_DETAILS.Where(x => x.P_ID == id).FirstOrDefault();
            db.PRODUCT_DETAILS.Remove(stc1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: ProductDetails/Delete/5
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
