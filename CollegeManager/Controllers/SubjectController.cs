using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CollegeManager.Data.Entities;
using CollegeManager.Data.Persistance;
using CollegeManager.ViewModel;

namespace CollegeManager.Controllers
{
    public class SubjectController : Controller
    {
        private CollegeDbContext db = new CollegeDbContext();

        // GET: Subjects
        public async Task<ActionResult> Index()
        {
            var subjects = db.Subjects.Include(c => c.Course);

            return View(await subjects.ToListAsync());
        }

        // GET: Subjects/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Subject subject = await db.Subjects.Include(c => c.Course)
                                       .Include(s => s.Students)
                                       .Include(t => t.Teacher)
                                       .FirstOrDefaultAsync(i => i.CourseId == id);

            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // GET: Subjects/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Title");
            return View();
        }

        // POST: Subjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Title,CourseId")] SubjectViewModel subjectViewModel)
        {
            var subject = new Subject(subjectViewModel.CourseId, subjectViewModel.Title);

            try
            {
                if (ModelState.IsValid)
                {
                    db.Subjects.Add(subject);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            PopulateCoursesDropDownList(subject.CourseId);
            return View(subject);
        }

        // GET: Subjects/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = await db.Subjects.FindAsync(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Title", subject.CourseId);
            return View(subject);
        }

        // POST: Subjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SubjectId,Title,CourseId")] SubjectViewModel subjectViewModel)
        {
            var subject = new Subject(subjectViewModel.CourseId, subjectViewModel.Title)
            {
                SubjectId = subjectViewModel.SubjectId ?? default(int)
            };

            if (ModelState.IsValid)
            {
                db.Entry(subject).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Title", subjectViewModel.CourseId);
            return View(subject);
        }

        // GET: Subjects/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = await db.Subjects.FindAsync(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Subject subject = await db.Subjects.FindAsync(id);
            db.Subjects.Remove(subject);
            await db.SaveChangesAsync();
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


        private void PopulateCoursesDropDownList(object selectedCourse = null)
        {
            var departmentsQuery = from d in db.Courses
                                   orderby d.Title
                                   select d;
            ViewBag.CourseId = new SelectList(departmentsQuery, "CourseId", "Title", selectedCourse);
        }
    }
}
