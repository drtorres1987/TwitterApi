using System.Collections.Generic;

namespace Twitter.Service.Models.Twitter
{
    public class TweetInfoEntity
    {
        public IEnumerable<HashTag> HashTags { get; set; } = new List<HashTag>();
        public IEnumerable<Mention> Mentions { get; set; } = new List<Mention>();
    }
}