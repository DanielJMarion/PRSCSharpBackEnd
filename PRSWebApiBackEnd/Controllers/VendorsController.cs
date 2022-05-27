using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRSWebApiBackEnd.Models;

namespace PRSWebApiBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        private readonly PRSdbcontex _context;

        public VendorsController(PRSdbcontex context)
        {
            _context = context;
        }

        // GET: api/Vendors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vendors>>> GetVendors()
        {
          if (_context.Vendors == null)
          {
              return NotFound();
          }
            return await _context.Vendors.ToListAsync();
        }

        // GET: api/Vendors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vendors>> GetVendors(int id)
        {
          if (_context.Vendors == null)
          {
              return NotFound();
          }
            var vendors = await _context.Vendors.FindAsync(id);

            if (vendors == null)
            {
                return NotFound();
            }

            return vendors;
        }

        // PUT: api/Vendors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVendors(int id, Vendors vendors)
        {
            if (id != vendors.Id)
            {
                return BadRequest();
            }

            _context.Entry(vendors).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorsExists(id))
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

        // POST: api/Vendors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vendors>> PostVendors(Vendors vendors)
        {
          if (_context.Vendors == null)
          {
              return Problem("Entity set 'PRSdbcontex.Vendors'  is null.");
          }
            _context.Vendors.Add(vendors);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVendors", new { id = vendors.Id }, vendors);
        }

        // DELETE: api/Vendors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendors(int id)
        {
            if (_context.Vendors == null)
            {
                return NotFound();
            }
            var vendors = await _context.Vendors.FindAsync(id);
            if (vendors == null)
            {
                return NotFound();
            }

            _context.Vendors.Remove(vendors);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VendorsExists(int id)
        {
            return (_context.Vendors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
