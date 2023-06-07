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
    public class UserRolesController : ControllerBase
    {
        private readonly LeaveAppDBContext _context;
        private readonly ILogger<UserRolesController> _logger;

        public UserRolesController(LeaveAppDBContext context, ILogger<UserRolesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/UserRoles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRole>>> GetUserRole()
        {
            _logger.LogInformation("GetUserRole Started");
            var userRoles = await (from ur in _context.UserRole
                        join u in _context.User
                             on ur.UserId equals u.UserId
                        join r in _context.Role
                            on ur.RoleId equals r.RoleId
                        select new UserRole
                        {
                            UserRoleId = ur.UserRoleId,
                            UserId = ur.UserId,
                            RoleId = ur.RoleId,
                            TimeStamp = ur.TimeStamp,
                            User = u,
                            Role = r
                        }).ToListAsync();

            //var userRoles = await _context.UserRole.ToListAsync(); 

            return userRoles;
        }

        // GET: api/UserRoles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserRole>> GetUserRole(int id)
        {
            //var userRole = await _context.UserRole.FindAsync(id);

            var userRole = await (from ur in _context.UserRole
                              join u in _context.User
                                   on ur.UserId equals u.UserId
                              join r in _context.Role
                                  on ur.RoleId equals r.RoleId
                              where ur.UserRoleId == id
                              select new UserRole
                              {
                                  UserRoleId = ur.UserRoleId,
                                  UserId = ur.UserId,
                                  RoleId = ur.RoleId,
                                  TimeStamp = ur.TimeStamp,
                                  User = u,
                                  Role = r
                              }).FirstOrDefaultAsync();

            if (userRole == null)
            {
                return NotFound();
            }

            return userRole;
        }

        // PUT: api/UserRoles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserRole(int id, UserRole userRole)
        {
            if (id != userRole.UserRoleId)
            {
                return BadRequest();
            }

            _context.Entry(userRole).State = EntityState.Modified;

            userRole.TimeStamp = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserRoleExists(id))
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

        // POST: api/UserRoles
        [HttpPost]
        public async Task<ActionResult<UserRole>> PostUserRole(UserRole userRole)
        {
            userRole.TimeStamp = DateTime.UtcNow;
            _context.UserRole.Add(userRole);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserRole", new { id = userRole.UserRoleId }, userRole);
        }

        // DELETE: api/UserRoles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserRole>> DeleteUserRole(int id)
        {
            var userRole = await _context.UserRole.FindAsync(id);
            if (userRole == null)
            {
                return NotFound();
            }

            _context.UserRole.Remove(userRole);
            await _context.SaveChangesAsync();

            return userRole;
        }

        private bool UserRoleExists(int id)
        {
            return _context.UserRole.Any(e => e.UserRoleId == id);
        }
    }
}
