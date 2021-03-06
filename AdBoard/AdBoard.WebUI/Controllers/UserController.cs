﻿using AdBoard.Domain.Abstract;
using AdBoard.Domain.Entities;
using AdBoard.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using AdBoard.Domain.Concrete;

namespace AdBoard.WebUI.Controllers
{
    public class UserController : Controller
    {
        IAdRepository repository;
        public int pageSize = 1;
        protected ApplicationDbContext ApplicationDbContext { get; set; }

        public UserController(IAdRepository repo)
        {
            this.ApplicationDbContext = new ApplicationDbContext();
            repository = repo;
        }

        public ActionResult UserAds(int page = 1)
        {
            var userId = User.Identity.GetUserId();

            UserAdsViewModel model = new UserAdsViewModel
            {
                Ads = repository.Ads
                    .Where(a => a.UserId == userId)
                    .OrderBy(a => a.Name)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = repository.Ads
                        .Where(a => a.UserId == userId)
                        .Count()
                },
                User = ApplicationDbContext.Users.FirstOrDefault(x => x.Id == userId)
            };
            foreach (Ad ad in model.Ads)
            {
                ad.Images = repository.Images
                            .Where(i => i.AdId == ad.Id);
            }
            ViewBag.IsInfo = true;
            ViewBag.IsUserAd = true;
            return View(model);
        }

        public PartialViewResult Profile()
        {
            var userId = User.Identity.GetUserId();
            var user = ApplicationDbContext.Users.FirstOrDefault(x => x.Id == userId);
            return PartialView(user);
        }
    }
}