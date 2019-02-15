using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;
using Identity;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize]
    public class ContactsController : Controller
    {
        private readonly AppDbContext _context;

        public ContactsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Contacts
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Contacts
                .Include(c => c.ContactType)
                .Include(c => c.Person)
                .Where(c => c.Person.AppUserId == User.GetUserId());

            return View(await appDbContext.ToListAsync());
        }

        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts
                .Include(c => c.ContactType)
                .Include(c => c.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // GET: Contacts/Create
        public IActionResult Create()
        {
            var vm = new ContactCreateViewModel()
            {
                ContactTypeSelectList = new SelectList(
                    _context.ContactTypes, 
                    nameof(ContactType.Id), 
                    nameof(ContactType.ContactTypeValue)),
                PersonSelectList = new SelectList(
                    _context.Persons.Where(p => p.AppUserId == User.GetUserId()),
                    nameof(Person.Id), 
                    nameof(Person.FirstLastName))
            };
            return View(vm);
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vm.Contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.PersonSelectList = new SelectList(_context.Persons.Where(p => p.AppUserId == User.GetUserId()),
                nameof(Person.Id), nameof(Person.FirstLastName), vm.Contact.PersonId);
            vm.ContactTypeSelectList = new SelectList(_context.ContactTypes, nameof(ContactType.Id),
                nameof(ContactType.ContactTypeValue), vm.Contact.ContactTypeId);

            return View(vm);
        }

        // GET: Contacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            ViewData["ContactTypeId"] =
                new SelectList(_context.ContactTypes, "Id", "ContactTypeValue", contact.ContactTypeId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "FirstName", contact.PersonId);
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContactValue,PersonId,ContactTypeId,Id")]
            Contact contact)
        {
            if (id != contact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contact.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["ContactTypeId"] =
                new SelectList(_context.ContactTypes, "Id", "ContactTypeValue", contact.ContactTypeId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "FirstName", contact.PersonId);
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts
                .Include(c => c.ContactType)
                .Include(c => c.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactExists(int id)
        {
            return _context.Contacts.Any(e => e.Id == id);
        }
    }
}