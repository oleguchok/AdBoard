using AdBoard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdBoard.WebUI.Models
{
    public class UserAdViewModel
    {
        public Ad Ad { get; set; }
        public ApplicationUser User { get; set; }
    }
}