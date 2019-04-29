using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers.v2_0
{
    [ApiVersion( "2.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
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
        public async Task<ActionResult<IEnumerable<PublicApi.v2.DTO.ContactTypeWithContactCounts>>> GetContactTypes()
        {

            return (await _bll.ContactTypes.GetAllWithContactCountAsync())
                .Select(e => PublicApi.v2.Mappers.ContactTypeMapper.MapFromBLL(e)).ToList();
        }


        
        // GET: api/ContactTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicApi.v2.DTO.ContactType>> GetContactType(int id)
        {
            var contactType = PublicApi.v2.Mappers.ContactTypeMapper.MapFromBLL( await _bll.ContactTypes.FindAsync(id));

            if (contactType == null)
            {
                return NotFound();
            }

            return contactType;
        }

        // PUT: api/ContactTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactType(int id, PublicApi.v2.DTO.ContactType contactType)
        {
            if (id != contactType.Id)
            {
                return BadRequest();
            }

            _bll.ContactTypes.Update(PublicApi.v2.Mappers.ContactTypeMapper.MapFromExternal( contactType));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/ContactTypes
        [HttpPost]
        public async Task<ActionResult<PublicApi.v2.DTO.ContactType>> PostContactType(PublicApi.v2.DTO.ContactType contactType)
        {
            await _bll.ContactTypes.AddAsync(PublicApi.v2.Mappers.ContactTypeMapper.MapFromExternal(contactType));
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetContactType", new { id = contactType.Id }, contactType);
        }

        // DELETE: api/ContactTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PublicApi.v2.DTO.ContactType>> DeleteContactType(int id)
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
