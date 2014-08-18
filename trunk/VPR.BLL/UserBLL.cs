﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VPR.DAL;
using VPR.Common;
using VPR.Entity;
using VPR.Utilities;
using System.Web;
using VPR.Utilities.ResourceManager;
using VPR.Utilities.Cryptography;
using System.Data;

namespace VPR.BLL
{
    public class UserBLL
    {
        #region User

        public static string GetDefaultPassword()
        {
            return Encryption.Encrypt(Constants.DEFAULT_PASSWORD);
        }

        public bool ValidateUser(IUser user)
        {
            UserDAL.ValidateUser(user);
            return (user.Id > 0) ? true : false;
        }

        public static int GetLoggedInUserId()
        {
            int userId = 0;

            if (!ReferenceEquals(System.Web.HttpContext.Current.Session[Constants.SESSION_USER_INFO], null))
            {
                IUser user = (IUser)System.Web.HttpContext.Current.Session[Constants.SESSION_USER_INFO];

                if (!ReferenceEquals(user, null))
                {
                    userId = user.Id;
                }
            }

            return userId;
        }

        public static bool GetUserLocationSpecific()
        {
            bool UserLocSpecific=false;

            if (!ReferenceEquals(System.Web.HttpContext.Current.Session[Constants.SESSION_USER_INFO], null))
            {
                IUser user = (IUser)System.Web.HttpContext.Current.Session[Constants.SESSION_USER_INFO];

                if (!ReferenceEquals(user, null))
                {
                    UserLocSpecific = user.UserlocationSpecific;
                }
            }

            return UserLocSpecific;
        }

        public static int GetUserLocation()
        {
            int uloc = 0;

            if (!ReferenceEquals(System.Web.HttpContext.Current.Session[Constants.SESSION_USER_INFO], null))
            {
                IUser user = (IUser)System.Web.HttpContext.Current.Session[Constants.SESSION_USER_INFO];

                if (!ReferenceEquals(user, null))
                {
                    uloc = user.UserLocation.Id;
                    //uloc = user.PortID;
                }
            }

            return uloc;
        }

        public static int GetUserPort()
        {
            int uloc = 0;

            if (!ReferenceEquals(System.Web.HttpContext.Current.Session[Constants.SESSION_USER_INFO], null))
            {
                IUser user = (IUser)System.Web.HttpContext.Current.Session[Constants.SESSION_USER_INFO];

                if (!ReferenceEquals(user, null))
                {
                    uloc = user.PortID;
                }
            }

            return uloc;
        }

        public static int GetLoggedInUserRoleId()
        {
            int roleId = 0;

            if (!ReferenceEquals(System.Web.HttpContext.Current.Session[Constants.SESSION_USER_INFO], null))
            {
                IUser user = (IUser)System.Web.HttpContext.Current.Session[Constants.SESSION_USER_INFO];

                if (!ReferenceEquals(user, null) && user.Id > 0)
                {
                    if (!ReferenceEquals(user.UserRole, null))
                    {
                        roleId = user.UserRole.Id;
                    }
                }
            }

            return roleId;
        }

        private void SetDefaultSearchCriteriaForUser(SearchCriteria searchCriteria)
        {
            searchCriteria.SortExpression = "UserName";
            searchCriteria.SortDirection = "ASC";
        }

        public List<IUser> GetAllUserList(SearchCriteria searchCriteria)
        {
            return UserDAL.GetUserList(false, searchCriteria);
        }

        public List<IUser> GetActiveUserList()
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForUser(searchCriteria);
            return UserDAL.GetUserList(true, searchCriteria);
        }

        public IUser GetUser(int userId)
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForUser(searchCriteria);
            return UserDAL.GetUser(userId, false, searchCriteria);
        }

        public static int GetUserLoc(int userID)
        {
            return UserDAL.GetUserLoc(userID);
        }

        public static System.Data.DataSet GetUserById(int userId)
        {
           return UserDAL.GetUserById(userId);
        }

        public string SaveUser(IUser user, int modifiedBy)
        {
            int result = 0;
            string errMessage = string.Empty;
            result = UserDAL.SaveUser(user, Constants.DEFAULT_COMPANY_ID, modifiedBy);

            switch (result)
            {
                case 1:
                    errMessage = ResourceManager.GetStringWithoutName("ERR00060");
                    break;
                default:
                    break;
            }

            return errMessage;
        }

        public void DeleteUser(int userId, int modifiedBy)
        {
            UserDAL.DeleteUser(userId, modifiedBy);
        }

        public bool ChangePassword(IUser user)
        {
            return UserDAL.ChangePassword(user);
        }

        public void ResetPassword(IUser user, int modifiedBy)
        {
            user.Password = GetDefaultPassword();
            UserDAL.ResetPassword(user, modifiedBy);
        }

        public static System.Data.DataSet GetAdminUsers(int LoggedUserId,int locationId)
        {
            return VPR.DAL.UserDAL.GetAdminUsers(LoggedUserId, locationId);
        }

        #endregion

        #region Role

        private void SetDefaultSearchCriteriaForRole(SearchCriteria searchCriteria)
        {
            searchCriteria.SortExpression = "Role";
            searchCriteria.SortDirection = "ASC";
        }

        public List<IRole> GetAllRole(SearchCriteria searchCriteria)
        {
            return UserDAL.GetRole(false, searchCriteria);
        }

        public List<IRole> GetActiveRole()
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForRole(searchCriteria);
            return UserDAL.GetRole(true, searchCriteria);
        }

        public IRole GetRole(int roleId)
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForRole(searchCriteria);
            return UserDAL.GetRole(roleId, false, searchCriteria);
        }

        public string SaveRole(List<RoleMenuEntity> lstRoleMenu, IRole role, int modifiedBy)
        {
            int result = 0;
            string errMessage = string.Empty;
            string xmlDoc = GeneralFunctions.Serialize(lstRoleMenu);
            result = UserDAL.SaveRole(role, Constants.DEFAULT_COMPANY_ID, xmlDoc, modifiedBy);

            switch (result)
            {
                case 1:
                    errMessage = ResourceManager.GetStringWithoutName("ERR00072");
                    break;
                default:
                    break;
            }

            return errMessage;
        }

        public void DeleteRole(int roleId, int modifiedBy)
        {
            UserDAL.DeleteRole(roleId, modifiedBy);
        }

        public void ChangeRoleStatus(int roleId, bool status, int modifiedBy)
        {
            UserDAL.ChangeRoleStatus(roleId, status, modifiedBy);
        }

        public List<IRoleMenu> GetMenuByRole(int roleId, int mainId)
        {
            return UserDAL.GetMenuByRole(roleId, mainId);
        }

        //public static void GetMenuAccessByUser(int userId, int menuId, out bool canAdd, out bool canEdit, out bool canDelete, out bool canView)
        //{
        //    canAdd = false;
        //    canEdit = false;
        //    canDelete = false;
        //    canView = false;

        //    IRoleMenu roleMenuAccess = UserDAL.GetMenuAccessByUser(userId, menuId);

        //    if (!ReferenceEquals(roleMenuAccess, null))
        //    {
        //        canAdd = roleMenuAccess.CanAdd;
        //        canEdit = roleMenuAccess.CanEdit;
        //        canDelete = roleMenuAccess.CanDelete;
        //        canView = roleMenuAccess.CanView;
        //    }
        //}

        public static IUserPermission GetMenuAccessByUser(int userId, int menuId)
        {
            IUserPermission userPermission = new UserPermission();

            IRoleMenu roleMenuAccess = UserDAL.GetMenuAccessByUser(userId, menuId);

            if (!ReferenceEquals(roleMenuAccess, null))
            {
                userPermission.CanAdd = roleMenuAccess.CanAdd;
                userPermission.CanEdit = roleMenuAccess.CanEdit;
                userPermission.CanDelete = roleMenuAccess.CanDelete;
                userPermission.CanView = roleMenuAccess.CanView;
            }

            return userPermission;
        }

        public static void GetUserPermission(out bool canAdd, out bool canEdit, out bool canDelete, out bool canView)
        {
            canAdd = false;
            canEdit = false;
            canDelete = false;
            canView = false;

            if (!ReferenceEquals(System.Web.HttpContext.Current.Session[Constants.SESSION_USER_PERMISSION], null))
            {
                IUserPermission userPermission = (IUserPermission)System.Web.HttpContext.Current.Session[Constants.SESSION_USER_PERMISSION];

                if (!ReferenceEquals(userPermission, null))
                {
                    canAdd = userPermission.CanAdd;
                    canEdit = userPermission.CanEdit;
                    canDelete = userPermission.CanDelete;
                    canView = userPermission.CanView;
                }
            }
        }

        public static DataTable GetUserSpecificMenuList(int UserID)
        {
            return UserDAL.GetUserSpecificMenuList(UserID);
        }

        #endregion
    }
}
