using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LeaveRequestApp.Models;
using Microsoft.AspNetCore.Cors;

namespace LeaveRequestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class LeaveRequestApprovalsController : ControllerBase
    {
        private readonly LeaveAppDBContext _context;

        public LeaveRequestApprovalsController(LeaveAppDBContext context)
        {
            _context = context;
        }

        // GET: api/LeaveRequestApprovals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaveRequestApproval>>> GetLeaveRequestApproval()
        {
            return await _context.LeaveRequestApproval.ToListAsync();
        }

        // GET: api/LeaveRequestApprovals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveRequestApproval>> GetLeaveRequestApproval(int id)
        {
            var leaveRequestApproval = await _context.LeaveRequestApproval.FindAsync(id);

            if (leaveRequestApproval == null)
            {
                return NotFound();
            }

            return leaveRequestApproval;
        }

        // PUT: api/LeaveRequestApprovals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeaveRequestApproval(int id, LeaveRequestApproval leaveRequestApproval)
        {
            if (id != leaveRequestApproval.LeaveRequestApprovalId)
            {
                return BadRequest();
            }

            _context.Entry(leaveRequestApproval).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveRequestApprovalExists(id))
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

        // POST: api/LeaveRequestApprovals
        [HttpPost]
        public async Task<ActionResult<LeaveRequestApproval>> PostLeaveRequestApproval(LeaveRequestApproval leaveRequestApproval)
        {
            _context.LeaveRequestApproval.Add(leaveRequestApproval);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLeaveRequestApproval", new { id = leaveRequestApproval.LeaveRequestApprovalId }, leaveRequestApproval);
        }

        // DELETE: api/LeaveRequestApprovals/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LeaveRequestApproval>> DeleteLeaveRequestApproval(int id)
        {
            var leaveRequestApproval = await _context.LeaveRequestApproval.FindAsync(id);
            if (leaveRequestApproval == null)
            {
                return NotFound();
            }

            _context.LeaveRequestApproval.Remove(leaveRequestApproval);
            await _context.SaveChangesAsync();

            return leaveRequestApproval;
        }

        private bool LeaveRequestApprovalExists(int id)
        {
            return _context.LeaveRequestApproval.Any(e => e.LeaveRequestApprovalId == id);
        }
    }
}
