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
using PagedList;

namespace Ictshop.Areas.Admin.Controllers
{
    public class QLDonHangController : Controller
    {
        private QLNuochoa db = new QLNuochoa();
        // GET: Adm
        // in/QLDonHang
      
        public ActionResult Index(int? page, string SearchString = "")
        {
            if (page == null) page = 1;
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            if (SearchString != "")
            {
                var listdonhang = from a in db.Donhangs
                                  join b in db.Nguoidungs on a.MaNguoidung equals b.MaNguoiDung
                                  where a.MaNguoidung == b.MaNguoiDung && a.Madon.ToString().Contains(SearchString)
                                  orderby a.Madon descending
                                  select new QuanlybannuochoaVM()
                                  {
                                      Madon = a.Madon,
                                      MaNguoidung = b.MaNguoiDung,
                                      Nguoidung = b.Hoten,
                                      Dienthoai = b.Dienthoai,
                                      Ngaydat = a.Ngaydat,
                                      Diachi = b.Diachi,
                                      Tinhtrang = a.Tinhtrang
                                  };
                return View(listdonhang.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                var listdonhang = from a in db.Donhangs
                                  join b in db.Nguoidungs on a.MaNguoidung equals b.MaNguoiDung
                                  where a.MaNguoidung == b.MaNguoiDung
                                  orderby a.Madon descending
                                  select new QuanlybannuochoaVM()
                                  {
                                      Madon = a.Madon,
                                      MaNguoidung = b.MaNguoiDung,
                                      Nguoidung = b.Hoten,
                                      Dienthoai = b.Dienthoai,
                                      Ngaydat = a.Ngaydat,
                                      Diachi = b.Diachi,
                                      Tinhtrang = a.Tinhtrang
                                  };
                return View(listdonhang.ToPagedList(pageNumber, pageSize));
            }
            

           
            
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donhang donhang = db.Donhangs.Find(id);
            var chitiet = db.Chitietdonhangs.Include(d => d.Sanpham).Where(d => d.Madon == id).ToList();
            if (donhang == null)
            {
                return HttpNotFound();
            }
            return View(chitiet);
        }
    }
}
