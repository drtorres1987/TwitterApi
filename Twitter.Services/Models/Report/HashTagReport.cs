using System.Collections.Generic;

namespace Twitter.Service.Models.Report
{
    public class HashTagReport
    {
        public int TotalTwitts { get; set; }
        public IEnumerable<HashTagRecord> HashTags { get; set; } = new List<HashTagRecord>();
    }
}