using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PersonalWebsite.Models
{
    [Index(nameof(AuthId))]
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string AuthId { get; set; }
        public ICollection<Problem> Problems { get; set; }
    }
}
