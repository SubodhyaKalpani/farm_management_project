using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Farm_Management.Models;

namespace Farm_Management.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        FARMEntities db = new FARMEntities();
        public ActionResult Index()
        {
            
           
            return View(db.Categories.ToList());
        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(Category cat)
        {
            try
            {



                Category st = new Category();
                {


                    st.Category1 = cat.Category1;
                    db.Categories.Add(st);
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
        public ActionResult YIndex()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
           
            return View(db.Categories.Where(x => x.Id == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Edit(int id, Category cat)
        {
            try
            {
                Category st = db.Categories.Where(x => x.Id == id).FirstOrDefault();
                {

                    st.Category1 = cat.Category1;
                    
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
            Category stc1 = db.Categories.Where(x => x.Id == id).FirstOrDefault();
            db.Categories.Remove(stc1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}