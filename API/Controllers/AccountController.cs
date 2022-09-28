using System.Text;
using System.Security.Cryptography;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Interfaces;
using AutoMapper;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public AccountController(DataContext context, ITokenService tokenService, IMapper mapper)
        {
            _context = context;
            _tokenService = tokenService;
            _mapper = mapper;
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
                DormitotyNumber = registerDto.DormitotyNumber,
                DormitotyRoom = registerDto.DormitotyRoom,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key,
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var userToReturn = _mapper.Map<UserDto>(user);
            userToReturn.Token = _tokenService.CreateToken(user);
            return userToReturn;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            AppUser user = await _context.Users
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

            var userToReturn = _mapper.Map<UserDto>(user);
            userToReturn.Token = _tokenService.CreateToken(user);
            // return userToReturn;

            return new UserDto
            {
                Username = user.UserName,
                Id = user.Id,
                Token = _tokenService.CreateToken(user),
                Email = user.Email,
                DormitotyNumber = user.DormitotyNumber,
                DormitotyRoom = user.DormitotyRoom,
                Announcements = _mapper.Map<ICollection<AnnouncementDto>>(user.Announcements)
            };
        }
    }
}