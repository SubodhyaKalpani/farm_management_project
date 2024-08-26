using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Farm_Management.Models;

namespace Farm_Management.Controllers
{
    public class NakathController : Controller
    {
        // GET: Nakath
        FARMEntities db = new FARMEntities();
        public ActionResult Index()
        {
            List<NalathView> Bedt = new List<NalathView>();


            List<NAKATH> OList = db.NAKATHs.ToList();
            foreach (var DbData in OList)
            {
                string stat = "";
                if(DbData.STATUS==0)
                {
                    stat = "Active";
                }
                else
                {
                    stat = "Inactive";
                }

                NalathView Wareh = new NalathView()
                {
                    ID = DbData.ID,
                    YEAR = DbData.YEAR,
                    MONTH = DbData.MONTH,
                    STATUS = stat
                    

                };
                Bedt.Add(Wareh);
            }
            return View(Bedt);
        }

        // GET: Nakath/Details/5
        public ActionResult Details(int id)
        {
            return View(db.NAKATHs.Where(x=>x.ID == id).FirstOrDefault());
        }

        // GET: Nakath/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Nakath/Create
        [HttpPost]
        public ActionResult Create(NalathView stc)
        {
            try
            {
                string fileanme = Path.GetFileNameWithoutExtension(stc.Img_File.FileName);
                string extension = Path.GetExtension(stc.Img_File.FileName);
                fileanme = fileanme + DateTime.Now.ToString("yymmssfff") + extension;
                stc.Img_path = "~/VideoFile/" + fileanme;
                fileanme = Path.Combine(Server.MapPath("~/VideoFile/"), fileanme);
                stc.Img_File.SaveAs(fileanme);

                NAKATH st = new NAKATH();
                {
                    st.YEAR = stc.YEAR;
                    st.MONTH = stc.MONTH;
                    st.DATE = System.DateTime.Now;
                    
                    st.IMG_PATH = stc.Img_path;
                    st.STATUS = 0;


                    db.NAKATHs.Add(st);
                    db.SaveChanges();

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Nakath/Edit/5
        public ActionResult Edit(int id)
        {
            List<NalathView> Bedt = new List<NalathView>();


            NAKATH OList = db.NAKATHs.Where(x => x.ID == id).FirstOrDefault();
           

                NalathView Wareh = new NalathView()
                {
                    ID = OList.ID,
                    YEAR = OList.YEAR,
                    MONTH = OList.MONTH,
                    


                };
                Bedt.Add(Wareh);
           
            return View(Bedt);


           // return View(db.NAKATHs.Where(x=>x.ID==id).FirstOrDefault());
        }

        // POST: Nakath/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, NalathView stc)
        {
            try
            {
                string fileanme = Path.GetFileNameWithoutExtension(stc.Img_File.FileName);
                string extension = Path.GetExtension(stc.Img_File.FileName);
                fileanme = fileanme + DateTime.Now.ToString("yymmssfff") + extension;
                stc.Img_path = "~/VideoFile/" + fileanme;
                fileanme = Path.Combine(Server.MapPath("~/VideoFile/"), fileanme);
                stc.Img_File.SaveAs(fileanme);

                NAKATH st = db.NAKATHs.Where(x => x.ID == id).FirstOrDefault();
                {

                    st.YEAR = stc.YEAR;
                    st.MONTH = stc.MONTH;
                    st.IMG_PATH = stc.Img_path;
                   

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

        // GET: Nakath/Delete/5
        public ActionResult Delete(int id)
        {
            NAKATH st = db.NAKATHs.Where(x => x.ID == id).FirstOrDefault();
            {

                st.STATUS = 1;


                db.Entry(st).State = EntityState.Modified;
                db.SaveChanges();

            }

            return RedirectToAction("Index");
        }

        // POST: Nakath/Delete/5
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

        public ActionResult Profiles(string searchString)
        {
            DateTime myDateTime = DateTime.Now;
            string year = myDateTime.Year.ToString();

            int ye = Convert.ToInt32(year);
            ViewBag.Mon = new SelectList(db.NAKATHs.Where(x=>x.YEAR ==ye).ToList(), "MONTH", "MONTH");
            if (searchString != null)
            {
                int ser = Convert.ToInt32(searchString);
                return View(db.NAKATHs.Where(x => x.YEAR == ye && x.MONTH == ser).ToList());
            }
            else
            {
                return View(db.NAKATHs.Where(x => x.YEAR == ye).ToList());
            }
          
        }
    }
}
