using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Farm_Management.Models;
using System.Web.Security;
using System.Data.Entity.Validation;

namespace Farm_Management.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
        FARMEntities db = new FARMEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Member_View mv)
        {
            try
            {

                //string userName = System.Web.HttpContext.Current.Session["User_Name"].ToString();

                Login lg = new Login();
                {
                    lg.User_Name = mv.Name;
                    lg.Password = mv.password;
                    lg.Confirm_Password = mv.Confirmpassword;
                    lg.Status = 0;
                    lg.Role = "User";
                    lg.Enter_Date = DateTime.Now;
                    lg.Enter_User = "User";


                    db.Logins.Add(lg);
                    db.SaveChanges();

                   

                }
                Member me = new Member();
                {
                    me.Name = mv.Name;
                    me.Bod = mv.Bod;
                    me.District = mv.District;
                    me.Telephone_No = mv.Telephone_No;
                    me.Email = mv.Email;
                    me.Status = 0;
                    me.Entered_User = mv.Name;
                    me.Entered_Date = mv.Entered_Date;
                   


                    db.Members.Add(me);
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

            //return RedirectToAction("Index");
            return RedirectToAction("Log_in", "Home", new { area = "" });
        }


    }
}