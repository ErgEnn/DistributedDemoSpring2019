using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.App.EF;
using Domain;
using Domain.Identity;
using Identity;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize]
    public class PersonsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public PersonsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }


        // GET: Persons
        public async Task<IActionResult> Index()
        {
            /*
            var persons = await _context.Persons
                .Include(p => p.AppUser)
                .Where(p => p.AppUserId == User.GetUserId()).ToListAsync();
            */
            var persons = await _uow.Persons.AllAsync(User.GetUserId());

            return View(persons);
        }

        // GET: Persons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*
            var person = await _context.Persons
                .Include(p => p.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            */

            var person = await _uow.Persons.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: Persons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Persons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Id")] Person person)
        {
            person.AppUserId = User.GetUserId();

            if (ModelState.IsValid)
            {
                await _uow.Persons.AddAsync(person);
                await _uow.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(person);
        }


        // GET: Persons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _uow.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            ViewData["AppUserId"] = new SelectList(
                await _uow.BaseRepository<AppUser>().AllAsync(),
                "Id", "Id", person.AppUserId);

            return View(person);
        }

        // POST: Persons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FirstName,LastName,AppUserId,Id")]
            Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.Persons.Update(person);
                await _uow.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["AppUserId"] = new SelectList(await _uow.BaseRepository<AppUser>().AllAsync(), "Id", "Id",
                person.AppUserId);
            return View(person);
        }

        // GET: Persons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _uow.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: Persons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _uow.Persons.Remove(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}