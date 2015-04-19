using AdBoard.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdBoard.WebUI.Controllers
{
    public class ImageController : Controller
    {
        private ApplicationDbContext contextIdentity = new ApplicationDbContext();

        public ImageController()
        {

        }

        public FileContentResult GetImage(string id)
        {
            ApplicationUser user = contextIdentity.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                if (user.ImageData != null)
                    return File(user.ImageData, user.ImageMimeType);
                else
                {
                    string path = AppDomain.CurrentDomain.BaseDirectory + "/Content/Images/User.png";
                    return File(System.IO.File.ReadAllBytes(path), "image/jpg");
                }
            }
            else
                return null;
        }

        protected override void Dispose(bool disposing)
        {
            contextIdentity.Dispose();
            base.Dispose(disposing);
        }
    }
}