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

namespace AdBoard.WebUI.Controllers
{
    public class UserController : Controller
    {
        private IAdRepository repository;
        public int pageSize = 2;

        public UserController(IAdRepository repo)
        {
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
                }
            };
            return View(model);
        }

        public PartialViewResult ProfileBar(string id)
        {
            var users = new ApplicationDbContext();
            ApplicationUser user = users.Users.Where(u => u.Id == id).First();
            return PartialView();
        }
    }
}