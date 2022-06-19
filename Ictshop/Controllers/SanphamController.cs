using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ictshop.Models;

namespace Ictshop.Controllers
{
    public class SanphamController : Controller
    {
        private QLNuochoa db = new QLNuochoa();

        public QLNuochoa Db { get => db; set => db = value; }

        // GET: Sanpham
        public ActionResult dtiphonepartial()
        {
            var ip = Db.Sanphams.Where(n => n.Maloai == 3).Take(4).ToList();
            return PartialView(ip);
        }
        public ActionResult dtsamsungpartial()
        {
            var ss = Db.Sanphams.Where(n => n.Maloai == 1).Take(4).ToList();
            return PartialView(ss);
        }
        public ActionResult dtxiaomipartial()
        {
            var mi = Db.Sanphams.Where(n => n.Maloai == 2).Take(4).ToList();
            return PartialView(mi);
        }
        //public ActionResult dttheohang()
        //{
        //    var mi = db.Sanphams.Where(n => n.Mahang == 5).Take(4).ToList();
        //    return PartialView(mi);
        //}
        public ActionResult ListSanPham(string SearchString = "")
        {
            if (SearchString != "")
            {
                var ListSP = Db.Sanphams.Where(x => x.Tensp.ToUpper().Contains(SearchString.ToUpper()));
                return PartialView(ListSP.ToList());
            }
            else
            {
                var ListSP = Db.Sanphams.ToList();
                return PartialView(ListSP);
            }

        }
        public ActionResult xemchitiet(int Masp=0)
        {
            var chitiet = Db.Sanphams.SingleOrDefault(n=>n.Masp==Masp);
            if (chitiet == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(chitiet);
        }

    }

}