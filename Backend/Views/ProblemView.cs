namespace PersonalWebsite.Views
{
    public class ProblemView
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Difficulty { get; set; }
        public string? Link { get; set; }
        public string? Comments { get; set; }
        public string? Tags { get; set; }
        public long ProblemNumber { get; set; }
    }
}
