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
    public class RequestsController : ControllerBase
    {
        private readonly PRSdbcontex _context;

        public RequestsController(PRSdbcontex context)
        {
            _context = context;
        }

        //lists requests in review but not the ones in review made by the request reviewer
        [HttpGet("Review/{id}")]
        public async Task<ActionResult<IEnumerable<Requests>>> GetRequestsForReview(int id)
        { 
            if (_context.Requests == null)
            {
                return NotFound();
            }
            return await _context.Requests.Where(r => r.Status == "REVIEW" && r.UserId != id).ToListAsync();

        }
        // GET: api/Requests
        [HttpGet ("Request")]
        public async Task<ActionResult<IEnumerable<Requests>>> GetRequest(int id)
        {
          if (_context.Requests == null)
          {
              return NotFound();
          }
            return await _context.Requests.ToListAsync();
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Requests>> GetRequests(int id)
        {
          if (_context.Requests == null)
          {
              return NotFound();
          }
            var requests = await _context.Requests.FindAsync(id);

            if (requests == null)
            {
                return NotFound();
            }

            return requests;
        }

        //shows request in review status //50 DOLLARS OR LESS
        [HttpPut("{id}/REVIEW")]
        public async Task<IActionResult> Reviews(int id)
        {

            var request = await _context.Requests.FindAsync(id); 

            if (request == null)
            {
                return NotFound();
            } 
            if (request.Total <= 50)
            {
                request.Status = "APPROVED"; 

            } 
            else
            {
                request.Status = "REVIEW";
            }
            _context.SaveChanges();
            return NoContent();
    }
        //shows requests in approved status
        [HttpPut("{id}/APPROVE")]
        public async Task<IActionResult> Approve(int id)
        {

            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }
            request.Status = "APPROVED";
            _context.SaveChanges();
            return NoContent();
        }

        //shows review set to rejected status
        [HttpPut("{id}/REJECT")]
        public async Task<IActionResult> Reject(int id)
        {

            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }
            request.Status = "REJECTED";
            _context.SaveChanges();
            return NoContent();
        }

        // PUT: api/Requests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequests(int id, Requests requests)
        {
            if (id != requests.Id)
            {
                return BadRequest();
            }

            _context.Entry(requests).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestsExists(id))
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

        // POST: api/Requests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Requests>> PostRequests(Requests requests)
        {
          if (_context.Requests == null)
          {
              return Problem("Entity set 'PRSdbcontex.Requests'  is null.");
          }
            _context.Requests.Add(requests);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequests", new { id = requests.Id }, requests);
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequests(int id)
        {
            if (_context.Requests == null)
            {
                return NotFound();
            }
            var requests = await _context.Requests.FindAsync(id);
            if (requests == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(requests);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestsExists(int id)
        {
            return (_context.Requests?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
