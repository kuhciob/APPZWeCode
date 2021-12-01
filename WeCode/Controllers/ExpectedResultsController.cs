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
    public class ExpectedResultsController : ControllerBase
    {
        private readonly APPZWeCodeContext _context;

        public ExpectedResultsController(APPZWeCodeContext context)
        {
            _context = context;
        }

        // GET: api/ExpectedResults
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpectedResult>>> GetExpectedResults()
        {
            return await _context.ExpectedResults.ToListAsync();
        }

        // GET: api/ExpectedResults/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpectedResult>> GetExpectedResult(int id)
        {
            var expectedResult = await _context.ExpectedResults.FindAsync(id);

            if (expectedResult == null)
            {
                return NotFound();
            }

            return expectedResult;
        }

        // PUT: api/ExpectedResults/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpectedResult(int id, ExpectedResult expectedResult)
        {
            if (id != expectedResult.ExpectedResultId)
            {
                return BadRequest();
            }

            _context.Entry(expectedResult).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpectedResultExists(id))
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

        // POST: api/ExpectedResults
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ExpectedResult>> PostExpectedResult(ExpectedResult expectedResult)
        {
            _context.ExpectedResults.Add(expectedResult);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ExpectedResultExists(expectedResult.ExpectedResultId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetExpectedResult", new { id = expectedResult.ExpectedResultId }, expectedResult);
        }

        // DELETE: api/ExpectedResults/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ExpectedResult>> DeleteExpectedResult(int id)
        {
            var expectedResult = await _context.ExpectedResults.FindAsync(id);
            if (expectedResult == null)
            {
                return NotFound();
            }

            _context.ExpectedResults.Remove(expectedResult);
            await _context.SaveChangesAsync();

            return expectedResult;
        }

        private bool ExpectedResultExists(int id)
        {
            return _context.ExpectedResults.Any(e => e.ExpectedResultId == id);
        }
    }
}
