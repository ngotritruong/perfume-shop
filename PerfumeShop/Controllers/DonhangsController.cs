using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ictshop.Models;

namespace Ictshop.Controllers
{
    public class DonhangsController : Controller
    {
        private QLNuochoa db = new QLNuochoa();

        // GET: Donhangs
        // Hiển thị danh sách đơn hàng
        public ActionResult Index()
        {
            //Kiểm tra đang đăng nhập
            if (Session["use"] == null || Session["use"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "User");
            }
            Nguoidung kh = (Nguoidung)Session["use"];
            int maND = kh.MaNguoiDung;
            var donhangs = db.Donhangs.Include(d => d.Nguoidung).Where(d=>d.MaNguoidung == maND);
            return View(donhangs.ToList());
        }

        // GET: Donhangs/Details/5
        //Hiển thị chi tiết đơn hàng
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donhang donhang = db.Donhangs.Find(id);
            var chitiet = db.Chitietdonhangs.Include(d => d.Sanpham).Where(d=> d.Madon == id).ToList();
            if (donhang == null)
            {
                return HttpNotFound();
            }
            return View(chitiet);
        }
  
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donhang dh = db.Donhangs.Find(id);
            if (dh == null)
            {
                return HttpNotFound();
            }
            return View(dh);
        }

        // POST: Admin/Nguoidungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Chitietdonhang ctdionhang = db.Chitietdonhangs.SingleOrDefault(m => m.Madon == id);
            db.Chitietdonhangs.Remove(ctdionhang);
            Donhang donhang = db.Donhangs.Find(id);
            db.Donhangs.Remove(donhang);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
