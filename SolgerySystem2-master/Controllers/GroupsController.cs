using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SolgerySystem2.Models;
using System.Diagnostics;

namespace SolgerySystem2.Controllers
{
    [Authorize]
    public class GroupsController : Controller
    {
        private UserGrpModel db = new UserGrpModel();

        [Authorize]
        // GET: Groups
        public ActionResult Index()
        {

            var groups1 = db.Groups1.Include(g => g.Usr).Where(g=> g.Usr.uname == User.Identity.Name).Select(g=>g);
            //var gp = db.Groups1.Select(g=>new Group{ GId=g.GId, GrpName=g.GrpName });
            //var gp1 = db.Groups1.GroupBy(g => g.GId).Select(g => g.First());
            Session["nextInt"] = db.Groups1.Max(g => g.GId) + 1;
            return View(groups1.ToList());
        }

        [Authorize]
        // GET: Groups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var group = db.Groups1.Select(g => g).Where(x => x.GId == id);
            ViewBag.grpnm = db.Groups1.Select(g => g).Where(x => x.GId == id).First().GrpName;
            //ViewBag.id = db.Groups1.Find(id).GId;
            //Group group = db.Groups1.Find(id);
            TempData["id"] = db.Groups1.Select(g => g).Where(x => x.GId == id).First().GId;
            Session["id"]= db.Groups1.Select(g => g).Where(x => x.GId == id).First().GId;
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group.ToList());
        }

        [Authorize]
        // GET: Groups/Create
        public ActionResult Create(int id)
        {

            ViewBag.UsrId = new SelectList(db.Users1, "UserId", "uname");
            ViewData["nxtId"]= id;
            ViewBag.ni = id;
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupId,GId,GrpName,UsrId")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Groups1.Add(group);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UsrId = new SelectList(db.Users1, "UserId", "uname", group.UsrId);
            return View(group);
        }

        [Authorize]
        // GET: Groups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Group group = db.Groups1.Find(id);
            ViewBag.grpnm1 = db.Groups1.Select(g => g).Where(x => x.GId == id).First().GrpName;
            Group group = db.Groups1.Where(g => g.GId == id).FirstOrDefault();
            if (group == null)
            {
                return HttpNotFound();
            }
            ViewBag.UsrId = new SelectList(db.Users1.Where(p=>p.uname!=User.Identity.Name), "UserId", "uname", group.UsrId);
            return View(group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupId,GId,GrpName,UsrId")] Group group)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(group).State = EntityState.Modified;
                //db.Groups1.Select(g => g.GId == group.GId);
                var group1 = db.Groups1.Select(g=>g).Where(x=> x.GId == group.GId);
                foreach(var g1 in group1)
                {
                    g1.GrpName = group.GrpName;
                    db.Entry(g1).State = EntityState.Modified;
                    //System.Diagnostics.Debug.Write(g);
                }
                db.SaveChanges();
                //var x = db.Groups1.AddRange(group1);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UsrId = new SelectList(db.Users1, "UserId", "uname", group.UsrId);
            return View(group);
        }

        [Authorize]
        // GET: Groups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups1.Where(g => g.GId == id).FirstOrDefault();
            ViewBag.grpnm1 = db.Groups1.Select(g => g).Where(x => x.GId == id).First().GrpName;
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        [Authorize]
        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var group = db.Groups1.Where(g=> g.GId == id);
            //var group = db.Groups1.Find(id);
            db.Groups1.RemoveRange(group);
            //db.Groups1.Remove(group);
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
        // GET: Groups/Create
        [Authorize]
        public ActionResult AddUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups1.Where(g => g.GId == id).FirstOrDefault();
            if (group == null)
            {
                return HttpNotFound();
            }
            ViewBag.UsrId = new SelectList(db.Users1.Where(p => p.uname != User.Identity.Name), "UserId", "uname");
            return View(group);
            //ViewBag.UsrId = new SelectList(db.Users1, "UserId", "uname");
            //return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser([Bind(Include = "GId,GrpName,UsrId")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Groups1.Add(group);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UsrId = new SelectList(db.Users1.Where(p => p.uname != User.Identity.Name), "UserId", "uname", group.UsrId);
            return View(group);
        }

        // GET: Groups/Delete/5
        [Authorize]
        public ActionResult RemoveUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups1.Find(id);
            try {            
                var x = db.Payments1.Where(g => g.GrpId == group.GId & ( g.ToUsrId == group.UsrId) & g.paid==0).FirstOrDefault().narration;
            
                Debug.WriteLine(x);
                if (group == null)
                {
                    return HttpNotFound();
                }
                return RedirectToAction("cannotRemove");
            }
            catch(Exception e)
            {
                return View(group);
            }
            
        }

        // POST: Groups/Delete/5
        [Authorize]
        [HttpPost, ActionName("RemoveUser")]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveUser(int id)
        {
            var group = db.Groups1.Find(id);
            db.Groups1.Remove(group);
            db.SaveChanges();
            return RedirectToAction("Details", "Groups", new { id = (int)Session["id"] });
        }

        [Authorize]
        public ActionResult FindUser(int id)
        {
            var toUid = db.Groups1.Where(g=>g.GroupId==id).FirstOrDefault().UsrId;
            TempData["toUid"] = toUid;
            Session["toUid"] = toUid;
            Session["ToName"] = db.Users1.Where(g => g.UserId == toUid).FirstOrDefault().uname;
            return RedirectToAction("Index2","Payments");
        }
        public ActionResult cannotRemove()
        {
            return View();
        }


    }

    
}
