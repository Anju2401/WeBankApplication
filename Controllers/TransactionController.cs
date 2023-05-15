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
    public class TransactionController : Controller
    {
        private WeBankEntities12 db = new WeBankEntities12();

        // GET: Transaction
        public ActionResult Index(string searching,string searchtransactiontype)
        {
            var transactions = db.Transactions.Include(t => t.ACCOUNT_HOLDER_DETAILS);
            return View(db.Transactions.Where(x => x.account_number.Contains(searching) || searching == null));
            //return View(transactions.ToList());

            //List<Transaction> newtran = db.Transactions.ToList();
            //return View(db.Transactions.Where(x => x.Transaction_Type.StartsWith(searchtransactiontype) || searchtransactiontype == null).ToList());
        }

        // GET: Transaction/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transaction/Create
        public ActionResult Create()
        {
            ViewBag.AccID = new SelectList(db.ACCOUNT_HOLDER_DETAILS, "ID", "account_number");
            return View();
        }

        // POST: Transaction/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string btn,[Bind(Include = "ID,account_number,amount,Transaction_Date,Transaction_Type,AccID")] Transaction transaction)
        {
           // ViewBag.account_number = ACCOUNT_HOLDER_DETAILS.Getaccount_number(HttpContext.User);
            int num = 1;
            if (btn == "Withdraw")
            {
                var data = db.Transactions.Where(account1 => account1.account_number == transaction.account_number).FirstOrDefault();
                if (transaction.amount <= data.amount)
                {
                    data.amount -= transaction.amount;
                    int msg = db.SaveChanges();
                    if (msg == 1)
                    {
                        //ViewBag.data = "Withdraw Done";
                        TempData["SuccessMessage"] = "Withdraw successful.";
                    }
                    else
                    {
                        TempData["SuccessMessage"] = "Withdraw Not successful.";
                    }


                }
                else
                {
                    //ViewBag.data = "Insufficent balance";
                    TempData["SuccessMessage"] = "Insufficent balance.";
                }

            }
            else if (btn == "Deposit")
            {
                var data = db.Transactions.Where(account1 => account1.account_number == transaction.account_number).FirstOrDefault();
                data.amount += transaction.amount;
                int msg = db.SaveChanges();
                if (msg == 1)
                {
                    //ViewBag.data = "Deposit Done";
                    TempData["SuccessMessage"] = "Deposit successful.";
                }
                else
                {
                    //ViewBag.data = "Deposit Not Done";
                    TempData["SuccessMessage"] = "Deposit Not successful.";
                }

            }
            else if (btn == "CheckBalance")
            {
                var data = db.Transactions.Where(account1 => account1.account_number == transaction.account_number).FirstOrDefault();

                ViewBag.CheckBalance = data.amount;
                num = 0;

            }


            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
               // return RedirectToAction("Index");
            }

            ViewBag.AccID = new SelectList(db.ACCOUNT_HOLDER_DETAILS, "ID", "account_number", transaction.AccID);
            return View(transaction);
        }

        // GET: Transaction/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccID = new SelectList(db.ACCOUNT_HOLDER_DETAILS, "ID", "account_number", transaction.AccID);
            return View(transaction);
        }

        // POST: Transaction/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,account_number,amount,Transaction_Date,Transaction_Type,AccID")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccID = new SelectList(db.ACCOUNT_HOLDER_DETAILS, "ID", "account_number", transaction.AccID);
            return View(transaction);
        }

        // GET: Transaction/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
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
