using System.Linq;
using System.Web.Mvc;
using Test.Models;
using System.Web.Security;
using System;

namespace Test.Controllers
{ 
    [AllowAnonymous]
    public class AccountController : Controller
    {
         PatientMangementDBContext db = new PatientMangementDBContext();

        [HttpGet]
        public ActionResult Login()
        { 
            return View();
          

        }

        [HttpPost]
        public ActionResult LoginPost(User model)
        {
            
            
                bool isValid = db.Users.Any(x => x.Username == model.Username && x.Password == model.Password);
                if (isValid)
                {
                    User prd = db.Users.Single(x => x.Username == model.Username);
                   
                    FormsAuthentication.SetAuthCookie(prd.ID.ToString(), false);
                    
                    if (prd.Role == "user")
                    {
                        return RedirectToAction("Index", "Patient");
                    }
                    else
                    {
                        return RedirectToAction("DoctorDashBoard", "Patient");
                    }
                    
                }
                ModelState.AddModelError("PROGRAM_ID", "Invalid UserName and Password");
                return View("Login");

        }

            // GET: Account
            public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ActionName("SignUp")]
        public ActionResult SignUpPost(User us)
        {
            if (ModelState.IsValid)
            {

                User user = new User();
                user.Name = us.Name;
                user.Role = "user";
                user.Username = us.Username;
                user.Password = us.Password;
                user.Gender = us.Gender;
                user.ContactNum = us.ContactNum;

                db.Users.Add(user).ToString();
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            else
            {
                return View("SignUp");
            }

        }
    
    public ActionResult Logout()
    {
        FormsAuthentication.SignOut();
        return RedirectToAction("Login");
    }
        public ActionResult About()
        {
            return View();
        }



    }
}
