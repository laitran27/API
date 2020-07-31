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
    public class RolesController : ControllerBase
    {
        private readonly RoleContext _context;

        public RolesController(RoleContext context)
        {
            _context = context;
        }

        // GET: api/Roles
        [HttpGet]
        public object GetRoles()
        {
        
            var obj = new List<RolePermissions>()
            {
               new RolePermissions(){  Id = 1,
                Name = "Administrator",
                isCoreRole = true,
                Permissions = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 }
               },
               new RolePermissions(){  Id = 2,
                Name = "Manager",
                isCoreRole = true,
                Permissions = new int[] {  3, 4, 10 }
               },
               new RolePermissions(){  Id = 3,
                Name = "Guest",
                isCoreRole = true,
                Permissions = new int[] {  }
               },
            };
            return obj;
        }
        public class RolePermissions
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool isCoreRole { get; set; }
            public int[] Permissions { get; set; }
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Roles>> GetRoles(int id)
        {
            var roles = await _context.Roles.FindAsync(id);

            if (roles == null)
            {
                return NotFound();
            }

            return roles;
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoles(int id, Roles roles)
        {
            if (id != roles.Id)
            {
                return BadRequest();
            }

            _context.Entry(roles).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolesExists(id))
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

        // POST: api/Roles
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Roles>> PostRoles(Roles roles)
        {
            _context.Roles.Add(roles);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoles", new { id = roles.Id }, roles);
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Roles>> DeleteRoles(int id)
        {
            var roles = await _context.Roles.FindAsync(id);
            if (roles == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(roles);
            await _context.SaveChangesAsync();

            return roles;
        }

        private bool RolesExists(int id)
        {
            return _context.Roles.Any(e => e.Id == id);
        }
    }
}
