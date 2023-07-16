﻿using API.DataAccess;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace API.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class PeopleController : ControllerBase
   {
      private readonly APIContext _context;

      public PeopleController(APIContext context)
      {
         _context = context;
      }

      [HttpGet]
      public async Task<ActionResult<IEnumerable<People>>> GetPeoples(string? name = null)
      {
         if (_context.Peoples == null)
         {
            return NotFound();
         }
         IQueryable<People> model = _context.Peoples.AsNoTrackingWithIdentityResolution();
         if (string.IsNullOrEmpty(name) == false)
         {
            //model = model.Where(c => c.Name.Contains(name));
            model = model.Where(c => EF.Functions.Like(c.Name, $"%{name}%"));
            //model = model.WhereContains(c => c.Id, name);
         }
         return await model.ToListAsync();
      }

      // GET: api/People/5
      [HttpGet("{id}")]
      public async Task<ActionResult<People>> GetPeople(int id)
      {
         if (_context.Peoples == null)
         {
            return NotFound();
         }
         var people = await _context.Peoples.FindAsync(id);

         if (people == null)
         {
            return NotFound();
         }

         return people;
      }

      // PUT: api/People/5
      // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
      [HttpPut("{id}")]
      public async Task<IActionResult> PutPeople(int id, People people)
      {
         if (id != people.Id)
         {
            return BadRequest();
         }

         _context.Entry(people).State = EntityState.Modified;

         try
         {
            await _context.SaveChangesAsync();
         }
         catch (DbUpdateConcurrencyException)
         {
            if (!PeopleExists(id))
            {
               return NotFound();
            }
            else
            {
               throw;
            }
         }

         return NoContent();
      }

      // POST: api/People
      // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
      [HttpPost]
      public async Task<ActionResult<People>> PostPeople(People people)
      {
         if (_context.Peoples == null)
         {
            return Problem("Entity set 'APIContext.Peoples'  is null.");
         }
         _context.Peoples.Add(people);
         await _context.SaveChangesAsync();

         return CreatedAtAction("GetPeople", new { id = people.Id }, people);
      }

      // DELETE: api/People/5
      [HttpDelete("{id}")]
      public async Task<IActionResult> DeletePeople(int id)
      {
         if (_context.Peoples == null)
         {
            return NotFound();
         }
         var people = await _context.Peoples.FindAsync(id);
         if (people == null)
         {
            return NotFound();
         }

         _context.Peoples.Remove(people);
         await _context.SaveChangesAsync();

         return NoContent();
      }

      private bool PeopleExists(int id)
      {
         return (_context.Peoples?.Any(e => e.Id == id)).GetValueOrDefault();
      }
   }
}
