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
    public class StudentController : Controller
    {
        private CollegeDbContext db = new CollegeDbContext();

        // GET: Students
        public async Task<ActionResult> Index()
        {
            var students = db.Students.Include(s => s.Subject);
            return View(await students.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.Include(s=>s.Subject)
                                            .Include(g=>g.Grades)
                                            .FirstOrDefaultAsync(i => i.StudentId == id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            var student = new Student
            {
                Birthday = DateTime.Today
            };

            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "Title");
            return View(student);
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SubjectId,RegistrationNumber,Name,Birthday")] StudentViewModel studentViewModel)
        {
            var student = new Student(studentViewModel.SubjectId, studentViewModel.RegistrationNumber, studentViewModel.Name, studentViewModel.Birthday);
            try
            {
                if (ModelState.IsValid)
                {
                    db.Students.Add(student);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            PopulateSubjectsDropDownList(student.SubjectId);

            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "Title", student.SubjectId);

            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "StudentId,SubjectId,RegistrationNumber,Name,Birthday")] StudentViewModel studentViewModel)
        {
            var student = new Student(studentViewModel.SubjectId, studentViewModel.RegistrationNumber, studentViewModel.Name, studentViewModel.Birthday)
            {
                StudentId = studentViewModel.StudentId ?? default(int)
            };

            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "Title", studentViewModel.SubjectId);

            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Student student = await db.Students.FindAsync(id);
            db.Students.Remove(student);
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
