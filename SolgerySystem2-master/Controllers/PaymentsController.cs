using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SolgerySystem2.Models;

namespace SolgerySystem2.Controllers
{
    [Authorize]
    [HandleError]
    public class PaymentsController : Controller
    {
        private UserGrpModel db = new UserGrpModel();

        [Authorize]
        // GET: Payments
        public ActionResult Index()
        {
            var payments1 = db.Payments1.Include(p => p.FromUsr).Include(p => p.Grp).Include(p => p.ToUsr);
            return View(payments1.ToList());
        }
        public ActionResult Display2(int id, int toUid)
        {
            //var payments1 = db.Payments1.Include(p => p.FromUsr).Include(p => p.Grp).Include(p => p.ToUsr).Where(g=> g.GrpId==id & g.ToUsrId==toUid & g.FromUsr.uname==User.Identity.Name );
            var payments1 = db.Payments1.Include(p => p.FromUsr).Include(p => p.Grp).Include(p => p.ToUsr);
            return View(payments1.ToList());
        }
        public ActionResult Index1()
        {
            int id = (int) TempData["id"];
            return RedirectToAction("Create",new { id = id});
        }
        public ActionResult Index2()
        {
            //int id = (int)TempData["id"];
            int id = (int)Session["id"];
            //int toUid = (int)TempData["toUid"];
            int toUid = (int)Session["toUid"];
            return RedirectToAction("Display2", new { id = id, toUid=toUid });
        }
        // GET: Payments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments1.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // GET: Payments/Create
        public ActionResult Create(int id)
        {
            ViewBag.FromUsrId = new SelectList(db.Users1.Where(g=>g.uname==User.Identity.Name), "UserId", "uname");
            ViewBag.GrpId = new SelectList((db.Groups1.GroupBy(g => g.GId).Select(g => g.FirstOrDefault())).Where(g=>g.GId==id), "GId", "GrpName");
            //ViewBag.ToUsrId = new SelectList(db.Groups1.Where(g=>g.GId==id), "UserId", "uname", payment.ToUsrId);
            ViewBag.toUser = db.Groups1.Where(g=>g.GId==id & g.Usr.uname!=User.Identity.Name).Select(g=>g);
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[Bind(Include = "PaymentId,narration,GrpId,amount,FromUsrId,ToUsrId,paid")] Payment payment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string[] contrylist)
        {
            var t = contrylist;
            ViewBag.temp = t;
            //return RedirectToAction("Debug");
            if (ModelState.IsValid)
            {
                
                var count=t.Count();
                foreach (var i in t )
                {
                    Payment p = new Payment();
                    //p.PaymentId = int.Parse(Request["PaymentId"]);
                    p.narration = Request["narration"];
                    p.GrpId = int.Parse(Request["GrpId"]);
                    p.amount = int.Parse(Request["amount"])/(count+1);
                    p.ToUsrId = int.Parse(i);
                    p.FromUsrId = int.Parse(Request["FromUsrId"]);
                    db.Payments1.Add(p);
                    db.SaveChanges();
                }
                return RedirectToAction("Details", "Groups", new { id = (int)Session["id"] });

            }

            /* ViewBag.FromUsrId = new SelectList(db.Users1, "UserId", "uname", payment.FromUsrId);
             ViewBag.GrpId = new SelectList(db.Groups1, "GroupId", "GrpName", payment.GrpId);
             ViewBag.ToUsrId = new SelectList(db.Users1, "UserId", "uname", payment.ToUsrId);
             return View(payment);*/
            return View();
        }

        // GET: Payments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments1.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.FromUsrId = new SelectList(db.Users1, "UserId", "uname", payment.FromUsrId);
            ViewBag.GrpId = new SelectList(db.Groups1, "GroupId", "GrpName", payment.GrpId);
            ViewBag.ToUsrId = new SelectList(db.Users1, "UserId", "uname", payment.ToUsrId);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaymentId,narration,GrpId,amount,FromUsrId,ToUsrId,paid")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FromUsrId = new SelectList(db.Users1, "UserId", "uname", payment.FromUsrId);
            ViewBag.GrpId = new SelectList(db.Groups1, "GroupId", "GrpName", payment.GrpId);
            ViewBag.ToUsrId = new SelectList(db.Users1, "UserId", "uname", payment.ToUsrId);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments1.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment payment = db.Payments1.Find(id);
            db.Payments1.Remove(payment);
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

        public ActionResult Paid(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments1.Find(id);
            payment.paid = 1;
            db.SaveChanges();
            if (payment == null)
            {
                return HttpNotFound();
            }

            return RedirectToAction("Details", "Groups", new { id = (int)Session["id"] });
        }
    }
}
