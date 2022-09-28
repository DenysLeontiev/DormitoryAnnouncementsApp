using API.Entities;

namespace API.DTOs
{
    public class AnnouncementDto
    {
        public int Id { get; set; }
        public string Body { get; set; } // all text(info) about announcement
        public string Header { get; set; } // header of announcement
        // [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        // public DateTime? Created { get; set; }
        public ICollection<PhotoDto> Photos { get; set; }
    }
}