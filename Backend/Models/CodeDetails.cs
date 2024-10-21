namespace PersonalWebsite.Models
{
    public class CodeDetails
    {
        public CodeData data { get; set; }
    }
    public class CodeData
    {
        public CodeSubmissionDetails submissionDetails { get; set; }
    }
    public class CodeSubmissionDetails
    {
        public string code { get; set; }
        public long runTimePercentile { get; set; }
        public long runTime { get; set; }
        public long? memoryPercentile { get; set; }
    }
}
