using System;
using System.Collections.Generic;
using QuanLyThuVien.Enums;

namespace QuanLyThuVien.Managers
{
    /// <summary>
    /// Manages permissions based on user position/role.
    /// 
    /// Permission mapping:
    /// - Nhân Viên (Staff): View
    /// - Phó Phòng (Vice Manager): View + Add
    /// - Trưởng Phòng (Manager): View + Add + Edit
    /// - Phó Giám Đốc (Vice Director): View + Add + Edit + Approve
    /// - Giám Đốc (Director): Full permissions
    /// </summary>
    internal class PermissionManager
    {
        private static readonly Dictionary<Position, Permission> RolePermissions = new Dictionary<Position, Permission>()
        {
            { Position.NhanVien, Permission.View },
            { Position.PhoPhong, Permission.ViewAndAdd },
            { Position.TruongPhong, Permission.ViewAddEdit },
            { Position.PhoGiamDoc, Permission.ViewAddEditApprove },
            { Position.GiamDoc, Permission.FullAccess }
        };

        /// <summary>
        /// Gets all permissions for a given position.
        /// </summary>
        public static Permission GetPermissionsForPosition(Position position)
        {
            Permission permissions;
            return RolePermissions.TryGetValue(position, out permissions)
                ? permissions
                : Permission.None;
        }

        /// <summary>
        /// Checks if a position has a specific permission.
        /// </summary>
        public static bool HasPermission(Position position, Permission requiredPermission)
        {
            var permissions = GetPermissionsForPosition(position);
            return (permissions & requiredPermission) == requiredPermission;
        }

        /// <summary>
        /// Checks if a position has any of the specified permissions.
        /// </summary>
        public static bool HasAnyPermission(Position position, Permission requiredPermissions)
        {
            var permissions = GetPermissionsForPosition(position);
            return (permissions & requiredPermissions) != Permission.None;
        }

        /// <summary>
        /// Gets a human-readable description of a position and its permissions.
        /// </summary>
        public static string GetPositionDescription(Position position)
        {
            var permissions = GetPermissionsForPosition(position);
            var permissionList = new List<string>();

            if ((permissions & Permission.View) != 0)
                permissionList.Add("Xem dữ liệu");
            if ((permissions & Permission.Add) != 0)
                permissionList.Add("Thêm");
            if ((permissions & Permission.Edit) != 0)
                permissionList.Add("Sửa");
            if ((permissions & Permission.Approve) != 0)
                permissionList.Add("Duyệt");
            if ((permissions & Permission.Delete) != 0)
                permissionList.Add("Xóa");
            if ((permissions & Permission.ManageUsers) != 0)
                permissionList.Add("Quản lý người dùng");

            return string.Join(", ", permissionList);
        }

        /// <summary>
        /// Gets the display name of a position.
        /// </summary>
        public static string GetPositionDisplayName(Position position)
        {
            if (position == Position.GiamDoc)
                return "Giám Đốc";
            if (position == Position.PhoGiamDoc)
                return "Phó Giám Đốc";
            if (position == Position.TruongPhong)
                return "Trưởng Phòng";
            if (position == Position.PhoPhong)
                return "Phó Phòng";
            if (position == Position.NhanVien)
                return "Nhân Viên";

            return "Không xác định";
        }
    }
}
