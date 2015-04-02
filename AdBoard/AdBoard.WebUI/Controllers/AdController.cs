using AdBoard.Domain.Abstract;
using AdBoard.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdBoard.WebUI.Controllers
{
    public class AdController : Controller
    {
        private IAdRepository repository;
        public int pageSize = 2;
        
        public AdController(IAdRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(string category, int page = 1)
        {
            AdListViewModel model = new AdListViewModel
            {
                Ads = repository.Ads
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(ads => ads.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?
                    repository.Ads.Count() :
                    repository.Ads.Where(a => a.Category == category).Count(),
                },
                CurrentCategory = category
            };
            return View(model);
        }
    }
}