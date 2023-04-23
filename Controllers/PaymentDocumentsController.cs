using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Contexts;
using InsuranceAgency__.Models;

namespace InsuranceAgency__.Controllers
{
    public class PaymentDocumentsController : Controller
    {
        private readonly InsuranceAgencyDataContext _context;

        public PaymentDocumentsController(InsuranceAgencyDataContext context)
        {
            _context = context;
        }

        // GET: PaymentDocuments
        public async Task<IActionResult> Index()
        {
            return View(await _context.PaymentDocuments.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Index(int AccountNumber)
        {
            var paydoc = _context.PaymentDocuments.ToList().Where(p => p.AccountNumber == AccountNumber);
            return View(paydoc);
        }
        // GET: PaymentDocuments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentDocument = await _context.PaymentDocuments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentDocument == null)
            {
                return NotFound();
            }

            return View(paymentDocument);
        }

        // GET: PaymentDocuments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PaymentDocuments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AccountNumber,BeneficiaryBankName,InvoicingDate,NameOfService,ServiceAmount,FIOPolicyholder,PaymentMethod")] PaymentDocument paymentDocument)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paymentDocument);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paymentDocument);
        }

        // GET: PaymentDocuments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentDocument = await _context.PaymentDocuments.FindAsync(id);
            if (paymentDocument == null)
            {
                return NotFound();
            }
            return View(paymentDocument);
        }

        // POST: PaymentDocuments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AccountNumber,BeneficiaryBankName,InvoicingDate,NameOfService,ServiceAmount,FIOPolicyholder,PaymentMethod")] PaymentDocument paymentDocument)
        {
            if (id != paymentDocument.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentDocument);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentDocumentExists(paymentDocument.Id))
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
            return View(paymentDocument);
        }

        // GET: PaymentDocuments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentDocument = await _context.PaymentDocuments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentDocument == null)
            {
                return NotFound();
            }

            return View(paymentDocument);
        }

        // POST: PaymentDocuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paymentDocument = await _context.PaymentDocuments.FindAsync(id);
            _context.PaymentDocuments.Remove(paymentDocument);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentDocumentExists(int id)
        {
            return _context.PaymentDocuments.Any(e => e.Id == id);
        }
    }
}
