using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Test.Models;

namespace Test.Controllers
{
    
    public class PatientController : Controller
    {

        PatientMangementDBContext db = new PatientMangementDBContext();

        // GET: Patient   
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        [HttpGet]
        public ActionResult BookAppointment(User us)
        {
            //var sql = @"SELECT Users.ID as Value, 
            //Users.Name AS Text
            //FROM [User] AS Users
            //JOIN Appointment ON Users.ID = Appointment.DoctorID
            //Order by Users.Name";
            var sql = @"SELECT Users.ID as Value, 
            Users.Name AS Text
            FROM [User] AS Users WHERE Users.Role='doctor'
            Order by Users.Name";
            var kk = db.Database.SqlQuery<QueryResults>(sql).ToList();
            ViewBag.DoctorID = new SelectList(kk,"Value","Text");
            return View();

        }

        [HttpPost]
        [ActionName("BookAppointment")]
        public ActionResult BookAppointmentPost(Appointment appointment)
        {
            //[Bind(Include = "PatientID,Date,StartTime,EndTime,ContactNum,Address,Disease,Advice,Reply,Fees")]
                string cookieName = FormsAuthentication.FormsCookieName; //Find cookie name
                HttpCookie authCookie = HttpContext.Request.Cookies[cookieName]; //Get the cookie by it's name
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value); //Decrypt it
                string UserName = ticket.Name; //You have the UserName!

                
            if (ModelState.IsValid)
            {
                Appointment Appointment = new Appointment();
                Appointment.PatientID = int.Parse(UserName);
                Appointment.DoctorID = appointment.DoctorID;
                Appointment.Date = appointment.Date;
                Appointment.StartTime = appointment.StartTime;
                Appointment.EndTime = appointment.EndTime;
                Appointment.ContactNumber = appointment.ContactNumber;
                Appointment.Address = appointment.Address;
                Appointment.Disease = appointment.Disease;
                Appointment.Advice = appointment.Advice;
                db.Appointments.Add(Appointment);
              
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(appointment);
            }
        }

        [HttpGet]
        public ActionResult DoctorDashBoard()
        {
            PatientMangementDBContext db = new PatientMangementDBContext();
            string cookieName = FormsAuthentication.FormsCookieName; //Find cookie name
            HttpCookie authCookie = HttpContext.Request.Cookies[cookieName]; //Get the cookie by it's name
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value); //Decrypt it
            string UserName = ticket.Name; //You have the UserName!

            var selectedPatient = db.Appointments.Where(d => d.DoctorID.ToString() == UserName).OrderByDescending(a => a.AppointmentID).ToList();
            return View(selectedPatient);
        }
        [HttpPost]
        [ActionName("DoctorDashBoard")]
        public ActionResult DoctorDashBoardPost()
        {
         
            return View();
        }

        // GET: Patient/Details/5
        public ActionResult TreatmentHistory()
        {
            PatientMangementDBContext db = new PatientMangementDBContext();
            string cookieName = FormsAuthentication.FormsCookieName; //Find cookie name
            HttpCookie authCookie = HttpContext.Request.Cookies[cookieName]; //Get the cookie by it's name
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value); //Decrypt it
            string UserName = ticket.Name; //You have the UserName!

            var selectedPatient = db.Appointments.Where(d => d.PatientID.ToString() == UserName).OrderByDescending(a => a.AppointmentID).ToList();
            return View(selectedPatient);
        }
        //[HttpGet]
        //public ActionResult Reply()
        //{
        //    PatientMangementDBContext db = new PatientMangementDBContext();
        //    db.Appointments.ToList();
        //    return View();


        //}
        //[HttpPost]
        //[ActionName("Reply")]
        //public ActionResult ReplyPost([Bind(Include = "PatientID,Date,StartTime,EndTime,ContactNum,Address,Disease,Advice,Reply")] Appointment appointment)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(appointment).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(appointment);
        //}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.PatientID = new SelectList(db.Users, "ID", "Name", appointment.PatientID);
            ViewBag.DoctorID = new SelectList(db.Users, "ID", "Name", appointment.DoctorID);
            return View(appointment);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppointmentID,PatientID,DoctorID,Date,StartTime,EndTime,ContactNumber,Address,Disease,Advice,Reply,Fees")]  Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                //
                Appointment Appointment = new Appointment();
               
                Appointment.Reply = appointment.Reply;
                Appointment.Fees = appointment.Fees;

                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DoctorDashBoard");
            }
            ViewBag.PatientID = new SelectList(db.Users, "ID", "Name", appointment.PatientID);
            ViewBag.DoctorID = new SelectList(db.Users, "ID", "Name", appointment.DoctorID);
            return View(appointment);
        }

        public ActionResult FeesAnalysis(Appointment appointment)
        {
           
            return View();
        }

        public ActionResult Reschedule(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            var originalArticle = db.Appointments.Where(a => a.AppointmentID == id).First();
            originalArticle.Status = 1;

            db.Entry(originalArticle).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DoctorDashBoard");
            //return View();
        }
        public ActionResult Check(string area)
        {
            PatientMangementDBContext db = new PatientMangementDBContext();
            string cookieName = FormsAuthentication.FormsCookieName; //Find cookie name
            HttpCookie authCookie = HttpContext.Request.Cookies[cookieName]; //Get the cookie by it's name
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value); //Decrypt it
            string UserName = ticket.Name; //You have the UserName!
            if(UserName!=null)
            {
                User prd = db.Users.Single(x => x.ID.ToString() == UserName);

                if (prd.Role == "user")
                {
                    return RedirectToAction("Index", "Patient");
                }
                else
                {
                    return RedirectToAction("DoctorDashBoard", "Patient");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

    }
}

//public ActionResult ApproveComment(int id)
//{
//    foreach (var x in dBBlogReviewEntities.Comments.Where((y => y.ID == id)))
//    {
//        x.Status = "Approved";
//    }
//    dBBlogReviewEntities.SaveChanges();
//    return RedirectToAction("ShowComment");

//}



