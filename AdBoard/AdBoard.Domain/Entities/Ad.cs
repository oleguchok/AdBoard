using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace AdBoard.Domain.Entities
{
    public class Ad
    {
        [HiddenInput(DisplayValue=false)]
        public int Id { get; set; }
        
        [Required(ErrorMessage="Please, enter ad name")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage="Please, enter description for ad")]
        public string Description { get; set; }
        
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage="Please, enter date for ad")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage="Please, enter ad category")]
        public string Category { get; set; }
        public string UserId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please, enter ad price")]
        public decimal Price { get; set; }

        [HiddenInput(DisplayValue=false)]
        public IEnumerable<Image> Images { get; set; }
    }
}
