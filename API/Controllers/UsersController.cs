using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UsersController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            var user = await _context.Users
                                             .Include(announcements => announcements.Announcements)
                                             .ThenInclude(photos => photos.Photos)
                                             .FirstOrDefaultAsync(u => u.Id == id);

            if(user == null) return NotFound("User is not found");

            var userToReturn = _mapper.Map<UserDto>(user);
            return userToReturn;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users =  await _context.Users
                                 .Include(announcements => announcements.Announcements)
                                 .ThenInclude(photos => photos.Photos)
                                 .ToListAsync();
            var usersToReturn = _mapper.Map<List<UserDto>>(users);
            return usersToReturn;
        }
    }
}