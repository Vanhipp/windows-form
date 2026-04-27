using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public decimal TienNo { get; set; }
    }
}
