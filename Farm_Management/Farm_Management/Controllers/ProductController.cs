using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Farm_Management.Models;

namespace Farm_Management.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        FARMEntities db = new FARMEntities();
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.PROD = db.Proc_INCREMENT_ID("P").FirstOrDefault();
            ViewBag.Province = new SelectList(db.PROVINCEs.ToList(), "P_NAME", "P_NAME");
            ViewBag.ProductType = new SelectList(db.PRODUCT_TYPE.ToList(), "P_NAME", "P_NAME");
            ViewBag.Category = new SelectList(db.Categories.ToList(), "Id", "Category1");
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductView stc)
        {
            try
            {

                string fileanme = Path.GetFileNameWithoutExtension(stc.Img_File.FileName);
                string extension = Path.GetExtension(stc.Img_File.FileName);
                fileanme = fileanme + DateTime.Now.ToString("yymmssfff") + extension;
                stc.Img_path = "~/VideoFile/" + fileanme;
                fileanme = Path.Combine(Server.MapPath("~/VideoFile/"), fileanme);
                stc.Img_File.SaveAs(fileanme);

                Product st = new Product();
                {
                    st.pcode = stc.pcode;
                    st.pdesc = stc.pdesc;
                    st.price = 0;
                    st.qty = 0;
                    st.reorder = stc.reorder;
                    st.Cost =0;
                    st.Province = stc.Province;
                    st.Product_Type = stc.Product_Type;
                    st.Img_Path = stc.Img_path;
                    st.Category_Id = stc.Category_Id;


                    db.Products.Add(st);
                    db.SaveChanges();



                  


                }

                db.Proc_INCREMENT_ID_Update("P");
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

        public ActionResult Edit(int id)
        {
            ViewBag.Province = new SelectList(db.PROVINCEs.ToList(), "P_NAME", "P_NAME");
            ViewBag.ProductType = new SelectList(db.PRODUCT_TYPE.ToList(), "P_NAME", "P_NAME");
            ViewBag.Category = new SelectList(db.Categories.ToList(), "Id", "Category1");
            return View(db.Products.Where(x => x.PId == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Edit(int id, Product pd)
        {
            try
            {
                Product st = db.Products.Where(x => x.PId == id).FirstOrDefault();
                {

                    st.pdesc = pd.pdesc;
                    st.price = pd.price;
                    st.qty = pd.qty;
                    st.reorder = pd.reorder;
                    st.Cost = pd.Cost;
                    st.Province = pd.Province;
                    st.Product_Type = pd.Product_Type;
                    st.Category_Id = pd.Category_Id;

                    db.Entry(st).State = EntityState.Modified;
                    db.SaveChanges();

                }
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

        public ActionResult Delete(int id)
        {
            Product stc1 = db.Products.Where(x => x.PId == id).FirstOrDefault();
            db.Products.Remove(stc1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ProductSellView(string searchString)
        {
            ViewBag.Cat  = new SelectList(db.Categories.ToList(), "Id", "Category1");

            if(searchString != null)
            {
                int search = Convert.ToInt32(searchString);

                return View(db.Products.Where(x=>x.Category_Id == search && x.qty>0).ToList());
            }
            else
            {
                return View(db.Products.Where(x=> x.qty > 0).ToList());
            }
            
        }

        public ActionResult ProductSave(int id, Product pd)
        {
            var qtys = db.Products.Where(x => x.PId == id).FirstOrDefault();
                var inv = db.Proc_INCREMENT_ID("B").FirstOrDefault();
            try
            {
                
                if (qtys != null)
                {

                    //var duplicateitemid = db.tmpcarts.Where(x => x.itm_id == pd.pcode).FirstOrDefault();
                   



                        tmpcart lg = new tmpcart();
                    {
                        lg.itm_id = qtys.pcode;
                        lg.Inv_No = inv;
                        lg.item =qtys.pdesc;
                        //lg.category = category;
                        lg.price = Convert.ToInt32(qtys.price);
                        lg.qty = 0;
                        lg.total = Convert.ToInt32(qtys.price)* Convert.ToInt32(pd.qty);
                        lg.user_name = System.Web.HttpContext.Current.Session["User_Name"].ToString();
                        lg.status = 0;
                        lg.img_path = qtys.Img_Path;

                        db.tmpcarts.Add(lg);
                        db.SaveChanges();

                    }
                   


                }
                else
                {
                   
                }
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
            return RedirectToAction("QtyView", new { id = inv });
        }

        public ActionResult Invoice()
        {
            ViewBag.INV = db.Proc_INCREMENT_ID("B").FirstOrDefault();

            string uname = System.Web.HttpContext.Current.Session["User_Name"].ToString();

            var usernameinvoice = db.tmpcarts.Where(x => x.user_name == uname).FirstOrDefault();

            if(usernameinvoice != null)
            {
                return View(db.tmpcarts.Where(x=>x.user_name ==uname).ToList());
            }
            else
            {
                return View(db.tmpcarts.ToList());
            }
            
        }

      

        public ActionResult ProductDetails(int id)
        {
           return View(db.V_Product_Det.Where(x => x.PId == id).FirstOrDefault());
         
        }

        public ActionResult QtyView(string id)
        {

            return View(db.tmpcarts.Where(x=>x.Inv_No ==id).FirstOrDefault());
        }

        public ActionResult ProductQtySave( tmpcart pd)
        {
           
            try
            {
                if(pd.qty>0)
                {
                    tmpcart st = db.tmpcarts.Where(x => x.Inv_No == pd.Inv_No).FirstOrDefault();
                    {

                        st.qty = pd.qty;
                        st.total = Convert.ToInt32(st.price) * Convert.ToInt32(pd.qty);

                        db.Entry(st).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Invoice");
                    }
                }
                else
                {
                    return RedirectToAction("QtyView", new { id = pd.Inv_No });
                }

                    



               
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
            return RedirectToAction("Invoice");
        }
        public ActionResult Deleteinv(int id, tmpcart tmp)
        {
            //bool save = false;
            try
            {
                tmpcart tmp1 = db.tmpcarts.Where(x => x.id == id).FirstOrDefault();
                db.tmpcarts.Remove(tmp1);
                db.SaveChanges();
                //save = true;

                return RedirectToAction("Invoice");
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
                //save = false;
            }


            return View();
        }
        [HttpPost]
        public JsonResult ProcessInv(string invno, decimal subtots = 0)
        {
            bool save = false;
            try
            {
                string unam = System.Web.HttpContext.Current.Session["User_Name"].ToString();
                List<tblCart> tca = db.tblCarts.Where(x => x.cashier == unam).ToList();
                int dis = 0;
                var usermna = db.tblCarts.Where(x => x.cashier == unam).FirstOrDefault();
                if (usermna != null)
                {
                    List<tmpcart> tmpca1 = db.tmpcarts.Where(x => x.Inv_No == invno).ToList();
                    if (subtots > 0)
                    {



                        foreach (var item in tmpca1)
                        {
                            var it = db.tmpcarts.Where(x => x.Inv_No == invno).FirstOrDefault();

                            //int iid = Convert.ToInt32(it.itm_id);

                            var itmcode = db.Products.Where(x => x.pcode == it.itm_id).FirstOrDefault();
                            tblCart crt = new tblCart();
                            {
                                crt.transno = item.Inv_No;
                                crt.pcode = itmcode.pcode;
                                crt.price = itmcode.price;
                                crt.ourprice = 0;
                                crt.qty = item.qty;
                                crt.Linedisc = item.id;
                                crt.total = 0;
                                crt.sdate = System.DateTime.Now;
                                crt.status = "0";
                                crt.Description = null;
                                crt.cashier = System.Web.HttpContext.Current.Session["User_Name"].ToString();
                                crt.GrossTotalDiscount = 0;
                                crt.HiddenOurPrice = 0;
                                crt.LoyalityPrice = 0;
                                crt.PaymentMethod = null;
                                crt.CashAmount = 0;
                                crt.CreditCardAmount = null;
                                crt.CreditCardInterest = null;
                                crt.CreditAmount = null;
                                crt.PayingAmount = 0;
                                crt.Balance = 0;
                                crt.NetTotal = null;
                                crt.LineDiscPer = null;
                                crt.LineDiscAmt = null;
                                crt.TotalBillDiscPer = null;
                                crt.TotalBillDiscAmount = null;
                                crt.TotalBillDiscPerAmount = null;
                                crt.BillPrice = item.qty * itmcode.price;
                                crt.SinhalaDesc = null;
                                crt.DifferentDisc = 0 - 0;
                                crt.PrintLanguage = null;
                                crt.ItemCount = null;
                                crt.Time = null;
                                crt.TotalDiscounts = 0;
                                crt.Counter = "1";
                                crt.Cost = itmcode.Cost;
                                crt.TotalCost = item.qty * itmcode.Cost;

                                db.tblCarts.Add(crt);
                                db.SaveChanges();

                            }

                            Product prduct = db.Products.Where(x => x.pcode == it.itm_id).FirstOrDefault();
                            {

                                prduct.qty = prduct.qty - it.qty;
                                db.Entry(prduct).State = EntityState.Modified;
                                db.SaveChanges();

                            }

                            tmpcart tcrt1 = db.tmpcarts.Where(x => x.Inv_No == invno).FirstOrDefault();
                            db.tmpcarts.Remove(tcrt1);
                            db.SaveChanges();


                        }
                      
                        db.Proc_INCREMENT_ID_Update("B");
                        save = true;

                        //save = true;
                    }
                }
                

            }

            catch (Exception ex)
            {
                throw (ex);
                save = false;
            }


            return Json(save, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProductDet(string searchString)
        {
            ViewBag.Cat = new SelectList(db.Categories.ToList(), "Id", "Category1");

            if (searchString != null)
            {
                int search = Convert.ToInt32(searchString);
                return View(db.Products.Where(x => x.Category_Id == search).ToList());
            }

            else
            {
                return View(db.Products.ToList());
            }

        }
    }
}