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
    public class ContactFormModelsController : ControllerBase
    {
        private readonly WebDevSem2MySqlContext _context;

        public ContactFormModelsController(WebDevSem2MySqlContext context)
        {
            _context = context;
        }

        // GET: api/ContactFormModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactFormModel>>> GetContactFormModel()
        {
          if (_context.ContactFormModel == null)
          {
              return NotFound();
          }
            return await _context.ContactFormModel.ToListAsync();
        }

        // GET: api/ContactFormModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactFormModel>> GetContactFormModel(int id)
        {
          if (_context.ContactFormModel == null)
          {
              return NotFound();
          }
            var contactFormModel = await _context.ContactFormModel.FindAsync(id);

            if (contactFormModel == null)
            {
                return NotFound();
            }

            return contactFormModel;
        }

        // PUT: api/ContactFormModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactFormModel(int id, ContactFormModel contactFormModel)
        {
            if (id != contactFormModel.ContactFormId)
            {
                return BadRequest();
            }

            _context.Entry(contactFormModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactFormModelExists(id))
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

        // POST: api/ContactFormModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ContactFormModel>> PostContactFormModel(ContactFormModel contactFormModel)
        {
          if (_context.ContactFormModel == null)
          {
              return Problem("Entity set 'WebDevSem2MySqlContext.ContactFormModel'  is null.");
          }
            _context.ContactFormModel.Add(contactFormModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContactFormModel", new { id = contactFormModel.ContactFormId }, contactFormModel);
        }

        // DELETE: api/ContactFormModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactFormModel(int id)
        {
            if (_context.ContactFormModel == null)
            {
                return NotFound();
            }
            var contactFormModel = await _context.ContactFormModel.FindAsync(id);
            if (contactFormModel == null)
            {
                return NotFound();
            }

            _context.ContactFormModel.Remove(contactFormModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactFormModelExists(int id)
        {
            return (_context.ContactFormModel?.Any(e => e.ContactFormId == id)).GetValueOrDefault();
        }
    }
}
