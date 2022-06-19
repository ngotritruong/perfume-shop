using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ictshop.Areas.ViewModel;
using Ictshop.Models;

namespace Ictshop.Areas.Admin.Controllers
{
    public class QLDonHangController : Controller
    {
        private QLNuochoa db = new QLNuochoa();
        // GET: Adm
        // in/QLDonHang
        public ActionResult Index()
        {
            var listdonhang = from a in db.Donhangs
                              join b in db.Nguoidungs on a.MaNguoidung equals b.MaNguoiDung
                              where a.MaNguoidung == b.MaNguoiDung
                              select new QuanlybannuochoaVM()
                              {
                                  Madon = a.Madon,
                                  MaNguoidung = b.MaNguoiDung,
                                  Nguoidung = b.Hoten,
                                  Dienthoai = b.Dienthoai,
                                  Ngaydat = a.Ngaydat,
                              };
            return View(listdonhang.ToList());
        }
    }
}
