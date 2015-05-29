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
        [Display(Name="Name")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage="Please, enter description for ad")]
        public string Description { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString=("{0:dd/MM/yyyy}"), ApplyFormatInEditMode=true)]
        [Required(ErrorMessage="Please, enter date for ad(use slash\"/\")")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage="Please, enter ad category")]
        [Display(Name="Cateogry")]
        public string Category { get; set; }

        [HiddenInput(DisplayValue=false)]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Please, enter ad price")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [HiddenInput(DisplayValue = true)]
        public IEnumerable<Image> Images { get; set; }
    }
}
