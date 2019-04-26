using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.App.DTO;
using DAL.App.EF;
using Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ContactTypesController : ControllerBase
    {

        private readonly IAppBLL _bll;

        public ContactTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/ContactTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.ContactTypeWithContactCounts>>> GetContactTypes()
        {

            return (await _bll.ContactTypes.GetAllWithContactCountAsync())
                .Select(e => PublicApi.v1.Mappers.ContactTypeMapper.MapFromBLL(e)).ToList();
        }

        // GET: api/ContactTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicApi.v1.DTO.ContactType>> GetContactType(int id)
        {
            var contactType = PublicApi.v1.Mappers.ContactTypeMapper.MapFromBLL( await _bll.ContactTypes.FindAsync(id));

            if (contactType == null)
            {
                return NotFound();
            }

            return contactType;
        }

        // PUT: api/ContactTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactType(int id, PublicApi.v1.DTO.ContactType contactType)
        {
            if (id != contactType.Id)
            {
                return BadRequest();
            }

            _bll.ContactTypes.Update(PublicApi.v1.Mappers.ContactTypeMapper.MapFromExternal( contactType));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/ContactTypes
        [HttpPost]
        public async Task<ActionResult<PublicApi.v1.DTO.ContactType>> PostContactType(PublicApi.v1.DTO.ContactType contactType)
        {
            await _bll.ContactTypes.AddAsync(PublicApi.v1.Mappers.ContactTypeMapper.MapFromExternal(contactType));
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetContactType", new { id = contactType.Id }, contactType);
        }

        // DELETE: api/ContactTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PublicApi.v1.DTO.ContactType>> DeleteContactType(int id)
        {
            var contactType = await _bll.ContactTypes.FindAsync(id);
            if (contactType == null)
            {
                return NotFound();
            }

            _bll.ContactTypes.Remove(id);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
