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
    public class Fund_TransferController : Controller
    {
        private WeBankEntities12 db = new WeBankEntities12();

        // GET: Fund_Transfer
        public ActionResult Index(string searching)
        {
            var fund_Transfer = db.Fund_Transfer.Include(f => f.Transaction);
            return View(db.Fund_Transfer.Where(x => x.Transaction_id.Contains(searching) || searching == null));
           // return View(fund_Transfer.ToList());
        }

        // GET: Fund_Transfer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fund_Transfer fund_Transfer = db.Fund_Transfer.Find(id);
            if (fund_Transfer == null)
            {
                return HttpNotFound();
            }
            return View(fund_Transfer);
        }

        // GET: Fund_Transfer/Create
        public ActionResult Create(int id = 0)
        {
            Fund_Transfer cus = new Fund_Transfer();
            var lastcustomer = db.Fund_Transfer.OrderByDescending(x => x.ID).FirstOrDefault();
            if (id != 0)
            {
                cus = db.Fund_Transfer.Where(x => x.ID == id).FirstOrDefault<Fund_Transfer>();
            }
            else if (lastcustomer == null)
            {
                cus.Transaction_id = "TIDW00";
            }
            else
            {
                cus.Transaction_id = "TIDW00" + (Convert.ToInt32(lastcustomer.Transaction_id.Substring(5, lastcustomer.Transaction_id.Length - 5)) + 1).ToString("D3");
            }


            ViewBag.TCCID = new SelectList(db.Transactions, "ID", "account_number");
            return View(cus);
        }

        // POST: Fund_Transfer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Transaction_id,account_number,To_account_number,amount,TCCID")] Fund_Transfer fund_Transfer)
        {
            var data = db.Transactions.Where(account1 => account1.account_number == fund_Transfer.account_number).FirstOrDefault();
            var data1 = db.Transactions.Where(account1 => account1.account_number == fund_Transfer.To_account_number).FirstOrDefault();
            if (fund_Transfer.amount <= data.amount)
            {
                data.amount -= fund_Transfer.amount;
                db.SaveChanges();
                if (data != data1)
                {
                    data1.amount += fund_Transfer.amount;
                    int m = db.SaveChanges();
                    if (m == 1)
                    {
                        TempData["SuccessMessage"] = "Transfer successful.";
                    }
                    else
                    {
                        TempData["SuccessMessage"] = "Transfer successful.";
                    }


                    if (ModelState.IsValid)
                    {
                        db.Fund_Transfer.Add(fund_Transfer);
                        db.SaveChanges();
                        //return RedirectToAction("Index");
                    }

                    ViewBag.TCCID = new SelectList(db.Transactions, "ID", "account_number", fund_Transfer.TCCID);
                    return View(fund_Transfer);
                }
            }

            return View(fund_Transfer);
        }

        // GET: Fund_Transfer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fund_Transfer fund_Transfer = db.Fund_Transfer.Find(id);
            if (fund_Transfer == null)
            {
                return HttpNotFound();
            }
            ViewBag.TCCID = new SelectList(db.Transactions, "ID", "account_number", fund_Transfer.TCCID);
            return View(fund_Transfer);
        }

        // POST: Fund_Transfer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Transaction_id,account_number,To_account_number,amount,TCCID")] Fund_Transfer fund_Transfer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fund_Transfer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TCCID = new SelectList(db.Transactions, "ID", "account_number", fund_Transfer.TCCID);
            return View(fund_Transfer);
        }

        // GET: Fund_Transfer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fund_Transfer fund_Transfer = db.Fund_Transfer.Find(id);
            if (fund_Transfer == null)
            {
                return HttpNotFound();
            }
            return View(fund_Transfer);
        }

        // POST: Fund_Transfer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fund_Transfer fund_Transfer = db.Fund_Transfer.Find(id);
            db.Fund_Transfer.Remove(fund_Transfer);
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
