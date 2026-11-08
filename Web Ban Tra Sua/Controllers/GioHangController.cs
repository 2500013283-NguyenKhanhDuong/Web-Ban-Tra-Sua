using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebBanTraSua.Models;

namespace WebBanTraSua.Controllers
{
    public class GioHangController : Controller
    {
        public List<CartItem> LayGioHang()
        {
            List<CartItem> lstGioHang = Session["GioHang"] as List<CartItem>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<CartItem>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }

        [HttpPost]
        public JsonResult ThemVaoGioAjax(int id)
        {
            List<CartItem> lstGioHang = LayGioHang();
            int tongSoLuongHienTai = lstGioHang.Sum(n => n.SoLuong);

            if (tongSoLuongHienTai >= 10)
            {
                return Json(new { success = false, message = "Giỏ hàng đã đạt giới hạn tối đa (10 ly)!" });
            }

            CartItem sanPham = lstGioHang.FirstOrDefault(s => s.SanPhamId == id);
            if (sanPham == null)
            {
                TraSua ts = TraSuaController.GetDanhSachTraSua().FirstOrDefault(s => s.Id == id);
                if (ts != null)
                {
                    sanPham = new CartItem { SanPhamId = ts.Id, TenSanPham = ts.TenMon, DonGia = ts.Gia, SoLuong = 1, HinhAnh = ts.HinhAnh };
                    lstGioHang.Add(sanPham);
                }
            }
            else
            {
                sanPham.SoLuong++;
            }

            return Json(new { success = true, tongSoLuong = lstGioHang.Sum(n => n.SoLuong), soLuongItem = sanPham.SoLuong, message = "Đã thêm 1 ly!" });
        }

        [HttpPost]
        public JsonResult TruKhoiGioAjax(int id)
        {
            List<CartItem> lstGioHang = LayGioHang();
            CartItem sanPham = lstGioHang.FirstOrDefault(s => s.SanPhamId == id);
            int soLuongItem = 0;

            if (sanPham != null)
            {
                if (sanPham.SoLuong > 1)
                {
                    sanPham.SoLuong--;
                    soLuongItem = sanPham.SoLuong;
                }
                else
                {
                    lstGioHang.Remove(sanPham);
                    soLuongItem = 0;
                }
            }
            return Json(new { success = true, tongSoLuong = lstGioHang.Sum(n => n.SoLuong), soLuongItem = soLuongItem });
        }

        [HttpPost]
        public JsonResult XoaKhoiGioAjax(int id)
        {
            List<CartItem> lstGioHang = LayGioHang();
            CartItem sanPham = lstGioHang.FirstOrDefault(s => s.SanPhamId == id);
            if (sanPham != null)
            {
                lstGioHang.Remove(sanPham);
            }
            return Json(new { success = true });
        }

        public ActionResult Index()
        {
            List<CartItem> lstGioHang = LayGioHang();
            ViewBag.TongTien = lstGioHang.Sum(s => s.ThanhTien);
            return View(lstGioHang);
        }

        public ActionResult ThanhToan()
        {
            List<CartItem> lstGioHang = LayGioHang();
            if (lstGioHang.Count == 0) return RedirectToAction("Index", "TraSua");
            ViewBag.TongTien = lstGioHang.Sum(s => s.ThanhTien);
            return View(lstGioHang);
        }

        [HttpPost]
        public ActionResult DatHang(string HoTen, string SoDienThoai, string DiaChi)
        {
            List<CartItem> lstGioHang = LayGioHang();
            TempData["DonHang"] = new List<CartItem>(lstGioHang);
            TempData["HoTen"] = HoTen;
            TempData["TongTien"] = lstGioHang.Sum(s => s.ThanhTien);

            Session["GioHang"] = null;
            return RedirectToAction("XacNhan");
        }

        public ActionResult XacNhan()
        {
            if (TempData["DonHang"] == null) return RedirectToAction("Index", "TraSua");

            ViewBag.HoTen = TempData["HoTen"];
            ViewBag.TongTien = TempData["TongTien"];
            var donHang = TempData["DonHang"] as List<CartItem>;
            return View(donHang);
        }
    }
}