using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
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
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize]
    public class PersonsController : Controller
    {
        private readonly IAppBLL _bll;

        public PersonsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Persons
        public async Task<IActionResult> Index()
        {
            /*
            var persons = await _context.Persons
                .Include(p => p.AppUser)
                .Where(p => p.AppUserId == User.GetUserId()).ToListAsync();
            */
            var persons = await _bll.Persons.AllAsync(User.GetUserId());

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

            var person = await _bll.Persons.FindAsync(id);

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
                await _bll.Persons.AddAsync(person);
                await _bll.SaveChangesAsync();

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

            var person = await _bll.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            var vm = new PersonCreateEditViewModel();
            vm.Person = person;
            vm.AppUserSelectList = new SelectList(
                await _bll.BaseService<AppUser>().AllAsync(),
                nameof(AppUser.Id), nameof(AppUser.FirstLastName), vm.Person.AppUserId);

            return View(vm);
        }

        // POST: Persons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PersonCreateEditViewModel vm)
        {
            if (id != vm.Person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.Persons.Update(vm.Person);
                await _bll.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            vm.AppUserSelectList = new SelectList(
                await _bll.BaseService<AppUser>().AllAsync(), 
                nameof(AppUser.Id), nameof(AppUser.FirstLastName),
                vm.Person.AppUserId);
            return View(vm);
        }

        // GET: Persons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _bll.Persons.FindAsync(id);
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
            _bll.Persons.Remove(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}