using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Enums
{
    public enum ReaderType
    {
        Whitelist,
        Blacklist,
        Graylist
    }

    public static class ReaderTypeExtensions
    {
        public static string ToDisplayString(this ReaderType type)
        {
            switch (type)
            {
                case ReaderType.Whitelist: return "Danh sách trắng";
                case ReaderType.Blacklist: return "Danh sách đen";
                case ReaderType.Graylist: return "Danh sách xám";
                default: return type.ToString();
            }
        }

        public static ReaderType FromDbString(string dbValue)
        {
            if (string.IsNullOrWhiteSpace(dbValue)) return ReaderType.Whitelist;
            
            var trimmed = dbValue.Trim().ToLower();
            switch (trimmed)
            {
                case "whitelist":
                    return ReaderType.Whitelist;
                case "blacklist":
                    return ReaderType.Blacklist;
                case "graylist":
                    return ReaderType.Graylist;
                default:
                    return ReaderType.Whitelist;
            }
        }
    }
}
