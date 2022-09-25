using System.Text;
using System.Security.Cryptography;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Interfaces;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if(await _context.Users.AnyAsync(x => x.UserName == registerDto.Username.ToLower())) 
                return BadRequest("Sorry,but the username is taken");

            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                Email = registerDto.Email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key,
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                Email = user.Email, 
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users
                .Include(user => user.Announcements)
                .ThenInclude(announcement => announcement.Photos)
                .FirstOrDefaultAsync(predicate: x => x.UserName == loginDto.Username && x.Email == loginDto.Email);
            if(user == null) return NotFound("user not found");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if(user.PasswordHash[i] != computedHash[i]) return Unauthorized("Wrong password");
            }

            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                Email = user.Email,
            };
        }
    }
}