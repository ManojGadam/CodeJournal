using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PersonalWebsite.Models
{
    [Index(nameof(Name)),Index(nameof(Difficulty)),Index(nameof(UserId))]
    public class Problem
    {
        [Key]
        public int Id {  get; set; }
        public string Name { get; set; }
        public string? Difficulty { get; set; }
       // public string? TitleSlug { get; set; }
        public string? Link { get; set; }
        public string? Comments { get; set; }
        public string? Tags { get; set; }
        public long ProblemNumber { get; set; }
        public string? Code { get; set; }
        public string? MemoryPercentile {  get; set; }
        public string? TimePercentile { get; set; }
        public string? LangName {  get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int UserId { get; set; } // Explicit foreign key property
        public User User { get; set; }
        //public long RunTime { get; set; }
        //public long? Memory { get; set; }
       // public string? Code { get; set; }
    }
}
