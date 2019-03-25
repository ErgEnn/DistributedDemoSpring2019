using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.App.EF;
using Domain;
using DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ContactTypesController : ControllerBase
    {

        private readonly IAppUnitOfWork _uow;
        public ContactTypesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/ContactTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactTypeDTO>>> GetContactTypes()
        {
            /*
             Moved functionality over to repository
            var res = new List<ContactTypeDTO>();
            var contactTypes = await _uow.ContactTypes.AllAsync();
            foreach (var contactType in contactTypes)
            {
                res.Add(new ContactTypeDTO()
                {
                    Id = contactType.Id,
                    ContactTypeValue = contactType.ContactTypeValue,
                    ContactCount = contactType.Contacts.Count
                });   
            }
            */
            return Ok(await _uow.ContactTypes.GetAllWithContactCountAsync());
        }

        // GET: api/ContactTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactType>> GetContactType(int id)
        {
            var contactType = await _uow.ContactTypes.FindAsync(id);

            if (contactType == null)
            {
                return NotFound();
            }

            return contactType;
        }

        // PUT: api/ContactTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactType(int id, ContactType contactType)
        {
            if (id != contactType.Id)
            {
                return BadRequest();
            }

            _uow.ContactTypes.Update(contactType);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/ContactTypes
        [HttpPost]
        public async Task<ActionResult<ContactType>> PostContactType(ContactType contactType)
        {
            await _uow.ContactTypes.AddAsync(contactType);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetContactType", new { id = contactType.Id }, contactType);
        }

        // DELETE: api/ContactTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ContactType>> DeleteContactType(int id)
        {
            var contactType = await _uow.ContactTypes.FindAsync(id);
            if (contactType == null)
            {
                return NotFound();
            }

            _uow.ContactTypes.Remove(contactType);
            await _uow.SaveChangesAsync();

            return contactType;
        }
    }
}
