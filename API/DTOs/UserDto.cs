using API.Entities;

namespace API.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int DormitotyNumber { get; set; }
        public int DormitotyRoom { get; set; }
        public string Token { get; set; }
        public ICollection<AnnouncementDto> Announcements { get; set; }
    }
}