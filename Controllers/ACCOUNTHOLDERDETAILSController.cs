using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WeBankApplication.Models;

namespace WeBankApplication.Controllers
{
    public class ACCOUNTHOLDERDETAILSController : Controller
    {
        private WeBankEntities12 db = new WeBankEntities12();

        // GET: ACCOUNTHOLDERDETAILS
        public ActionResult Index(string searching)
        {

            var aCCOUNT_HOLDER_DETAILS = db.ACCOUNT_HOLDER_DETAILS.Include(a => a.User_Register);
            return View(db.ACCOUNT_HOLDER_DETAILS.Where(x => x.account_number.Contains(searching) || searching == null));
            //return View(aCCOUNT_HOLDER_DETAILS.ToList());
        }

        // GET: ACCOUNTHOLDERDETAILS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACCOUNT_HOLDER_DETAILS aCCOUNT_HOLDER_DETAILS = db.ACCOUNT_HOLDER_DETAILS.Find(id);
            if (aCCOUNT_HOLDER_DETAILS == null)
            {
                return HttpNotFound();
            }
            return View(aCCOUNT_HOLDER_DETAILS);
        }

        // GET: ACCOUNTHOLDERDETAILS/Create
        public ActionResult Create(int id = 0)
        {
            ACCOUNT_HOLDER_DETAILS cus = new ACCOUNT_HOLDER_DETAILS();
            var lastcustomer = db.ACCOUNT_HOLDER_DETAILS.OrderByDescending(x => x.ID).FirstOrDefault();
            if (id != 0)
            {
                cus = db.ACCOUNT_HOLDER_DETAILS.Where(x => x.ID == id).FirstOrDefault<ACCOUNT_HOLDER_DETAILS>();
            }
            else if (lastcustomer == null)
            {
                cus.account_number = "WEBAC00";

            }
            else

            {
                cus.account_number = "WEBAC00" + (Convert.ToInt32(lastcustomer.account_number.Substring(6, lastcustomer.account_number.Length - 6)) + 1).ToString("D3");
            }

           
            ViewBag.CusID = new SelectList(db.User_Register, "ID", "Username");
            return View(cus);
        }

        // POST: ACCOUNTHOLDERDETAILS/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,account_number,Password,Fullname,Fathername,Adharno,Email,Address,DOB,Age,accounttype,Branch,Registration_Date,Phoneno,CusID")] ACCOUNT_HOLDER_DETAILS aCCOUNT_HOLDER_DETAILS)
        {
            if (ModelState.IsValid)
            {
                db.ACCOUNT_HOLDER_DETAILS.Add(aCCOUNT_HOLDER_DETAILS);
                db.SaveChanges();
                // ViewBag.Message = "Successfully Opened The Account";
                TempData["SuccessMessage"] = "Successfully Opened The Account";
                //return RedirectToAction("Index");
            }

            ViewBag.CusID = new SelectList(db.User_Register, "ID", "Username", aCCOUNT_HOLDER_DETAILS.CusID);
            return View(aCCOUNT_HOLDER_DETAILS);
        }

        // GET: ACCOUNTHOLDERDETAILS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACCOUNT_HOLDER_DETAILS aCCOUNT_HOLDER_DETAILS = db.ACCOUNT_HOLDER_DETAILS.Find(id);
            if (aCCOUNT_HOLDER_DETAILS == null)
            {
                return HttpNotFound();
            }
            ViewBag.CusID = new SelectList(db.User_Register, "ID", "Username", aCCOUNT_HOLDER_DETAILS.CusID);
            return View(aCCOUNT_HOLDER_DETAILS);
        }

        // POST: ACCOUNTHOLDERDETAILS/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,account_number,Password,Fullname,Fathername,Adharno,Email,Address,DOB,Age,accounttype,Branch,Registration_Date,Phoneno,CusID")] ACCOUNT_HOLDER_DETAILS aCCOUNT_HOLDER_DETAILS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aCCOUNT_HOLDER_DETAILS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CusID = new SelectList(db.User_Register, "ID", "Username", aCCOUNT_HOLDER_DETAILS.CusID);
            return View(aCCOUNT_HOLDER_DETAILS);
        }

        // GET: ACCOUNTHOLDERDETAILS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACCOUNT_HOLDER_DETAILS aCCOUNT_HOLDER_DETAILS = db.ACCOUNT_HOLDER_DETAILS.Find(id);
            if (aCCOUNT_HOLDER_DETAILS == null)
            {
                return HttpNotFound();
            }
            return View(aCCOUNT_HOLDER_DETAILS);
        }

        // POST: ACCOUNTHOLDERDETAILS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ACCOUNT_HOLDER_DETAILS aCCOUNT_HOLDER_DETAILS = db.ACCOUNT_HOLDER_DETAILS.Find(id);
            db.ACCOUNT_HOLDER_DETAILS.Remove(aCCOUNT_HOLDER_DETAILS);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
