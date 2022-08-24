namespace Twitter.Service.Models.Twitter
{
    public class Mention
    {
        public int Start { get; set; }
        public int End { get; set; }
        public string Username { get; set; }
        public string Id { get; set; }
    }
}