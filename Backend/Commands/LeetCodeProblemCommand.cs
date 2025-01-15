namespace PersonalWebsite.Views
{
    public class LeetCodeProblemCommand
    {
        public long ProblemNumber { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string? URL { get; set; }
        public string? Difficulty {  get; set; }
        public string? Tags {  get; set; }
        public string? Comments {  get; set; }
        public string? Code { get; set; }
        public string? RunTimePercentile {  get; set; }
        public string? MemoryPercentile { get; set; }
        public string? LangName {  get; set; }
    }
}
