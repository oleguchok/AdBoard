using AdBoard.Domain.Abstract;
using AdBoard.Domain.Concrete;
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
        private EFDbContext db = new EFDbContext();
        protected ApplicationDbContext ApplicationDbContext { get; set; }
        
        public AdController(IAdRepository repo)
        {
            this.ApplicationDbContext = new ApplicationDbContext();
            repository = repo;
        }

        public ViewResult List(string category, int page = 1)
        {
            AdListViewModel model = new AdListViewModel
            {
                Ads = db.Ads
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
            ViewBag.IsInfo = false;
            return View(model);
        }

        public ViewResult AdInfo(int id)
        {
            var ad = db.Ads.Where(a => a.Id == id).FirstOrDefault();
            UserAdViewModel model = new UserAdViewModel
            {
                Ad = ad,
                User = ApplicationDbContext.Users.FirstOrDefault(u => u.Id == ad.UserId)
            };
            ViewBag.IsInfo = true;
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}