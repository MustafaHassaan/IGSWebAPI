using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IGS.Models;
using Microsoft.AspNetCore.Cors;

namespace IGS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class UsersController : ControllerBase
    {
        private readonly IGSCompanyContext _Con;

        public UsersController(IGSCompanyContext Con)
        {
            _Con = Con;
        }

        [HttpPost("Users")]
        public async Task<ActionResult<Users>> Login(Users user)
        {
            var users = await _Con.Users.Where(x => x.UserPhone.Equals(user.UserPhone) && x.UserPassword.Equals(user.UserPassword)).FirstOrDefaultAsync();
            if (users == null)
            {
                return NotFound();
            }
            return users;
        }

        [HttpPost("ForgetPassword")]
        public async Task<ActionResult<Users>> FP(Users users)
        {
            var mem = await _Con.Users.Where(x => x.UserPhone.Equals(users.UserPhone)).FirstOrDefaultAsync();
            if (mem == null)
            {
                return NotFound();
            }
            return mem;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _Con.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUsers(int id)
        {
            var users = await _Con.Users.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(int id, Users users)
        {
            if (id != users.Id)
            {
                return BadRequest();
            }
            _Con.Entry(users).State = EntityState.Modified;
            await _Con.SaveChangesAsync();
            return StatusCode(201);
        }

        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers(Users users)
        {
            //UP : user Phone
            var UP = await _Con.Users.Where(x => x.UserPhone == users.UserPhone).FirstOrDefaultAsync();
            if (UP != null)
            {
                return StatusCode(207);
            }
            _Con.Users.Add(users);
            await _Con.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = users.Id }, users);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Users>> DeleteUsers(int id)
        {
            var users = await _Con.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _Con.Users.Remove(users);
            await _Con.SaveChangesAsync();

            return users;
        }

        private bool UsersExists(int id)
        {
            return _Con.Users.Any(e => e.Id == id);
        }
    }
}
