using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
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

        //Get:api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> BuscarTodosUsuarios()
        {
            return await _context.Users.ToListAsync();
        }

        //Get:api/Users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> BuscarUsuarioPorId(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);

        }

        //Post:api/Users
        [HttpPost]
        public async Task<ActionResult<Users>> AdicionarUsuario(Users user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        //Put:api/Users/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Users>> AtualizarUsuario(Users user, int id)
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
        [HttpDelete("{id}")]
        public async Task<ActionResult<Users>> DeletarUsuario(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) 
            { 
                return NotFound();
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
