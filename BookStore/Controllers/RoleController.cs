using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly bookstoreContext _context;
        private readonly IMapper _mapper;

        public RoleController(bookstoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Role
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        // GET: api/Role/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRole(int id)
        {
            var role = await _context.Roles.FindAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            return role;
        }

        // PUT: api/Role/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutRole(int id, Role role)
        //{
        //    if (id != role.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(role).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!RoleExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Role
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Role>> PostRole(Role role)
        //{
        //    _context.Roles.Add(role);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetRole", new { id = role.Id }, role);
        //}

        // DELETE: api/Role/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteRole(int id)
        //{
        //    var role = await _context.Roles.FindAsync(id);
        //    if (role == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Roles.Remove(role);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool RoleExists(int id)
        //{
        //    return _context.Roles.Any(e => e.Id == id);
        //}

        //[HttpGet]
        //[Route("test")]
        //[Authorize(Roles = "User,Admin")]
        //public async Task<ActionResult<IList<RoleViewModel>>> GetRolesTest()
        //{
        //    var roles = await _context.Roles.ToListAsync();
        //    var vm = _mapper.Map<IList<Role>, IList<RoleViewModel>>(roles);
        //    return Ok(vm);
        //    //return await _context.Roles.Include(u => u.Users).ToListAsync();
        //}
    }
}
