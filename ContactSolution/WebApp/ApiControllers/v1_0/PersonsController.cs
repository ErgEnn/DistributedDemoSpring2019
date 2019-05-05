using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers.v1_0
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PersonsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public PersonsController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: api/Persons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.Person>>> GetPersons()
        {
            return (await _bll.Persons.AllForUserAsync(User.GetUserId()))
                .Select(e => PublicApi.v1.Mappers.PersonMapper.MapFromBLL(e)).ToList();
        }

        // GET: api/Persons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicApi.v1.DTO.Person>> GetPerson(int id)
        {
            var person =
                PublicApi.v1.Mappers.PersonMapper.MapFromBLL(await _bll.Persons.FindForUserAsync(id, User.GetUserId()));

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        // PUT: api/Persons/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, PublicApi.v1.DTO.Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            // check for the ownership - is this Person record really belonging to logged in user.
            if (!await _bll.Persons.BelongsToUserAsync(id, User.GetUserId()))
            {
                return NotFound();
            }

            person.AppUserId = User.GetUserId();

            _bll.Persons.Update(PublicApi.v1.Mappers.PersonMapper.MapFromExternal(person));
            await _bll.SaveChangesAsync();


            return NoContent();
        }

        // POST: api/Persons
        [HttpPost]
        public async Task<ActionResult<PublicApi.v1.DTO.Person>> PostPerson(PublicApi.v1.DTO.Person person)
        {
            person.AppUserId = User.GetUserId();

            person = PublicApi.v1.Mappers.PersonMapper.MapFromBLL(
                _bll.Persons.Add(PublicApi.v1.Mappers.PersonMapper.MapFromExternal(person)));
            await _bll.SaveChangesAsync();
            person = PublicApi.v1.Mappers.PersonMapper.MapFromBLL(
                _bll.Persons.GetUpdatesAfterUOWSaveChanges(PublicApi.v1.Mappers.PersonMapper.MapFromExternal(person)));

            // get the new id into the object


            return CreatedAtAction("GetPerson", new {id = person.Id}, person);
        }

        // DELETE: api/Persons/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePerson(int id)
        {
            // check for the ownership - is this Person record really belonging to logged in user.
            if (!await _bll.Persons.BelongsToUserAsync(id, User.GetUserId()))
            {
                return NotFound();
            }

            _bll.Persons.Remove(id);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}