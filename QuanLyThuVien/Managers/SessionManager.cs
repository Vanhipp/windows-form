using System;
using QuanLyThuVien.Enums;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Managers
{
    /// <summary>
    /// Manages the current user session.
    /// Stores the logged-in user information and provides access to permissions.
    /// </summary>
    internal static class SessionManager
    {
        public static Staff CurrentUser { get; private set; }

        public static bool IsLoggedIn => CurrentUser != null;

        /// <summary>
        /// Sets the current user session.
        /// </summary>
        public static void Login(Staff user)
        {
            CurrentUser = user ?? throw new ArgumentNullException(nameof(user));
        }

        /// <summary>
        /// Clears the current user session.
        /// </summary>
        public static void Logout()
        {
            CurrentUser = null;
        }

        /// <summary>
        /// Gets the current user's permissions.
        /// </summary>
        public static Permission GetCurrentUserPermissions()
        {
            if (!IsLoggedIn)
                return Permission.None;

            return PermissionManager.GetPermissionsForPosition(CurrentUser.ChucVu);
        }

        /// <summary>
        /// Checks if the current user has a specific permission.
        /// </summary>
        public static bool HasPermission(Permission permission)
        {
            if (!IsLoggedIn)
                return false;

            return PermissionManager.HasPermission(CurrentUser.ChucVu, permission);
        }

        /// <summary>
        /// Checks if the current user has any of the specified permissions.
        /// </summary>
        public static bool HasAnyPermission(Permission permissions)
        {
            if (!IsLoggedIn)
                return false;

            return PermissionManager.HasAnyPermission(CurrentUser.ChucVu, permissions);
        }
    }
}
