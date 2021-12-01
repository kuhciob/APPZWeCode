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
    public class TaskResultsController : ControllerBase
    {
        private readonly APPZWeCodeContext _context;

        public TaskResultsController(APPZWeCodeContext context)
        {
            _context = context;
        }

        // GET: api/TaskResults
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskResult>>> GetTaskResults()
        {
            return await _context.TaskResults.ToListAsync();
        }

        // GET: api/TaskResults/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskResult>> GetTaskResult(int id)
        {
            var taskResult = await _context.TaskResults.FindAsync(id);

            if (taskResult == null)
            {
                return NotFound();
            }

            return taskResult;
        }

        // PUT: api/TaskResults/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskResult(int id, TaskResult taskResult)
        {
            if (id != taskResult.TaskResultId)
            {
                return BadRequest();
            }

            _context.Entry(taskResult).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskResultExists(id))
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

        // POST: api/TaskResults
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TaskResult>> PostTaskResult(TaskResult taskResult)
        {
            _context.TaskResults.Add(taskResult);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TaskResultExists(taskResult.TaskResultId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTaskResult", new { id = taskResult.TaskResultId }, taskResult);
        }

        // DELETE: api/TaskResults/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TaskResult>> DeleteTaskResult(int id)
        {
            var taskResult = await _context.TaskResults.FindAsync(id);
            if (taskResult == null)
            {
                return NotFound();
            }

            _context.TaskResults.Remove(taskResult);
            await _context.SaveChangesAsync();

            return taskResult;
        }

        private bool TaskResultExists(int id)
        {
            return _context.TaskResults.Any(e => e.TaskResultId == id);
        }
    }
}
