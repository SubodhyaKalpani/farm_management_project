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
    public class OfficerController : Controller
    {
        // GET: Officer
        FARMEntities db = new FARMEntities();
        public ActionResult Index()
        {
            return View(db.Staffs.ToList());
        }
        public ActionResult Create()
        {
            ViewBag.Province = new SelectList(db.PROVINCEs.ToList(), "ID", "P_NAME");
            return View();
        }
        [HttpPost]
        public ActionResult Create(StaffVM stg)
        {
            try
            {

                string fileanme = Path.GetFileNameWithoutExtension(stg.Img_File.FileName);
                string extension = Path.GetExtension(stg.Img_File.FileName);
                fileanme = fileanme + DateTime.Now.ToString("yymmssfff") + extension;
                stg.Img_path = "~/VideoFile/" + fileanme;
                fileanme = Path.Combine(Server.MapPath("~/VideoFile/"), fileanme);
                stg.Img_File.SaveAs(fileanme);

                Staff st = new Staff();
                {

                    st.Name = stg.Name;
                    st.Bod = stg.Bod;
                    st.Province = stg.Province;
                    st.Telephone_No = stg.Telephone_No;
                    st.Email = stg.Email;
                    st.Job_Position = stg.Job_Position;
                    st.Status = 0;
                    st.Entered_User = System.Web.HttpContext.Current.Session["User_Name"].ToString();
                    st.Entered_Date = System.DateTime.Now;
                    st.Pro_Pic = stg.Img_path;
                    db.Staffs.Add(st);
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

        public ActionResult Edit(int id)
        {
            ViewBag.Province = new SelectList(db.PROVINCEs.ToList(), "ID", "P_NAME");

            return View(db.Staffs.Where(x => x.Id == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int id, Staff cat)
        {
            try
            {
                Staff st = db.Staffs.Where(x => x.Id == id).FirstOrDefault();
                {

                    st.Name = cat.Name;
                    st.Bod = cat.Bod;
                    st.Province = cat.Province;
                    st.Telephone_No = cat.Telephone_No;
                    st.Email = cat.Email;
                    st.Job_Position = cat.Job_Position;
                    st.Modify_User = System.Web.HttpContext.Current.Session["User_Name"].ToString();
                    st.Modify_Date = System.DateTime.Now;

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
            Staff stc1 = db.Staffs.Where(x => x.Id == id).FirstOrDefault();
            db.Staffs.Remove(stc1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult OfficersView(string searchString)
        {
            ViewBag.Province = new SelectList(db.PROVINCEs.ToList(), "ID", "P_NAME");

            if (searchString != null)
            {
                int search = Convert.ToInt32(searchString);

                return View(db.Staffs.Where(x => x.Province == search).ToList());
            }
            else
            {
                return View(db.Staffs.ToList());
            }
           
        }
    }
}