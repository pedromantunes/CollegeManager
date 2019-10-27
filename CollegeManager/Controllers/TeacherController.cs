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
    public class TeacherController : Controller
    {
        private CollegeDbContext db = new CollegeDbContext();

        // GET: Teachers
        public async Task<ActionResult> Index()
        {
            return View(await db.Teachers.ToListAsync());
        }

        // GET: Teachers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Teacher teacher = await db.Teachers.Include(c => c.Subject)
                                      .FirstOrDefaultAsync(i => i.TeacherId == id);

            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // GET: Teachers/Create
        public ActionResult Create()
        {
            var teacher = new Teacher
            {
                Birthday = DateTime.Today
            };

            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "Title");

            return View(teacher);
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SubjectId,Salary,Name,Birthday")] TeacherViewModel teacherViewModel)
        {
            var teacher = new Teacher(teacherViewModel.Name, teacherViewModel.Birthday, teacherViewModel.Salary, teacherViewModel.SubjectId);

            try
            {
                if (ModelState.IsValid)
                {
                    db.Teachers.Add(teacher);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            PopulateSubjectsDropDownList(teacher.SubjectId);
            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = await db.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "Title", teacher.SubjectId);
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TeacherId,Salary,Name,Birthday, SubjectId")] TeacherViewModel teacherViewModel)
        {
            var teacher = new Teacher(teacherViewModel.Name, teacherViewModel.Birthday, teacherViewModel.Salary, teacherViewModel.SubjectId)
            {
                TeacherId = teacherViewModel.TeacherId ?? default(int)
            };

            if (ModelState.IsValid)
            {
                db.Entry(teacher).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "Title", teacherViewModel.SubjectId);

            return View(teacher);
        }

        // GET: Teachers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = await db.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Teacher teacher = await db.Teachers.FindAsync(id);
            db.Teachers.Remove(teacher);
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

        private void PopulateSubjectsDropDownList(object selectedSubject = null)
        {
            var subjectQuery = from d in db.Subjects
                                   orderby d.Title
                                   select d;
            ViewBag.SubjectId = new SelectList(subjectQuery, "Subjectid", "Title", selectedSubject);
        }
    }
}
