using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Base64ToImage.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Base64ToImage.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;


        public HomeController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        /// <summary>
        /// Render Demo page
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ImageViewModel imageViewModel = new ImageViewModel();
            return View(imageViewModel);
        }
        /// <summary>
        /// View Image
        /// </summary>
        /// <param name="imageViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Index(ImageViewModel imageViewModel)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            Base64ToImage(imageViewModel.Base64String).Save(webRootPath + "\\Images\\sample.png", System.Drawing.Imaging.ImageFormat.Png);
            imageViewModel.ImageUrl = "~/Images/sample.png";
            return View(imageViewModel);
        }

        /// <summary>
        /// Convert base64 string to Image
        /// </summary>
        /// <returns></returns>
        public System.Drawing.Image Base64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            return image;
        }

    }
}
