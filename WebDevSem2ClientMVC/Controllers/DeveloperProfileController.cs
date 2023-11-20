using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebDevSem2ClientMVC.Areas.Identity.Data;
using WebDevSem2ClientMVC.Models;

namespace WebDevSem2ClientMVC.Controllers
{
    public class DeveloperProfileController : Controller, IDeveloperProfileController
    {
        private readonly ApplicationDBContext _context;

        public DeveloperProfileController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: DeveloperProfiles
        public async Task<IActionResult> Index()
        {
            var developerProfiles = await _context.DeveloperProfile.ToListAsync();

            if (developerProfiles != null && developerProfiles.Any())
            {
                return View(developerProfiles);
            }

            return Problem("No developer profiles found.");
        }

        // GET: DeveloperProfiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DeveloperProfile == null)
            {
                return NotFound();
            }

            var developerProfile = await _context.DeveloperProfile
                .FirstOrDefaultAsync(m => m.DeveloperProfileId == id);
            if (developerProfile == null)
            {
                return NotFound();
            }

            return View(developerProfile);
        }

        // GET: DeveloperProfiles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DeveloperProfiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeveloperProfileId,Name,Skills,Discription,PictureURL,Email")] DeveloperProfile developerProfile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(developerProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(developerProfile);
        }
        [Authorize(Roles = "Admin,Manager")]
        // GET: DeveloperProfiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DeveloperProfile == null)
            {
                return NotFound();
            }

            var developerProfile = await _context.DeveloperProfile.FindAsync(id);
            if (developerProfile == null)
            {
                return NotFound();
            }
            return View(developerProfile);
        }

        // POST: DeveloperProfiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DeveloperProfileId,Name,Skills,Discription,PictureURL,Email")] DeveloperProfile developerProfile)
        {
            if (id != developerProfile.DeveloperProfileId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(developerProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeveloperProfileExists(developerProfile.DeveloperProfileId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(developerProfile);
        }
        [Authorize(Roles = "Admin,Manager")]
        // GET: DeveloperProfiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DeveloperProfile == null)
            {
                return NotFound();
            }

            var developerProfile = await _context.DeveloperProfile
                .FirstOrDefaultAsync(m => m.DeveloperProfileId == id);
            if (developerProfile == null)
            {
                return NotFound();
            }

            return View(developerProfile);
        }
        [Authorize(Roles = "Admin,Manager")]

        // POST: DeveloperProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DeveloperProfile == null)
            {
                return Problem("Entity set 'ApplicationDBContext.DeveloperProfile'  is null.");
            }
            var developerProfile = await _context.DeveloperProfile.FindAsync(id);
            if (developerProfile != null)
            {
                _context.DeveloperProfile.Remove(developerProfile);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeveloperProfileExists(int id)
        {
            return (_context.DeveloperProfile?.Any(e => e.DeveloperProfileId == id)).GetValueOrDefault();
        }
    }
}
