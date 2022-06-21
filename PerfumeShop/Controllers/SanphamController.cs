using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ictshop.Models;
using PagedList;

namespace Ictshop.Controllers
{
    public class SanphamController : Controller
    {
        private QLNuochoa db = new QLNuochoa();

        public QLNuochoa Db { get => db; set => db = value; }


        // GET: Sanpham
        public ActionResult danhChoCahai()
        {
            var nn = Db.Sanphams.Where(n => n.Maloai == 3).Take(4).ToList();
            return PartialView(nn);
        }
        public ActionResult danhChoNam()
        {
            var nam = Db.Sanphams.Where(n => n.Maloai == 1).Take(4).ToList();
            return PartialView(nam);
        }
        public ActionResult danhChoNu()
        {
            var nu = Db.Sanphams.Where(n => n.Maloai == 2).Take(4).ToList();
            return PartialView(nu);
        }
        public ActionResult ListSanPham(int? page,string SearchString = "")
        {
            if (page == null) page = 1;
            int pageSize = 16;
            int pageNumber = (page ?? 1);
            if (SearchString != "")
            {
                var ListSP = Db.Sanphams.Where(x => x.Tensp.ToUpper().Contains(SearchString.ToUpper())).OrderBy(x => x.Masp); ;

                return View(ListSP.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                var ListSP = Db.Sanphams.OrderBy(x => x.Masp); ;

                return View(ListSP.ToPagedList(pageNumber, pageSize));
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