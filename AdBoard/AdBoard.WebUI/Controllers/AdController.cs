using AdBoard.Domain.Abstract;
using AdBoard.Domain.Concrete;
using AdBoard.Domain.Entities;
using AdBoard.WebUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;


namespace AdBoard.WebUI.Controllers
{
    public class AdController : Controller
    {
        private EFAdRepository repository;
        public int pageSize = 2;
        EFDbContext db = new EFDbContext();
        protected ApplicationDbContext ApplicationDbContext { get; set; }
        
        public AdController(EFAdRepository repo)
        {
            this.ApplicationDbContext = new ApplicationDbContext();
            repository = repo;
        }

        public ViewResult Edit(int id)
        {
            Ad ad = repository.Ads
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
                return View(ad);
            }
        }

        [HttpPost]
        public ActionResult Delete(int adId)
        {
            Ad deletedAd = repository.DeleteAd(adId);
            if (deletedAd != null)
            {
                TempData["message"] = string.Format("Ad \"{0}\" was deleted",
                    deletedAd.Name);
            }
            return RedirectToAction("UserAds","User");
        }

        public ViewResult Create()
        {
            Ad ad = new Ad();
            ad.UserId = User.Identity.GetUserId();
            return View("Edit", ad);
        }

        public ViewResult List(string category, int page = 1)
        {
            AdListViewModel model = new AdListViewModel
            {
                Ads = repository.Ads
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(ads => ads.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize).ToList(),
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
            foreach(Ad ad in model.Ads)
            {
                ad.Images = repository.Images
                            .Where(i => i.AdId == ad.Id);       
            }
            ViewBag.IsInfo = false;
            ViewBag.IsUserAd = false;
            return View(model);
        }

        public ViewResult AdInfo(int id)
        {
            var ad = repository.Ads.Where(a => a.Id == id).FirstOrDefault();
            ad.Images = repository.Images.Where(m => m.AdId == id);
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

        [HttpPost]
        public void StarAd(int adId)
        {
            string userId = User.Identity.GetUserId();
            ApplicationUser user = ApplicationDbContext.Users.FirstOrDefault(x => x.Id == userId);
            if (user.FavoritesAds != null && user.FavoritesAds.Contains(adId))
            {
                user.FavoritesAds = user.FavoritesAds.Where(a => a != adId).ToArray();
            }
            else
            {
                if (user.FavoritesAds != null)
                {
                    user.FavoritesAds.ToList().Add(adId);
                    user.FavoritesAds.ToArray();
                }
                else
                    user.FavoritesAds = new int[1] { adId };
            }
            ApplicationDbContext.SaveChanges();
        }

        public ActionResult SearchAds(string adName, string category)
        {
            ViewBag.Category = new SelectList(repository.Ads, "Category", "Category");
            ViewBag.IsInfo = false;
            ViewBag.IsUserAd = false;
            var ads = from a in repository.Ads
                      select a;
            if (!String.IsNullOrEmpty(adName))
            {
                ads = ads.Where(a => a.Name.ToLower() == adName.ToLower());
            }
            if (!String.IsNullOrEmpty(category))
            {
                return View(ads.Where(x => x.Category == category));
            }
            else
                return View(ads);
        }

        public FileContentResult GetAdImage(int imageId)
        {
            var Image = repository.Images.Where(i => i.Id == imageId).FirstOrDefault();
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

        public ActionResult EditImages(int adId)
        {
            var images = repository.Images.Where(i => i.AdId == adId);
            ViewBag.AdId = adId;
            return View(images);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditImages(int adId, HttpPostedFileBase file = null)
        {
            Image image = new Image();
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    int count = repository.Images.Count();
                    image.Id = ++count;
                    image.AdId = adId;
                    image.ImageMimeType = file.ContentType;
                    image.ImageData = new byte[file.ContentLength];
                    file.InputStream.Read(image.ImageData, 0, file.ContentLength);
                    
                    /*var ad = repository.Ads.Where(a => a.Id == adId).FirstOrDefault();
                    ad.Images = repository.Images.Where(m => m.AdId == adId);
                    ad.Images.ToList().Add(image);
                    repository.Ads.Where(a => a.Id == adId).FirstOrDefault().Images = ad.Images;
                    repository.Save();*/
                    db.Images.Add(image);
                    db.SaveChanges();
                    //repository.SaveImage(image);
                }
               
            }
            else
            {
                return null;                
            }
            return RedirectToAction("AdInfo", new { id = image.AdId });
        }
    }
}