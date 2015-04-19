using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AdBoard.Domain.Entities
{
    public class Ad
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public string UserId { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<Image> Images { get; set; }
    }
}
