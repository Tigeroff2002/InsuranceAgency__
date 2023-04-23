using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Contexts;
using InsuranceAgency__.Models;
using Microsoft.AspNetCore.Authorization;

namespace InsuranceAgency__.Controllers
{
    public class ApplicationsController : Controller
    {
        private readonly InsuranceAgencyDataContext _context;

        public ApplicationsController(InsuranceAgencyDataContext context)
        {
            _context = context;
        }

        // GET: Applications
        public async Task<IActionResult> Index()
        {
            return View(await _context.Applicationss.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Index(int PassportNumber)
        {
            var application = _context.Applicationss.ToList().Where(p => p.PassportNumber == PassportNumber);
            return View(application);
        }
        // GET: Applications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applications = await _context.Applicationss
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applications == null)
            {
                return NotFound();
            }

            return View(applications);
        }

        // GET: Applications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FIO,PassportNumber,PassportSeries,ContractValidity,InsuranceType,InsuranceAmount,ApplicationStatus")] Applications applications)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applications);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(applications);
        }

        // GET: Applications/Edit/5
        [Authorize(Roles = "Страховой агент")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applications = await _context.Applicationss.FindAsync(id);
            if (applications == null)
            {
                return NotFound();
            }
            return View(applications);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Страховой агент")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FIO,PassportNumber,PassportSeries,ContractValidity,InsuranceType,InsuranceAmount,ApplicationStatus")] Applications applications)
        {
            if (id != applications.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applications);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationsExists(applications.Id))
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
            return View(applications);
        }

        // GET: Applications/Delete/5
        [Authorize(Roles = "Страховой агент")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applications = await _context.Applicationss
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applications == null)
            {
                return NotFound();
            }

            return View(applications);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Страховой агент")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applications = await _context.Applicationss.FindAsync(id);
            _context.Applicationss.Remove(applications);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationsExists(int id)
        {
            return _context.Applicationss.Any(e => e.Id == id);
        }
    }
}
