using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Models
{
    internal class ReturnSlip
    {
        public string IDPhieuMuon { get; set; }

        public string IDDocGia { get; set; } public virtual Reader TenDocGia { get; set; }

        public string IDSach { get; set; } public virtual Book TenSach { get; set; }

        public DateTime NgayGhiNhan { get; set; }

        public decimal TienPhat { get; set; }
    }
}
