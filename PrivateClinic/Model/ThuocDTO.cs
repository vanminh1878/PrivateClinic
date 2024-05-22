using System;

namespace PrivateClinic.Model
{
    public class ThuocDTO
    {
        public int STT { get; set; }
        public int MaThuoc { get; set; }
        public string TenThuoc { get; set; }
        public string DVT { get; set; }
        public int? SL { get; set; }
        public double? Gia { get; set; }

        public DateTime? NgayNhap { get; set; }
    }
}
