using HallOfFame.DTOs;
using HallOfFame.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HallOfFame.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonsController : Controller
    {
        private readonly IPersonsService _personsService;
        public PersonsController(IPersonsService personsService)
        {
            _personsService = personsService;
        }
            
        [HttpGet("getAllPersons")]
        public async Task<IActionResult> GetAllPersons()
        {
            var persons = await _personsService.GetAllPersons();

            return Ok(persons);
        }

        [HttpGet("getPerson")]
        public async Task<IActionResult> GetPerson(long id) 
        {
            var person = await _personsService.GetPerson(id);

            if (person != null)
                return Ok(person);
            return NotFound();
        }
            
        [HttpPost]
        public async Task<IActionResult> CreatePerson(PersonDTO personDTO) 
        {
            var person = await _personsService.CreatePerson(personDTO);

            return Ok(person);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePerson(long id, PersonDTO personDTO)
        {
            var person = await _personsService.UpdatePerson(id, personDTO);

            if (person != null)
                return Ok(person);
            return NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePerson(long id)
        {
            var person = await _personsService.DeletePerson(id);

            if (person != null)
                return Ok(person);
            return NotFound();
        }
    }
}
