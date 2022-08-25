using System.Collections.Generic;

namespace Twitter.Service.Models.Report
{
    public class HashTagReport
    {
        public int TotalTweets { get; set; }
        public IEnumerable<HashTagRecord> HashTags { get; set; } = new List<HashTagRecord>();
    }
}