using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdBoard.Domain.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public int? AdId { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}
