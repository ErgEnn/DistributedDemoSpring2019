using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.App.EF;
using Domain;
using Identity;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize]
    public class ContactsController : Controller
    {
        private readonly IAppBLL _bll;

        public ContactsController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: Contacts
        public async Task<IActionResult> Index()
        {
            var contacts = await _bll.Contacts.AllForUserAsync(User.GetUserId());


            return View(contacts);
        }

        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _bll.Contacts.FindForUserAsync(id.Value, User.GetUserId());

            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // GET: Contacts/Create
        public async Task<IActionResult> Create()
        {
            var vm = new ContactCreateEditViewModel()
            {
                ContactTypeSelectList = new SelectList(
                    await _bll.ContactTypes.AllAsync(),
                    nameof(ContactType.Id),
                    nameof(ContactType.ContactTypeValue)),
                PersonSelectList = new SelectList(
                    await _bll.Persons.AllForUserAsync(User.GetUserId()),
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
        public async Task<IActionResult> Create(ContactCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _bll.Contacts.Add(vm.Contact);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }


            vm.ContactTypeSelectList = new SelectList(
                await _bll.ContactTypes.AllAsync(),
                nameof(ContactType.Id),
                nameof(ContactType.ContactTypeValue),
                vm.Contact.ContactTypeId);
            vm.PersonSelectList = new SelectList(
                await _bll.Persons.AllForUserAsync(User.GetUserId()),
                nameof(Person.Id),
                nameof(Person.FirstLastName), vm.Contact.PersonId);


            return View(vm);
        }

        // GET: Contacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _bll.Contacts.FindForUserAsync(id.Value, User.GetUserId());
            if (contact == null)
            {
                return NotFound();
            }

            var vm = new ContactCreateEditViewModel();
            vm.Contact = contact;
            vm.ContactTypeSelectList = new SelectList(
                await _bll.ContactTypes.AllAsync(),
                nameof(ContactType.Id),
                nameof(ContactType.ContactTypeValue),
                vm.Contact.ContactTypeId);
            vm.PersonSelectList = new SelectList(
                await _bll.Persons.AllForUserAsync(User.GetUserId()),
                nameof(Person.Id),
                nameof(Person.FirstLastName), vm.Contact.PersonId);


            return View(vm);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ContactCreateEditViewModel vm)
        {
            if (id != vm.Contact.Id)
            {
                return NotFound();
            }

            if (!await _bll.Contacts.BelongsToUserAsync(id, User.GetUserId()))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.Contacts.Update(vm.Contact);

                await _bll.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            vm.ContactTypeSelectList = new SelectList(
                await _bll.ContactTypes.AllAsync(),
                nameof(ContactType.Id),
                nameof(ContactType.ContactTypeValue),
                vm.Contact.ContactTypeId);
            vm.PersonSelectList = new SelectList(
                await _bll.Persons.AllForUserAsync(User.GetUserId()),
                nameof(Person.Id),
                nameof(Person.FirstLastName), vm.Contact.PersonId);

            return View(vm);
        }

        // GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _bll.Contacts.FindForUserAsync(id.Value, User.GetUserId());
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
            if (!await _bll.Contacts.BelongsToUserAsync(id, User.GetUserId()))
            {
                return NotFound();
            }

            _bll.Contacts.Remove(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}