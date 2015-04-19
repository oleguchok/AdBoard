using AdBoard.Domain.Abstract;
using AdBoard.Domain.Concrete;
using AdBoard.Domain.Entities;
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
        private EFAdRepository repository;
        public int pageSize = 2;
        private EFDbContext db = new EFDbContext();
        protected ApplicationDbContext ApplicationDbContext { get; set; }
        
        public AdController(EFAdRepository repo)
        {
            this.ApplicationDbContext = new ApplicationDbContext();
            repository = repo;
        }

        public ViewResult Create()
        {
            return View();
        }

        public ViewResult Edit(int id)
        {
            Ad ad = db.Ads
                    .FirstOrDefault(a => a.Id == id);
            return View(ad);
        }

        [HttpPost]
        public ActionResult Edit(Ad ad)
        {
            if (ModelState.IsValid)
            {
                repository.SaveAd(ad);
                TempData["message"] = string.Format("Changing in ad \"{0}\" was saved", ad.Name);
                return RedirectToAction("UserAds","User");
            }
            else
            {
                // Что-то не так со значениями данных
                return View(ad);
            }
        }

        public ViewResult List(string category, int page = 1)
        {
            AdListViewModel model = new AdListViewModel
            {
                Ads = db.Ads
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(ads => ads.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize).ToList(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?
                    db.Ads.Count() :
                    db.Ads.Where(a => a.Category == category).Count(),
                },
                CurrentCategory = category
            };
            foreach(Ad ad in model.Ads)
            {
                ad.Images = db.Images
                            .Where(i => i.AdId == ad.Id);       
            }
            ViewBag.IsInfo = false;
            ViewBag.IsUserAd = false;
            return View(model);
        }

        public ViewResult AdInfo(int id)
        {
            var ad = db.Ads.Where(a => a.Id == id).FirstOrDefault();
            ad.Images = db.Images.Where(m => m.AdId == id);
            UserAdViewModel model = new UserAdViewModel
            {
                Ad = ad,
                User = ApplicationDbContext.Users.FirstOrDefault(u => u.Id == ad.UserId)
            };
            ViewBag.IsInfo = true;
            if (model.Ad.UserId == model.User.Id)
                ViewBag.IsUserAd = true;
            else
                ViewBag.IsUserAd = false;
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public FileContentResult GetAdImage(int imageId)
        {
            var Image = db.Images.Where(i => i.Id == imageId).FirstOrDefault();
            if (Image != null)
            {
                return File(Image.ImageData, Image.ImageMimeType);
            }
            else
            {
                return null;
                /*string path = AppDomain.CurrentDomain.BaseDirectory + "/Content/Images/No_image.png";
                return File(System.IO.File.ReadAllBytes(path), "image/jpg");*/
            }
        }
    }
}