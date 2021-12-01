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
    public class CodeBlocksController : ControllerBase
    {
        private readonly APPZWeCodeContext _context;

        public CodeBlocksController(APPZWeCodeContext context)
        {
            _context = context;
        }

        // GET: api/CodeBlocks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CodeBlock>>> GetCodeBlocks()
        {
            return await _context.CodeBlocks.ToListAsync();
        }

        // GET: api/CodeBlocks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CodeBlock>> GetCodeBlock(int id)
        {
            var codeBlock = await _context.CodeBlocks.FindAsync(id);

            if (codeBlock == null)
            {
                return NotFound();
            }

            return codeBlock;
        }

        // PUT: api/CodeBlocks/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCodeBlock(int id, CodeBlock codeBlock)
        {
            if (id != codeBlock.CodeBlockId)
            {
                return BadRequest();
            }

            _context.Entry(codeBlock).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CodeBlockExists(id))
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

        // POST: api/CodeBlocks
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CodeBlock>> PostCodeBlock(CodeBlock codeBlock)
        {
            _context.CodeBlocks.Add(codeBlock);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CodeBlockExists(codeBlock.CodeBlockId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCodeBlock", new { id = codeBlock.CodeBlockId }, codeBlock);
        }

        // DELETE: api/CodeBlocks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CodeBlock>> DeleteCodeBlock(int id)
        {
            var codeBlock = await _context.CodeBlocks.FindAsync(id);
            if (codeBlock == null)
            {
                return NotFound();
            }

            _context.CodeBlocks.Remove(codeBlock);
            await _context.SaveChangesAsync();

            return codeBlock;
        }

        private bool CodeBlockExists(int id)
        {
            return _context.CodeBlocks.Any(e => e.CodeBlockId == id);
        }
    }
}
