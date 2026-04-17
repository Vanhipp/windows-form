using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Models
{
    internal class BookLiquidation
    {
        public string IDSach { get; set; }

        public virtual Book TenSach { get; set; }

        public DateTime NgayThanhLy { get; set; }

        public string LyDoThanhLy { get; set; }
    }
}
