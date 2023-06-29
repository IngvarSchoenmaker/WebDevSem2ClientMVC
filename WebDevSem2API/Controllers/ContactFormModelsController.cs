using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDevSem2ClientMVC.Models;
using WebDevSem2ClientMVC.Areas.Identity.Data;

namespace WebDevSem2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactFormModelsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public ContactFormModelsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/ContactFormModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactForm>>> GetContactFormModel()
        {
          if (_context.ContactForm == null)
          {
              return NotFound();
          }
            return await _context.ContactForm.ToListAsync();
        }

        // GET: api/ContactFormModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactForm>> GetContactFormModel(int id)
        {
            if (_context.ContactForm == null)
            {
                return NotFound();
            }
            var contactForm = await _context.ContactForm.FindAsync(id);

            if (contactForm == null)
            {
                return NotFound();
            }

            return contactForm;
        }

        // PUT: api/ContactFormModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactFormModel(int id, ContactForm contactForm)
        {
            if (id != contactForm.ContactFormId)
            {
                return BadRequest();
            }

            _context.Entry(contactForm).State = EntityState.Modified;

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
        public async Task<ActionResult<ContactForm>> PostContactFormModel(ContactForm contactFormModel)
        {
          if (_context.ContactForm == null)
          {
              return Problem("Entity set 'WebDevSem2MySqlContext.ContactFormModel'  is null.");
          }
            _context.ContactForm.Add(contactFormModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContactFormModel", new { id = contactFormModel.ContactFormId }, contactFormModel);
        }

        // DELETE: api/ContactFormModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactFormModel(int id)
        {
            if (_context.ContactForm == null)
            {
                return NotFound();
            }
            var contactFormModel = await _context.ContactForm.FindAsync(id);
            if (contactFormModel == null)
            {
                return NotFound();
            }

            _context.ContactForm.Remove(contactFormModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactFormModelExists(int id)
        {
            return (_context.ContactForm?.Any(e => e.ContactFormId == id)).GetValueOrDefault();
        }
    }
}
