using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using simpleProject.Data;
using simpleProject.Models;
using simpleProject.Models.Entities;

namespace simpleProject.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;
        public StudentsController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Add(AddStudentsViewModel addStudentsViewModel)
        {
            var student = new Student
            {
               Name = addStudentsViewModel.Name,
               Email= addStudentsViewModel.Email,
               Phone = addStudentsViewModel.Phone,
               Subscribed = addStudentsViewModel.Subscribed,
            };

            await applicationDbContext.Students.AddAsync(student);
            await applicationDbContext.SaveChangesAsync();

            return View();
        }

        [HttpGet]

        public async Task<IActionResult> List()
        {
            var students = await applicationDbContext.Students.ToListAsync();
            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await applicationDbContext.Students.FindAsync(id);

            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student viewModel)
        {
            var students = await applicationDbContext.Students.FindAsync(viewModel.Id);

            if(students is not null) { 
                students.Name = viewModel.Name;
                students.Email = viewModel.Email;
                students.Phone=viewModel
                    .Phone;
                students.Subscribed = viewModel.Subscribed;

                await applicationDbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Students");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Student viewModel)
        {
            var students = await applicationDbContext.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(x=>x.Id == viewModel.Id);

            if(students is not null)
            {
                 applicationDbContext.Students.Remove(viewModel);
                await applicationDbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Students");
        }

    }
}
