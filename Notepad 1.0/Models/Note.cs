using System.ComponentModel.DataAnnotations;

namespace Notepad_1._0.Models
{
    public class Note
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }

        public DateTime CreatedDate = DateTime.Now;

    }
}
