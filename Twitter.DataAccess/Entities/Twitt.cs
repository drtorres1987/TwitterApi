using System;
using System.Collections.Generic;
using System.Text;

namespace Twitter.DataAccess.Entities
{
    public class Twitt : IEntity
    {
        public int Id { get; set; }
        public string TwitterId { get; set; }
        public string Text { get; set; }
        public ICollection<HashTag> HashTags = new List<HashTag>();
    }
}
