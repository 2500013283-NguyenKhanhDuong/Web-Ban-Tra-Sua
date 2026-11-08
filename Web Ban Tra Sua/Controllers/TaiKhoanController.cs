using System.Web.Mvc;
using WebBanTraSua.Models;

namespace WebBanTraSua.Controllers
{
    public class TaiKhoanController : Controller
    {
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(TaiKhoan tk)
        {
            if (tk.TenDangNhap == "admin" && tk.MatKhau == "123456")
            {
                Session["Admin"] = tk.TenDangNhap;
                return RedirectToAction("Index", "TraSua");
            }
            ViewBag.Loi = "Sai tài khoản hoặc mật khẩu";
            return View();
        }

        public ActionResult DangXuat()
        {
            Session["Admin"] = null;
            return RedirectToAction("Index", "TraSua");
        }
    }
}