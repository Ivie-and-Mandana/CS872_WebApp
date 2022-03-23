using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CS872_WebApp.Models;

namespace CS872_WebApp.Controllers
{
    public class BillViewModelsController : Controller
    {
        private readonly BillDataContext _context;

        public BillViewModelsController(BillDataContext context)
        {
            _context = context;
        }

        // GET: BillViewModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.BillViewModel.ToListAsync());
        }

        // GET: BillViewModels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billViewModel = await _context.BillViewModel
                .FirstOrDefaultAsync(m => m.billID == id);
            if (billViewModel == null)
            {
                return NotFound();
            }

            return View(billViewModel);
        }

        // GET: BillViewModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BillViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("billID,emailAddress,billDateTIme,amount,billStatus,houseArea,numberOfRooms,numberOfChildren,numberOfPeople,isAirCondtion,isTelevision,isFlat,isUrban")] BillViewModel billViewModel)
        {
            if (ModelState.IsValid)
            {
                billViewModel.billID = Guid.NewGuid();
                _context.Add(billViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(billViewModel);
        }

        // GET: BillViewModels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billViewModel = await _context.BillViewModel.FindAsync(id);
            if (billViewModel == null)
            {
                return NotFound();
            }
            return View(billViewModel);
        }

        // POST: BillViewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("billID,emailAddress,billDateTIme,amount,billStatus,houseArea,numberOfRooms,numberOfChildren,numberOfPeople,isAirCondtion,isTelevision,isFlat,isUrban")] BillViewModel billViewModel)
        {
            if (id != billViewModel.billID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(billViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillViewModelExists(billViewModel.billID))
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
            return View(billViewModel);
        }

        // GET: BillViewModels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billViewModel = await _context.BillViewModel
                .FirstOrDefaultAsync(m => m.billID == id);
            if (billViewModel == null)
            {
                return NotFound();
            }

            return View(billViewModel);
        }

        // POST: BillViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var billViewModel = await _context.BillViewModel.FindAsync(id);
            _context.BillViewModel.Remove(billViewModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillViewModelExists(Guid id)
        {
            return _context.BillViewModel.Any(e => e.billID == id);
        }
    }
}
