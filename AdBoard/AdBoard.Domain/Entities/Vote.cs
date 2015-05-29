using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdBoard.Domain.Entities
{
    public class Vote
    {
        public int Id { get; set; }
        public int VoteForId { get; set; }
        public string VoteUser { get; set; }
        public int AdVote { get; set; }
        public bool Active { get; set; }
    }
}
