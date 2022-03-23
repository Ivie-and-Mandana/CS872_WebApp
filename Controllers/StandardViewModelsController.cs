using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CS872_WebApp.Models;

namespace CS872_WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StandardViewModelsController : ControllerBase
    {
        private readonly ModelContext _context;

        public StandardViewModelsController(ModelContext context)
        {
            _context = context;
        }

        // GET: api/StandardViewModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StandardViewModel>>> GetStandardViewModel()
        {
            return await _context.StandardViewModel.ToListAsync();
        }

        // GET: api/StandardViewModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StandardViewModel>> GetStandardViewModel(string id)
        {
            var standardViewModel = await _context.StandardViewModel.FindAsync(id);

            if (standardViewModel == null)
            {
                return NotFound();
            }

            return standardViewModel;
        }

        // PUT: api/StandardViewModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStandardViewModel(string id, StandardViewModel standardViewModel)
        {
            if (id != standardViewModel.emailAddress)
            {
                return BadRequest();
            }

            _context.Entry(standardViewModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StandardViewModelExists(id))
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

        // POST: api/StandardViewModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StandardViewModel>> PostStandardViewModel(StandardViewModel standardViewModel)
        {
            _context.StandardViewModel.Add(standardViewModel);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StandardViewModelExists(standardViewModel.emailAddress))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetStandardViewModel", new { id = standardViewModel.emailAddress }, standardViewModel);
        }

        // DELETE: api/StandardViewModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStandardViewModel(string id)
        {
            var standardViewModel = await _context.StandardViewModel.FindAsync(id);
            if (standardViewModel == null)
            {
                return NotFound();
            }

            _context.StandardViewModel.Remove(standardViewModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StandardViewModelExists(string id)
        {
            return _context.StandardViewModel.Any(e => e.emailAddress == id);
        }
    }
}
