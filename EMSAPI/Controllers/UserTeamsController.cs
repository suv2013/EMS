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
    public class UserTeamsController : ControllerBase
    {
        private readonly LeaveAppDBContext _context;

        public UserTeamsController(LeaveAppDBContext context)
        {
            _context = context;
        }

        // GET: api/UserTeams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTeam>>> GetUserTeam()
        {
            var userTeams = await (from ur in _context.UserTeam
                                   join u in _context.User
                                        on ur.UserId equals u.UserId
                                   join t in _context.Team
                                       on ur.TeamId equals t.TeamId
                                   select new UserTeam
                                   {
                                       UserTeamId = ur.UserTeamId,
                                       UserId = ur.UserId,
                                       TeamId = ur.TeamId,
                                       TimeStamp = ur.TimeStamp,
                                       User = u,
                                       Team = t
                                   }).ToListAsync();
            //return await _context.UserTeam.ToListAsync();

            return userTeams;
        }

        // GET: api/UserTeams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserTeam>> GetUserTeam(int id)
        {
            //var userTeam = await _context.UserTeam.FindAsync(id);

            var userTeam = await (from ur in _context.UserTeam
                                  join u in _context.User
                                       on ur.UserId equals u.UserId
                                  join t in _context.Team
                                      on ur.TeamId equals t.TeamId
                                  where ur.UserTeamId == id
                                  select new UserTeam
                                  {
                                      UserTeamId = ur.UserTeamId,
                                      UserId = ur.UserId,
                                      TeamId = ur.TeamId,
                                      TimeStamp = ur.TimeStamp,
                                      User = u,
                                      Team = t
                                  }).FirstOrDefaultAsync();

            if (userTeam == null)
            {
                return NotFound();
            }

            return userTeam;
        }

        // PUT: api/UserTeams/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserTeam(int id, UserTeam userTeam)
        {
            if (id != userTeam.UserTeamId)
            {
                return BadRequest();
            }
            userTeam.TimeStamp = DateTime.UtcNow;
            _context.Entry(userTeam).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserTeamExists(id))
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

        // POST: api/UserTeams
        [HttpPost]
        public async Task<ActionResult<UserTeam>> PostUserTeam(UserTeam userTeam)
        {
            userTeam.TimeStamp = DateTime.UtcNow;
            _context.UserTeam.Add(userTeam);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserTeam", new { id = userTeam.UserTeamId }, userTeam);
        }

        // DELETE: api/UserTeams/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserTeam>> DeleteUserTeam(int id)
        {
            var userTeam = await _context.UserTeam.FindAsync(id);
            if (userTeam == null)
            {
                return NotFound();
            }

            _context.UserTeam.Remove(userTeam);
            await _context.SaveChangesAsync();

            return userTeam;
        }

        private bool UserTeamExists(int id)
        {
            return _context.UserTeam.Any(e => e.UserTeamId == id);
        }
    }
}
