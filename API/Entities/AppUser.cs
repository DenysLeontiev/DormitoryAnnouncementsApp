using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int DormitotyNumber { get; set; }
        public int DormitotyRoom { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        // [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        // public DateTime? LastActive { get; set; }
        public ICollection<Announcement> Announcements { get; set; }
    }
}