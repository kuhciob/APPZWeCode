using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeCode;

namespace WeCode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActualResultsController : ControllerBase
    {
        private readonly APPZWeCodeContext _context;

        public ActualResultsController(APPZWeCodeContext context)
        {
            _context = context;
        }

        // GET: api/ActualResults
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActualResult>>> GetActualResults()
        {
            return await _context.ActualResults.ToListAsync();
        }

        // GET: api/ActualResults/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActualResult>> GetActualResult(int id)
        {
            var actualResult = await _context.ActualResults.FindAsync(id);

            if (actualResult == null)
            {
                return NotFound();
            }

            return actualResult;
        }

        // PUT: api/ActualResults/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActualResult(int id, ActualResult actualResult)
        {
            if (id != actualResult.ActualResultId)
            {
                return BadRequest();
            }

            _context.Entry(actualResult).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActualResultExists(id))
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

        // POST: api/ActualResults
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ActualResult>> PostActualResult(ActualResult actualResult)
        {
            _context.ActualResults.Add(actualResult);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ActualResultExists(actualResult.ActualResultId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetActualResult", new { id = actualResult.ActualResultId }, actualResult);
        }

        // DELETE: api/ActualResults/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ActualResult>> DeleteActualResult(int id)
        {
            var actualResult = await _context.ActualResults.FindAsync(id);
            if (actualResult == null)
            {
                return NotFound();
            }

            _context.ActualResults.Remove(actualResult);
            await _context.SaveChangesAsync();

            return actualResult;
        }

        private bool ActualResultExists(int id)
        {
            return _context.ActualResults.Any(e => e.ActualResultId == id);
        }
    }
}
