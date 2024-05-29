using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteDatVeXeKhach08.Controllers
{
    public class AccountController : Controller
    {
        BanVeXeKhachEntities db = new BanVeXeKhachEntities();
        public ActionResult LogIn()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult LogIn(string username, string password)
        {
            User loggedInUser = db.Users.SingleOrDefault(u => u.Username == username && u.Password == password);
            if (loggedInUser.RoleID == 1)
            {
                Session["FullName"] = loggedInUser.FullName;
                return RedirectToAction("AdminView");
            }
            else if (loggedInUser.RoleID == 2)
            {
                Session["FullName"] = loggedInUser.FullName;
                return RedirectToAction("CustomerView");
            }
            else
            {
                return View();
            }
        }
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(User u)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(u.Username) || string.IsNullOrWhiteSpace(u.Password) ||
                   string.IsNullOrWhiteSpace(u.FullName) || string.IsNullOrWhiteSpace(u.Email) ||
                   string.IsNullOrWhiteSpace(u.Phone))
                {
                    ModelState.AddModelError("", "All fields are required.");
                    return View();
                }
                var existingUser = db.Users
                    .FirstOrDefault(s => s.Username == u.Username);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "Username already exists.");
                    return View();
                }
                u.RoleID = 2;
                db.Users.Add(u); 
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        public ActionResult Logout()
        {
            Session["FullName"] = null;
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login");
        }
        public ActionResult AdminView()
        {
            ViewBag.FullName = Session["FullName"];
            return View();
        }
        public ActionResult CustomerView()
        {
            ViewBag.FullName = Session["FullName"];
            return View();
        }
    }
}
