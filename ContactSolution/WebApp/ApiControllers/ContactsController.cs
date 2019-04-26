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
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.Contact>>> GetContacts()
        {
            return (await _bll.Contacts.AllForUserAsync(User.GetUserId()))
                .Select(e => PublicApi.v1.Mappers.ContactMapper.MapFromBLL(e)).ToList();
        }

        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicApi.v1.DTO.Contact>> GetContact(int id)
        {
            var contact = PublicApi.v1.Mappers.ContactMapper.MapFromBLL(await _bll.Contacts.FindForUserAsync(id, User.GetUserId()));

            if (contact == null)
            {
                return NotFound();
            }

            return contact;
        }

        // PUT: api/Contacts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact(int id, PublicApi.v1.DTO.Contact contact)
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

            _bll.Contacts.Update(PublicApi.v1.Mappers.ContactMapper.MapFromExternal(contact));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Contacts
        [HttpPost]
        public async Task<ActionResult<PublicApi.v1.DTO.Contact>> PostContact(PublicApi.v1.DTO.Contact contact)
        {
            
            // check, that the Person being used is really belongs to logged in user
            if (!await _bll.Persons.BelongsToUserAsync(contact.PersonId, User.GetUserId()))
            {
                return NotFound();
            }
            
            _bll.Contacts.Add(PublicApi.v1.Mappers.ContactMapper.MapFromExternal(contact));
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
