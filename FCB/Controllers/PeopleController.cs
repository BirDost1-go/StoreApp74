using Microsoft.AspNetCore.Mvc;
using FCB.Models;
using FCB.Services;
using Microsoft.EntityFrameworkCore;

namespace FCB.Controllers
{
    public class PeopleController : Controller
    {
        private readonly IPersonService _personService;

        public PeopleController(IPersonService personService)
        {
            _personService = personService;
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
            var people = await _personService.GetAllAsync();
            return View(people);
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var people = await _personService.GetByIdAsync(id.Value);

            if (people == null)
            {
                return NotFound();
            }

            return View(people);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,PhoneNumber")] People people)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _personService.AddAsync(people);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    var message = ex.InnerException?.Message ?? ex.Message;
                    if (message.Contains("Email"))
                    {
                        ModelState.AddModelError("Email", "Email already exists.");
                    }
                    else if (message.Contains("PhoneNumber"))
                    {
                        ModelState.AddModelError("PhoneNumber", "Phone number already exists.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "An unexpected error occurred. Please try again.");
                    }
                }
            }
            return View(people);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var people = await _personService.GetByIdAsync(id.Value);
            if (people == null)
            {
                return NotFound();
            }
            return View(people);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,PhoneNumber")] People people)
        {
            if (id != people.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _personService.UpdateAsync(people);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    var message = ex.InnerException?.Message ?? ex.Message;
                    if (message.Contains("Email"))
                    {
                        ModelState.AddModelError("Email", "Email already exists.");
                    }
                    else if (message.Contains("PhoneNumber"))
                    {
                        ModelState.AddModelError("PhoneNumber", "Phone number already exists.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "An unexpected error occurred. Please try again.");
                    }
                }

            }
            return View(people);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var people = await _personService.GetByIdAsync(id.Value);
            if (people == null)
            {
                return NotFound();
            }

            return View(people);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _personService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
