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
    public class RequestsLinesController : ControllerBase
    {
        private readonly PRSdbcontex _context;

        public RequestsLinesController(PRSdbcontex context)
        {
            _context = context;
        }

        // GET: api/RequestsLines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestsLine>>> GetRequestsLine()
        {
          if (_context.RequestsLine == null)
          {
              return NotFound();
          }
            return await _context.RequestsLine.ToListAsync();
        }

        // GET: api/RequestsLines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RequestsLine>> GetRequestsLine(int id)
        {
          if (_context.RequestsLine == null)
          {
              return NotFound();
          }
            var requestsLine = await _context.RequestsLine.FindAsync(id);

            if (requestsLine == null)
            {
                return NotFound();
            }

            return requestsLine;
        }

        // PUT: api/RequestsLines/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequestsLine(int id, RequestsLine requestsLine)
        {
            if (id != requestsLine.Id)
            {
                return BadRequest();
            }

            _context.Entry(requestsLine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestsLineExists(id))
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

        // POST: api/RequestsLines
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RequestsLine>> PostRequestsLine(RequestsLine requestsLine)
        {
          if (_context.RequestsLine == null)
          {
              return Problem("Entity set 'PRSdbcontex.RequestsLine'  is null.");
          }
            _context.RequestsLine.Add(requestsLine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequestsLine", new { id = requestsLine.Id }, requestsLine);
        }

        // DELETE: api/RequestsLines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequestsLine(int id)
        {
            if (_context.RequestsLine == null)
            {
                return NotFound();
            }
            var requestsLine = await _context.RequestsLine.FindAsync(id);
            if (requestsLine == null)
            {
                return NotFound();
            }

            _context.RequestsLine.Remove(requestsLine);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestsLineExists(int id)
        {
            return (_context.RequestsLine?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
