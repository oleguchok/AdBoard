using AdBoard.Domain.Abstract;
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
        public int pageSize = 4;
        
        public AdController(IAdRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(int page = 1)
        {
            return View(repository.Ads
                .OrderBy(ad => ad.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize));
        }
    }
}