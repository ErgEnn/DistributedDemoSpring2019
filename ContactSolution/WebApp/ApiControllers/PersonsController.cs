using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
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
        public async Task<ActionResult<IEnumerable<BLLAppDTO.Person>>> GetPersons()
        {
            return await _bll.Persons.AllForUserAsync(User.GetUserId());
        }

        // GET: api/Persons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BLLAppDTO.Person>> GetPerson(int id)
        {
            var person = await _bll.Persons.FindForUserAsync(id, User.GetUserId());

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        // PUT: api/Persons/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, BLLAppDTO.Person person)
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
            
            _bll.Persons.Update(person);
            await _bll.SaveChangesAsync();


            return NoContent();
        }

        // POST: api/Persons
        [HttpPost]
        public async Task<ActionResult<BLLAppDTO.Person>> PostPerson(BLLAppDTO.Person person)
        {
            person.AppUserId = User.GetUserId();
            
            _bll.Persons.Add(person);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new { id = person.Id }, person);
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
