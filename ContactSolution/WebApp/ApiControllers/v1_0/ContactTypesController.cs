using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers.v1_0
{
    [ApiVersion("1.0")]
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

        /// <summary>
        /// Get all ContactType objects
        /// </summary>
        /// <returns>Array of all ContactTypes with counts of contacts.</returns>
        /// <response code="200">The array of ContactTypes was successfully retrieved.</response>
        [ProducesResponseType(typeof(IEnumerable<PublicApi.v1.DTO.ContactTypeWithContactCounts>), StatusCodes.Status200OK)]
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
            var contactType = PublicApi.v1.Mappers.ContactTypeMapper.MapFromBLL(await _bll.ContactTypes.FindAsync(id));

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

            _bll.ContactTypes.Update(PublicApi.v1.Mappers.ContactTypeMapper.MapFromExternal(contactType));
            await _bll.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/ContactTypes
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PublicApi.v1.DTO.ContactType>> PostContactType(
            PublicApi.v1.DTO.ContactType contactType)
        {
            await _bll.ContactTypes.AddAsync(PublicApi.v1.Mappers.ContactTypeMapper.MapFromExternal(contactType));
            await _bll.SaveChangesAsync();

            //return NoContent();

            return CreatedAtAction(
                nameof(GetContactType), new
                {
                    version = HttpContext.GetRequestedApiVersion().ToString(),
                    id = contactType.Id
                }, contactType);
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