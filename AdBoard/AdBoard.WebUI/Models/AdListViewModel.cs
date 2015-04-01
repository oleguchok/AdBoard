using AdBoard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdBoard.WebUI.Models
{
    public class AdListViewModel
    {
        public IEnumerable<Ad> Ads { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}