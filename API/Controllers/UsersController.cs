using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<AppUser>> GetUserById(int id)
        {
            var user = await _context.Users
                                             .Include(announcements => announcements.Announcements)
                                             .ThenInclude(photos => photos.Photos)
                                             .FirstOrDefaultAsync(u => u.Id == id);

            if(user == null) return NotFound("User is not found");

            return user;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            return await _context.Users
                                 .Include(announcements => announcements.Announcements)
                                 .ThenInclude(photos => photos.Photos)
                                 .ToListAsync();
        }
    }
}