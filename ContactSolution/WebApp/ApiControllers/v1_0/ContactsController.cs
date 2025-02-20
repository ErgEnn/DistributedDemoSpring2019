using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using ee.itcollege.akaver.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers.v1_0
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
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
            var contact =
                PublicApi.v1.Mappers.ContactMapper.MapFromBLL(
                    await _bll.Contacts.FindForUserAsync(id, User.GetUserId()));

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

            // get the enitity back with attached state id - (- maxint)
            contact = PublicApi.v1.Mappers.ContactMapper.MapFromBLL(
                _bll.Contacts.Add(PublicApi.v1.Mappers.ContactMapper.MapFromExternal(contact)));
            // ef will update its internally tracked entities
            await _bll.SaveChangesAsync();
            // get the updated entity, now with ID from database
            contact = PublicApi.v1.Mappers.ContactMapper.MapFromBLL(
                _bll.Contacts.GetUpdatesAfterUOWSaveChanges(
                    PublicApi.v1.Mappers.ContactMapper.MapFromExternal(contact)));


            return CreatedAtAction("GetContact", new {id = contact.Id}, contact);
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