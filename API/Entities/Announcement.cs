using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Announcements")]
    public class Announcement
    {
        public int Id { get; set; }
        public string Body { get; set; } // all text(info) about announcement
        public string Header { get; set; } // header of announcement
        // [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        // public DateTime? Created { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }
    }
}