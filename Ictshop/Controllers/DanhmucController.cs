using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ictshop.Models;

namespace Ictshop.Controllers
{
    public class DanhmucController : Controller
    {
        QLNuochoa db = new QLNuochoa();
        // GET: Danhmuc
        public ActionResult DanhmucPartial()
        {
            var danhmuc = db.Hangsanxuats.ToList();
            return PartialView(danhmuc);
        }
    }
}