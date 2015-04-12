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

        public UserController(IAdRepository repo)
        {
            repository = repo;
        }

        public ActionResult UserAccount()
        {
            var user = User.Identity.GetUserId();
            
            Ad[] ads = repository.Ads.Where(a => a.UserId == user).Select(p => p).ToArray();

            return View(ads);
        }
    }
}