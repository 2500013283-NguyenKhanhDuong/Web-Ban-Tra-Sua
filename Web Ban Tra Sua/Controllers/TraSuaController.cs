using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebBanTraSua.Models;

namespace WebBanTraSua.Controllers
{
    public class TraSuaController : Controller
    {
        public static List<TraSua> GetDanhSachTraSua()
        {
            return new List<TraSua>
            {
                new TraSua { Id = 1, TenMon = "Trà Sữa Trân Châu Đường Đen", Gia = 35000, DanhMuc = "Trà Sữa", MoTa = "Đậm vị trà đen hòa quyện sữa tươi thanh trùng và trân châu đường đen dẻo thơm.", HinhAnh = "https://images.unsplash.com/photo-1527661591475-527312dd65f5?w=500" },
                new TraSua { Id = 2, TenMon = "Trà Sữa Thái Xanh", Gia = 30000, DanhMuc = "Trà Thái", MoTa = "Vị thanh mát đặc trưng của trà Thái xanh kết hợp lớp sữa béo ngậy.", HinhAnh = "https://images.unsplash.com/photo-1541658016709-82535e94bc69?w=500" },
                new TraSua { Id = 3, TenMon = "Hồng Trà Macchiato", Gia = 40000, DanhMuc = "Macchiato", MoTa = "Hồng trà đậm đà phủ lớp váng sữa Macchiato sánh mịn mặn nhẹ.", HinhAnh = "https://images.unsplash.com/photo-1576092768241-dec231879fc3?w=500" },
                new TraSua { Id = 4, TenMon = "Trà Sữa Oolong Kem Cheese", Gia = 45000, DanhMuc = "Trà Sữa", MoTa = "Trà Oolong thơm nồng kết hợp lớp kem phô mai béo ngậy đậm vị.", HinhAnh = "https://images.unsplash.com/photo-1544787219-7f47ccb76574?w=500" },
                new TraSua { Id = 5, TenMon = "Trà Đào Cam Sả", Gia = 38000, DanhMuc = "Trà Trái Cây", MoTa = "Vị chua ngọt thanh mát của đào tươi hòa quyện hương sả sảng khoái.", HinhAnh = "https://images.unsplash.com/photo-1513558161293-cdaf765ed2fd?w=500" },
                new TraSua { Id = 6, TenMon = "Trà Sữa Matcha Đậu Đỏ", Gia = 42000, DanhMuc = "Matcha", MoTa = "Matcha Uji Nhật Bản chuẩn vị kèm topping đậu đỏ bùi ngọt.", HinhAnh = "https://images.unsplash.com/photo-1536256263959-770b48d82b0a?w=500" }
            };
        }

        public ActionResult Index()
        {
            return View(GetDanhSachTraSua());
        }

        public ActionResult ChiTiet(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var sanPham = GetDanhSachTraSua().FirstOrDefault(x => x.Id == id);
            if (sanPham == null)
            {
                return RedirectToAction("Index");
            }
            return View(sanPham);
        }
    }
}