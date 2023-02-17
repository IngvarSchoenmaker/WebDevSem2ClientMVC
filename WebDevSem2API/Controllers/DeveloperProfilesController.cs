using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDevSem2ClientMVC.Models;
using WebDevSem2API.Entities;

namespace WebDevSem2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeveloperProfilesController : ControllerBase
    {
        private readonly WebDevSem2MySqlContext _context;

        public DeveloperProfilesController(WebDevSem2MySqlContext context)
        {
            _context = context;
        }

        // GET: api/DeveloperProfiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeveloperProfile>>> GetDeveloperProfile()
        {
          if (_context.DeveloperProfile == null)
          {
              return NotFound();
          }
            return await _context.DeveloperProfile.ToListAsync();
        }

        // GET: api/DeveloperProfiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DeveloperProfile>> GetDeveloperProfile(int id)
        {
          if (_context.DeveloperProfile == null)
          {
              return NotFound();
          }
            var developerProfile = await _context.DeveloperProfile.FindAsync(id);

            if (developerProfile == null)
            {
                return NotFound();
            }

            return developerProfile;
        }

        // PUT: api/DeveloperProfiles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeveloperProfile(int id, DeveloperProfile developerProfile)
        {
            if (id != developerProfile.DeveloperProfileId)
            {
                return BadRequest();
            }

            _context.Entry(developerProfile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeveloperProfileExists(id))
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

        // POST: api/DeveloperProfiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DeveloperProfile>> PostDeveloperProfile(DeveloperProfile developerProfile)
        {
          if (_context.DeveloperProfile == null)
          {
              return Problem("Entity set 'WebDevSem2MySqlContext.DeveloperProfile'  is null.");
          }
            _context.DeveloperProfile.Add(developerProfile);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDeveloperProfile", new { id = developerProfile.DeveloperProfileId }, developerProfile);
        }

        // DELETE: api/DeveloperProfiles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeveloperProfile(int id)
        {
            if (_context.DeveloperProfile == null)
            {
                return NotFound();
            }
            var developerProfile = await _context.DeveloperProfile.FindAsync(id);
            if (developerProfile == null)
            {
                return NotFound();
            }

            _context.DeveloperProfile.Remove(developerProfile);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeveloperProfileExists(int id)
        {
            return (_context.DeveloperProfile?.Any(e => e.DeveloperProfileId == id)).GetValueOrDefault();
        }
    }
}
