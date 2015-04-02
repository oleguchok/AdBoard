using AdBoard.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdBoard.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IAdRepository repository;

        public NavController(IAdRepository repo)
        {
            repository = repo;
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = repository.Ads
                .Select(a => a.Category)
                .Distinct()
                .OrderBy(x => x);
            return PartialView(categories);
        }
    }
}