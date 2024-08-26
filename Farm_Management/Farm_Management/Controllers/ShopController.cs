using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Farm_Management.Models;

namespace Farm_Management.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        FARMEntities db = new FARMEntities();
        string uname = System.Web.HttpContext.Current.Session["User_Name"].ToString();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SellView()
        {
            return View(db.tblCarts.Where(x => x.status == "0").ToList());
        }

        public ActionResult Details(string id)
        {
           


            List<tblCart> OList = db.tblCarts.Where(x => x.transno == id).ToList();
            foreach (var DbData in OList)
            {
                tblCart st = db.tblCarts.Where(x => x.transno == id).FirstOrDefault();
                {
                    st.status = "2";

                    db.Entry(st).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            var useremail = db.Members.Where(x => x.Name == uname).FirstOrDefault();
            var companyemailDetails = db.tblCompanies.FirstOrDefault();
            var transactiondetails = db.tblCarts.Where(x => x.cashier == uname).FirstOrDefault();
     

            string from = companyemailDetails.Email;
            string fromname = companyemailDetails.CompanyName;
            string from_password = companyemailDetails.Email_Password; //Mail Password: Kalpani@123
            string to = useremail.Email;
            string Toname = useremail.Name;
            string subject = "Delivery Success!!";
            string body = "Sucessfully!!!!"+transactiondetails.transno;

            var fromAddress = new MailAddress(from, fromname);
            var toAddress = new MailAddress(to, Toname);

         
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


            return RedirectToAction("SellView");
        }

        public ActionResult Delete(string id)
        {
            List<tblCart> OList = db.tblCarts.Where(x => x.transno == id).ToList();
            foreach (var DbData in OList)
            {
                tblCart st = db.tblCarts.Where(x => x.transno == id).FirstOrDefault();
                {
                    st.status = "3";

                    db.Entry(st).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }


            var useremail = db.Members.Where(x => x.Name == uname).FirstOrDefault();
            var companyemailDetails = db.tblCompanies.FirstOrDefault();
            var transactiondetails = db.tblCarts.Where(x => x.cashier == uname).FirstOrDefault();


            string from = companyemailDetails.Email;
            string fromname = companyemailDetails.CompanyName;
            string from_password = companyemailDetails.Email_Password; //Mail Password: Kalpani@123
            string to = useremail.Email;
            string Toname = useremail.Name;
            string subject = "Delivery Unsuccess!!";
            string body = "Unsucessfully!!!!" + transactiondetails.transno;

            var fromAddress = new MailAddress(from, fromname);
            var toAddress = new MailAddress(to, Toname);


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
            return RedirectToAction("SellView");
        }
    }
}