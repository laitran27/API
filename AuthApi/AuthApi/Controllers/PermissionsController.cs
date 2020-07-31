using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuthApi.Models;

namespace AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly RoleContext _context;

        public PermissionsController(RoleContext context)
        {
            _context = context;
        }

        // GET: api/Permissions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Permissions>>> GetPermissions()
        {
            return await _context.Permissions.ToListAsync();
        }

        // GET: api/Permissions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Permissions>> GetPermissions(int id)
        {
            var permissions = await _context.Permissions.FindAsync(id);

            if (permissions == null)
            {
                return NotFound();
            }

            return permissions;
        }

        // PUT: api/Permissions/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("getRolePermission/{id}")]
        public async Task<IActionResult> PutPermissions(int id, Permissions permissions)
        {
            if (id != permissions.Id)
            {
                return BadRequest();
            }

            _context.Entry(permissions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermissionsExists(id))
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

        // POST: api/Permissions
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Permissions>> PostPermissions(Permissions permissions)
        {
            _context.Permissions.Add(permissions);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetPermissions", new { id = permissions.Id }, permissions);
            return CreatedAtAction(nameof(GetPermissions), new { id = permissions.Id }, permissions);
        }

        // DELETE: api/Permissions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Permissions>> DeletePermissions(int id)
        {
            var permissions = await _context.Permissions.FindAsync(id);
            if (permissions == null)
            {
                return NotFound();
            }

            _context.Permissions.Remove(permissions);
            await _context.SaveChangesAsync();

            return permissions;
        }

        private bool PermissionsExists(int id)
        {
            return _context.Permissions.Any(e => e.Id == id);
        }
    }
}
