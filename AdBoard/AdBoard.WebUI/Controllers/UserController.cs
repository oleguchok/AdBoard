using AdBoard.Domain.Abstract;
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

namespace AdBoard.WebUI.Controllers
{
    public class UserController : Controller
    {
        private IAdRepository repository;
        public int pageSize = 2;

        protected ApplicationDbContext ApplicationDbContext { get; set; }

        protected UserManager<ApplicationUser> UserManager { get; set; }

        public UserController(IAdRepository repo)
        {
            this.ApplicationDbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));
            repository = repo;
        }

        public ActionResult UserAccount(int page = 1)
        {
            var userId = User.Identity.GetUserId();

            UserAdsViewModel model = new UserAdsViewModel
            {
                Ads = repository.Ads
                    .Where(a => a.UserId == userId)
                    .OrderBy(a => a.Date)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToArray(),
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