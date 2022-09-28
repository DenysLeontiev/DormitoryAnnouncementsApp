using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]public int DormitotyNumber { get; set; }
        [Required]public int DormitotyRoom { get; set; }
        [Required]
        public string Password { get; set; }
    }
}