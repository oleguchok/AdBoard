using AdBoard.Domain.Abstract;
using AdBoard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdBoard.WebUI.Controllers
{
    public class AdminController : Controller
    {
        IAdRepository repository;

        public AdminController(IAdRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Ads);
        }

        public ViewResult Edit(int adId)
        {
            Ad ad = repository.Ads.FirstOrDefault(a => a.Id == adId);
            return View(ad);
        }

        [HttpPost]
        public ActionResult Edit(Ad ad)
        {
            if (ModelState.IsValid)
            {
                repository.SaveAd(ad);
                TempData["message"] = string.Format("Changes in ad {0} was saved",
                                                    ad.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(ad);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Ad());
        }

        [HttpPost]
        public ActionResult Delete(int adId)
        {
            Ad deletedAd = repository.DeleteAd(adId);
            if (deletedAd != null)
            {
                TempData["message"] = string.Format("Ad {0} was deleted",
                    deletedAd.Name);
            }
            return RedirectToAction("Index");
        }
    }
}