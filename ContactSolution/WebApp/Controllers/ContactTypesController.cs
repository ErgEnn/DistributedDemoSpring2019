using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO; 
namespace WebApp.Controllers
{
    public class ContactTypesController : Controller
    {
        private readonly IAppBLL _bll;

        public ContactTypesController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: ContactTypes
        public async Task<IActionResult> Index()
        {
            return View(await _bll.ContactTypes.GetAllWithContactCountAsync());
        }

        // GET: ContactTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactType = await _bll.ContactTypes.FindAsync(id);
            
            if (contactType == null)
            {
                return NotFound();
            }

            return View(contactType);
        }

        // GET: ContactTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContactTypeValue,Id")] BLLAppDTO.ContactType contactType)
        {
            if (ModelState.IsValid)
            { 
                _bll.ContactTypes.Add(contactType);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactType);
        }

        // GET: ContactTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactType = await _bll.ContactTypes.FindAsync(id);
            if (contactType == null)
            {
                return NotFound();
            }
            return View(contactType);
        }

        // POST: ContactTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContactTypeValue,Id")] BLLAppDTO.ContactType contactType)
        {
            if (id != contactType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.ContactTypes.Update(contactType);
                await _bll.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(contactType);
        }

        // GET: ContactTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactType = await _bll.ContactTypes.FindAsync(id);

            if (contactType == null)
            {
                return NotFound();
            }

            return View(contactType);
        }

        // POST: ContactTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _bll.ContactTypes.Remove(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
