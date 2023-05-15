using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeBankApplication.Models;
using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Web.Security;
using System.Collections.ObjectModel;
using System.Net;
using System.Configuration;

namespace WeBankApplication.Controllers
{
    public class RegistrationController : Controller
    {
        WeBankEntities12 entity = new WeBankEntities12();

        // GET: Registration
        public ActionResult Login()
        {
            return View();
        }


        public ActionResult Signup()
        {
            return View();
        }

       

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewMode pass)
        {
            var changePwd = entity.User_Register.FirstOrDefault(x => x.Email == pass.Email);
            if (changePwd != null)
            {
                changePwd.Passcode = pass.NewPassword;
            }
            entity.User_Register.Attach(changePwd);
            entity.Entry(changePwd).State = EntityState.Modified;
            entity.SaveChanges();

            return RedirectToAction("Login", "Registration");
            
            TempData["SuccessMessage"] = "Password changed successfully.";
        }


        [HttpPost]
        public ActionResult Login(LoginViewModel credentials)
        {
            bool userExists = entity.User_Register.Any(x => x.Email == credentials.Email && x.Passcode == credentials.Password);
            User_Register u = entity.User_Register.FirstOrDefault(x => x.Email == credentials.Email && x.Passcode == credentials.Password);

            string message = string.Empty;

            if (userExists)
            {
                FormsAuthentication.SetAuthCookie(u.Username, false);
                Session["Id"] = credentials.Email.ToString();
                
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Username or Password is wrong");
           
            return View();
        }

        [HttpPost]
        public ActionResult Signup(User_Register userinfo)
        {
            if (entity.User_Register.Any(x => x.Username == userinfo.Username))
            {
               
                ViewBag.Notification = "This Account has already existed";
                return View();
            }
            else
            {
                entity.User_Register.Add(userinfo);
                entity.SaveChanges();
                Session["Username"] = userinfo.Username.ToString();
                Session["Email"] = userinfo.Email.ToString();
                TempData["AlertMessage"] = "Congratulations, Registered Successfully...!";
               
                return RedirectToAction("Login");
                return View();
                //return RedirectToAction("Index", "Home"); 
            }
        }

        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }

        public ActionResult Index(string searching)
        {
            return View(entity.User_Register.Where(x => x.Username.Contains(searching) || searching == null));
            //return View(db.accounts.ToList());
        }
     

        // GET: User_Register/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Register user_Register = entity.User_Register.Find(id);
            if (user_Register == null)
            {
                return HttpNotFound();
            }
            return View(user_Register);
        }
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Register user_Register = entity.User_Register.Find(id);
            if (user_Register == null)
            {
                return HttpNotFound();
            }
            return View(user_Register);
        }

        // POST: User_Register/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Username,Email,Passcode")] User_Register user_Register)
        {
            if (ModelState.IsValid)
            {
                entity.Entry(user_Register).State = EntityState.Modified;
                entity.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user_Register);
        }

        // GET: User_Register/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Register user_Register = entity.User_Register.Find(id);
            if (user_Register == null)
            {
                return HttpNotFound();
            }
            return View(user_Register);
        }

        // POST: User_Register/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            User_Register user_Register = entity.User_Register.Find(id);
            entity.User_Register.Remove(user_Register);
            entity.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                entity.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}