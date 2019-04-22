using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

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
        public async Task<ActionResult<IEnumerable<BLLAppDTO.ContactTypeContactCount>>> GetContactTypes()
        {

            return await _bll.ContactTypes.GetAllWithContactCountAsync();
        }

        // GET: api/ContactTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BLLAppDTO.ContactType>> GetContactType(int id)
        {
            var contactType = await _bll.ContactTypes.FindAsync(id);

            if (contactType == null)
            {
                return NotFound();
            }

            return contactType;
        }

        // PUT: api/ContactTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactType(int id, BLLAppDTO.ContactType contactType)
        {
            if (id != contactType.Id)
            {
                return BadRequest();
            }

            _bll.ContactTypes.Update(contactType);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/ContactTypes
        [HttpPost]
        public async Task<ActionResult<BLLAppDTO.ContactType>> PostContactType(BLLAppDTO.ContactType contactType)
        {
            await _bll.ContactTypes.AddAsync(contactType);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetContactType", new { id = contactType.Id }, contactType);
        }

        // DELETE: api/ContactTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BLLAppDTO.ContactType>> DeleteContactType(int id)
        {
            var contactType = await _bll.ContactTypes.FindAsync(id);
            if (contactType == null)
            {
                return NotFound();
            }

            _bll.ContactTypes.Remove(contactType);
            await _bll.SaveChangesAsync();

            return contactType;
        }
    }
}
