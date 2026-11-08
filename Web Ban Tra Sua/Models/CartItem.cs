namespace WebBanTraSua.Models
{
    public class CartItem
    {
        public int SanPhamId { get; set; }
        public string TenSanPham { get; set; }
        public decimal DonGia { get; set; }
        public int SoLuong { get; set; }
        public string HinhAnh { get; set; }
        // Tự động tính thành tiền
        public decimal ThanhTien { get { return SoLuong * DonGia; } }
    }
}