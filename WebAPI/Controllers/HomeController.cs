using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly _Context _context;
        private bool UserExists(int id)
        {
            return _context.Users.Any(x => x.Id == id);
        }
        public HomeController(_Context context)
        {
            _context = context;
        }
        //Get:http://localhost:5000/api/Users
        [HttpGet("/api/User")]
        public async Task<ActionResult<IEnumerable<Users>>> BuscarTodosUsuarios()
        {
            return await _context.Users.ToListAsync();
        }
        //Get:http://localhost:5000/api/Users/{id}
        [HttpGet("/api/User/{id}")]
        public async Task<ActionResult<Users>> BuscarUsuarioPorId(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user is null)
            {
                return NotFound();
            }
            return Ok(user);

        }
        //Post:api/Users
        [HttpPost("/api/User")]
        public async Task<ActionResult<Users>> AdicionarUsuario([FromBody]Users user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }
        //Put:http://localhost:5000/api/Users/{id}
        [HttpPut("/api/User/{id}")]
        public async Task<ActionResult<Users>> AtualizarUsuario([FromBody]Users user, int id)
        {
            if(id != user.Id) 
            {
                return BadRequest();
            }
            _context.Entry(user).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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
        //Delete:http://localhost:5000/api/Users/{id}
        [HttpDelete("/api/User/{id}")]
        public async Task<ActionResult<Users>> DeletarUsuario(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user is null) 
            { 
                return NotFound();
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
