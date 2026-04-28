using System;
using QuanLyThuVien.Enums;

namespace QuanLyThuVien.Models
{
    internal class Reader
    {
        public string IDDocGia { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public DateTime NgayLap { get; set; }
        public ReaderType LoaiDocGia { get; set; }
        public string LoaiDocGiaDisplay => LoaiDocGia.ToDisplayString();
        public decimal TienNo { get; set; }
        public bool IsBlocked => LoaiDocGia == ReaderType.Blacklist || LoaiDocGia == ReaderType.Graylist;
    }
}
