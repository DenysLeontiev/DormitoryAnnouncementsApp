using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Photos")]
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string IsMain { get; set; }
        public string PublicId { get; set; }
        public Announcement Announcement { get; set; }
        public int AnnouncementId { get; set; }
    }
}