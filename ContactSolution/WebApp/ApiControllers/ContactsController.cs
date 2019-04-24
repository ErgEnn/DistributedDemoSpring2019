using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ContactsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public ContactsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Contacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BLL.App.DTO.Contact>>> GetContacts()
        {
            return await _bll.Contacts.AllForUserAsync(User.GetUserId());
        }

        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BLL.App.DTO.Contact>> GetContact(int id)
        {
            var contact = await _bll.Contacts.FindForUserAsync(id, User.GetUserId());

            if (contact == null)
            {
                return NotFound();
            }

            return contact;
        }

        // PUT: api/Contacts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact(int id, BLL.App.DTO.Contact contact)
        {
            if (id != contact.Id)
            {
                return BadRequest();
            }

            // check, that the Person being used is really belongs to logged in user
            if (!await _bll.Contacts.BelongsToUserAsync(id, User.GetUserId()))
            {
                return NotFound();
            }

            _bll.Contacts.Update(contact);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Contacts
        [HttpPost]
        public async Task<ActionResult<BLL.App.DTO.Contact>> PostContact(BLL.App.DTO.Contact contact)
        {
            
            // check, that the Person being used is really belongs to logged in user
            if (!await _bll.Persons.BelongsToUserAsync(contact.PersonId, User.GetUserId()))
            {
                return NotFound();
            }
            
            _bll.Contacts.Add(contact);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetContact", new { id = contact.Id }, contact);
        }

        // DELETE: api/Contacts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteContact(int id)
        {

            // check, that the Person being used is really belongs to logged in user
            if (!await _bll.Contacts.BelongsToUserAsync(id, User.GetUserId()))
            {
                return NotFound();
            }
            
            _bll.Contacts.Remove(id);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}
