namespace PersonalWebsite.Models
{
    public class Config
    {
        public int Id { get; set; }
        public string GitToken { get; set; }
        public string GitURL { get; set; }
        public int UserId {  get; set; }
        public User User { get; set; }
    }
}
