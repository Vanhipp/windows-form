using System;
using QuanLyThuVien.Enums;

namespace QuanLyThuVien.Models
{
    internal class Book
    {
        public int IDSach { get; set; }

        public string TenSach { get; set; }

        public string TacGia { get; set; }

        public int NamXuatBan { get; set; }

        public string NhaXuatBan { get; set; }

        public DateTime NgayNhap { get; set; }

        public int TriGia { get; set; }

        public BookStatus TinhTrang { get; set; }

        public string IDDauSach { get; set; }
    }
}
