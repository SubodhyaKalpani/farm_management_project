using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Farm_Management.Models;
using System.Web.Security;
using System.Data.Entity.Validation;
using System.Net.Mail;
using System.Net;
using System.Data.Entity;
using System.Text;

namespace Farm_Management.Controllers
{
    public class HomeController : Controller
    {
        FARMEntities db = new FARMEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AdminIndex()
        {
            DateTime day = System.DateTime.Now;

            ViewBag.Officers = db.Staffs.Where(x => x.Status == 0).Count();
                ViewBag.Customers = db.Members.Where(x => x.Status == 0).Count();
            ViewBag.Sales = db.tblCarts.Where(x => x.sdate == day).Sum(x=>x.price);
            return View();
        }


        public ActionResult Log_In()
        {
           

            return View();
        }

        public ActionResult Sign_In()
        {


            return View();
        }

        public ActionResult CheckLogin(string username, string password)
        {
            int isLogged = 0;
            var user = db.Logins.Where(x => x.User_Name == username && x.Password == password && x.Status == 0).FirstOrDefault();
            
            if (user != null)
            {
                if(user.Role == "User")
                {
                    FormsAuthentication.SetAuthCookie(user.User_Name, false);
                    Session["User_Name"] = user.User_Name;
                    Session["Role"] = user.Role;
                    isLogged = 1;
                }
               else
                {
                    FormsAuthentication.SetAuthCookie(user.User_Name, false);
                    Session["User_Name"] = user.User_Name;
                    Session["Role"] = user.Role;
                    isLogged = 2;
                }

                // return RedirectToAction("Index", "Home");
            }
            else
            {
                isLogged = 0;
            }
            return Json(isLogged, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ForgetPassword()
        {


            return View();
        }


        public ActionResult CheckLoginS(string username)
        {

            bool save = true;
            try
            {
                string spassword = "";
                //string comp = System.Web.HttpContext.Current.Session["User_Name"].ToString();
                //int patient_id = db.PATIENTs.Where(x => x.P_NIC == comp).FirstOrDefault().P_ID;
                spassword = RandomPassword();

             

                Login st = db.Logins.Where(x => x.User_Name == username).FirstOrDefault();
                {

                    st.Password = spassword;
                    st.Confirm_Password = spassword;


                    db.Entry(st).State = EntityState.Modified;
                    db.SaveChanges();

                }

                var patientEmail = db.Members.Where(x => x.Name == username).FirstOrDefault();
                var companyemailDetails = db.tblCompanies.FirstOrDefault();
                try
                {
                    string from = companyemailDetails.Email;
                    string fromname = companyemailDetails.CompanyName;
                    string from_password = companyemailDetails.Email_Password;
                    string to = patientEmail.Email;
                    string Toname = patientEmail.Name;
                    string subject = "Login Password for Farm Management System";
                    string body = "You can use this Password to Loging. Your Password is - " + spassword;

                    //var fromAddress = new MailAddress(from, Toname);
                    //var toAddress = new MailAddress(to, "To Name");

                    var fromAddress = new MailAddress(from, fromname);
                    var toAddress = new MailAddress(to, Toname);


                    //using (SmtpClient smtpClient = new SmtpClient("localhost", 465))
                    //{ // <-- note the use of localhost
                    //    NetworkCredential creds = new NetworkCredential(from, from_password);
                    //    smtpClient.Credentials = creds;
                    //    MailMessage msg = new MailMessage(to, to, "Test", body);
                    //    smtpClient.Send(msg);
                    //}
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAddress.Address, from_password)
                    };
                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(message);
                    }

                    save = true;
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
                    save = false;
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
                save = false;
            }


            return Json(save, JsonRequestBehavior.AllowGet);
        }

     

        public string RandomPassword(int size = 0)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }
        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
    }
}