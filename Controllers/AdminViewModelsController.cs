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
    public class AdminViewModelsController : Controller
    {
        private readonly AdminDataContext _context;

        public AdminViewModelsController(AdminDataContext context)
        {
            _context = context;
        }

        // GET: AdminViewModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.AdminViewModel.ToListAsync());
        }

        // GET: AdminViewModels/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminViewModel = await _context.AdminViewModel
                .FirstOrDefaultAsync(m => m.emailAddress == id);
            if (adminViewModel == null)
            {
                return NotFound();
            }

            return View(adminViewModel);
        }

        // GET: AdminViewModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("userType,password,status,userType,emailAddress,firstName,lastName,fullName,address,city,province,postalCode")] AdminViewModel adminViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adminViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adminViewModel);
        }

        // GET: AdminViewModels/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminViewModel = await _context.AdminViewModel.FindAsync(id);
            if (adminViewModel == null)
            {
                return NotFound();
            }
            return View(adminViewModel);
        }

        // POST: AdminViewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("userType,password,status,userType,emailAddress,firstName,lastName,fullName,address,city,province,postalCode")] AdminViewModel adminViewModel)
        {
            if (id != adminViewModel.emailAddress)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adminViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminViewModelExists(adminViewModel.emailAddress))
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
            return View(adminViewModel);
        }

        // GET: AdminViewModels/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminViewModel = await _context.AdminViewModel
                .FirstOrDefaultAsync(m => m.emailAddress == id);
            if (adminViewModel == null)
            {
                return NotFound();
            }

            return View(adminViewModel);
        }

        // POST: AdminViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var adminViewModel = await _context.AdminViewModel.FindAsync(id);
            _context.AdminViewModel.Remove(adminViewModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminViewModelExists(string id)
        {
            return _context.AdminViewModel.Any(e => e.emailAddress == id);
        }
    }
}
