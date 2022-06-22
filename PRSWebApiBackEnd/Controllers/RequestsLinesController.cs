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
    [Route("api/RequestLines")]
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
            RecalculateRequestTotal(requestsLine.RequestId);
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

            RecalculateRequestTotal(requestsLine.RequestId);

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
            int requestid = requestsLine.RequestId;
            _context.RequestsLine.Remove(requestsLine);
            await _context.SaveChangesAsync();
            RecalculateRequestTotal(requestid);
            return NoContent();
        }
        ////Recals total in requests whenever an insert, update, or
        ////delete occurs modifier is set to private for security cannot be called out of Requestlines
     
        private bool RequestsLineExists(int id)
        {
            return (_context.RequestsLine?.Any(e => e.Id == id)).GetValueOrDefault();
        }
            private void RecalculateRequestTotal(int requestId)
        {
            
            decimal total = _context.RequestsLine   
                .Include(r => r.Requests)
                .Where(r => r.RequestId== requestId)
                .Sum(r=> r.Product.Price*r.Quantity);
            
            var request =  _context.Requests.FirstOrDefault(r=> r.Id== requestId); 
            if (request == null)
            {
                BadRequest();
            }
            
            request.Total=total;
            _context.SaveChanges();


        }
    
    
    }


}
