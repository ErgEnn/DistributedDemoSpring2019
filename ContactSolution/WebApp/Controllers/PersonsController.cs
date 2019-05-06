using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using ee.itcollege.akaver.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
            var persons = await _bll.Persons.AllForUserAsync(User.GetUserId());
            return View(persons);
        }

        // GET: Persons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _bll.Persons.FindForUserAsync(id.Value, User.GetUserId());

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
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Id")] BLL.App.DTO.Person person)
        {
            person.AppUserId = User.GetUserId();

            if (ModelState.IsValid)
            {
                _bll.Persons.Add(person);
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

            var person = await _bll.Persons.FindForUserAsync(id.Value, User.GetUserId());
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: Persons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BLL.App.DTO.Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            // check for the ownership - is this Person record really belonging to logged in user.
            if (!await _bll.Persons.BelongsToUserAsync(id, User.GetUserId()))
            {
                return NotFound();
            }


            person.AppUserId = User.GetUserId();

            if (ModelState.IsValid)
            {
                _bll.Persons.Update(person);
                await _bll.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(person);
        }

        // GET: Persons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _bll.Persons.FindForUserAsync(id.Value, User.GetUserId());

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
            // check for the ownership - is this Person record really belonging to logged in user.
            if (!await _bll.Persons.BelongsToUserAsync(id, User.GetUserId()))
            {
                return NotFound();
            }

            _bll.Persons.Remove(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}