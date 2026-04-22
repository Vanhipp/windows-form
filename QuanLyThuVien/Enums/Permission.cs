using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Enums
{
    [Flags]
    public enum Permission
    {
        None = 0,
        View = 1,
        Add = 2,
        Edit = 4,
        Approve = 8,
        Delete = 16,
        ManageUsers = 32,

        // Common combinations
        ViewAndAdd = View | Add,
        ViewAddEdit = View | Add | Edit,
        ViewAddEditApprove = View | Add | Edit | Approve,
        FullAccess = View | Add | Edit | Approve | Delete | ManageUsers
    }
}
