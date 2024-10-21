namespace PersonalWebsite.Models
{
    public class topicTag
    {
        public string name { get; set; }
        public string slug { get; set; }
    }

    public class question
    {
        public List<topicTag> TopicTags { get; set; }
    }

    public class data
    {
        public question Question { get; set; }
    }

    public class GraphQLResponse
    {
        public data Data { get; set; }
    }
}
