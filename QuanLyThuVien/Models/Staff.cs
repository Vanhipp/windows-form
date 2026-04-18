using System;
using QuanLyThuVien.Enums;

namespace QuanLyThuVien.Models
{
    internal class Staff
    {
        public string IDNhanVien { get; set; }

        public string HoTen { get; set; }

        public DateTime NgaySinh { get; set; }

        public string DiaChi { get; set; }

        public string SoDienThoai { get; set; }

        public string BangCap { get; set; }

        public Department BoPhan { get; set; }

        public Position ChucVu { get; set; }

        public string MatKhau { get; set; }
    }
}
