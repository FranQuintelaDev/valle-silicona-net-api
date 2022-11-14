using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_ValleSilicona.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace API_ValleSilicona.Controllers
{
    [Route("api/bootcamp/")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly PeopleContext _context;

        public PeopleController(PeopleContext context)
        {
            _context = context;
        }

        // GET: api/bootcamp/{idBootcamp}/people/{idPeople}
        [HttpGet("{idBootcamp}/people")]
        public async Task<ActionResult<IEnumerable<People>>> GetPeople(long idBootcamp)
        {
            List<People> listPeople = await _context.PeopleItems.ToListAsync();

            if (idBootcamp != 4) return NoContent();

            if (listPeople.Count == 0) return NoContent();

            return listPeople;

        }

        // GET: api/People/5
        [HttpGet("{idBootcamp}/people/{idPeople}")]
        public async Task<ActionResult<People>> GetPeople(long idBootcamp, long idPeople)
        {
            var People = await _context.PeopleItems.FindAsync(idPeople);

            if (idBootcamp != 4) return NoContent();

            if (People == null) return NotFound();

            return People;
        }

        // PUT: api/People/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{idBootcamp}/people/{idPeople}")]
        //public async Task<IActionResult> PutPeople(long idPeople, People People)
        //{
        //    if (idPeople != People.Id) return BadRequest();

        //    _context.Entry(People).State = EntityState.Modified;

        //    try{ await _context.SaveChangesAsync(); }

        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!PeopleExists(idPeople))return NotFound();
        //        else throw;
        //    }

        //    return NoContent();
        //}
        // PATCH: api/People/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{idBootcamp}/people/{idPeople}")]
        public async Task<IActionResult> JsonPatchWithModelState(long idBootcamp, long idPeople, [FromBody] JsonPatchDocument<People> patchDoc)
        {
            if (patchDoc == null) return BadRequest(ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (idBootcamp != 4) return NoContent();

            var People = _context.PeopleItems.Find(idPeople);

            if (People == null) return NotFound();

            patchDoc.ApplyTo(People, ModelState);

            _context.Entry(People).State = EntityState.Modified;

            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeopleExists(idPeople)) return NotFound();
                else throw;
            }

            return new OkResult();

        }

        // POST: api/People
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{idBootcamp}/people")]
        public async Task<ActionResult<People>> PostPeople(long idBootcamp, People People)
        {
            if (idBootcamp != 4) return NoContent();

            _context.PeopleItems.Add(People);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostPeople), new { idPeople = People.Id }, People);
        }


        // DELETE: api/People/5
        [HttpDelete("{idBootcamp}/people/{idPeople}")]
        public async Task<IActionResult> DeletePeople(long idBootcamp, long idPeople)
        {
            if (idBootcamp != 4) return NoContent();

            var People = await _context.PeopleItems.FindAsync(idPeople);
            
            if (People == null) return NotFound(); 

            _context.PeopleItems.Remove(People);

            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool PeopleExists(long id)
        {
            return _context.PeopleItems.Any(e => e.Id == id);
        }
    }
}
