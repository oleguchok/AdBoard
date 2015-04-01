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
        
        public AdController(IAdRepository repo)
        {
            repository = repo;
        }

        public ViewResult List()
        {
            return View(repository.Ads);
        }
    }
}