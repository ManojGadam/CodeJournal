namespace PersonalWebsite.Models
{
    public class Problem
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Difficulty { get; set; }
        public string? TitleSlug { get; set; }
        public string? Link { get; set; }
        public string? Comments { get; set; }
        public string? Tags { get; set; }
        public long ProblemNumber { get; set; }
        //public long RunTime { get; set; }
        //public long? Memory { get; set; }
       // public string? Code { get; set; }
    }
}
