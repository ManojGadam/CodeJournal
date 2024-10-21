namespace PersonalWebsite.Models
{
    public class submissions
    {
        public long Id { get; set; }
        public string? flagType { get; set; }
        public string? isPending { get; set; }
        public string? lang { get; set; }     
        public string? langName { get; set; }
        public string? memory { get; set; }
        public string? runtime { get; set; }
        public int status { get; set; }    
        public string? statusDisplay { get; set; }
        public string? timestamp { get; set; }
        public string? title { get; set; }
        public CodeDetails? codeDetails { get; set; }

    }
    public class questionSubmissionList
    {
        public bool hasNext { get; set; }
        public int? lastKey { get; set; }
        public List<submissions> Submissions { get;set; }
    }
    public class submissionData
    {
        public questionSubmissionList questionSubmissionList { get; set; }
    }
    public class SubmissionResponse
    {
        public submissionData data { get; set; }
    }
}
