﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VPR.Common;
using System.Data;
using System.Data.SqlClient;

namespace VPR.Entity
{
    public class UserEntity : IUser
    {
        #region IUser Members

        public string Password
        {
            get;
            set;
        }

        public string NewPassword
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public string UserFullName
        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
        }

        public string EmailId
        {
            get;
            set;
        }

        public bool IsActive
        {
            get;
            set;
        }

        public bool AllowMutipleLocation
        {
            get;
            set;
        }

        public bool UserlocationSpecific
        {
            get;
            set;
        }

        public int PortID
        {
            get;
            set;
        }

        public string PortName
        {
            get;
            set;
        }

        #endregion

        #region IRole Members

        public IRole UserRole
        {
            get;
            set;
        }

        #endregion

        #region ILocation Members

        public ILocation UserLocation
        {
            get;
            set;
        }

        #endregion

        #region IBase<int> Members

        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        #endregion

        #region ICommon Members

        public int CreatedBy
        {
            get;
            set;
        }

        public DateTime CreatedOn
        {
            get;
            set;
        }

        public int ModifiedBy
        {
            get;
            set;
        }

        public DateTime ModifiedOn
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public UserEntity()
        {
            this.UserRole = new RoleEntity();
            this.UserLocation = new LocationEntity();
        }

        public UserEntity(DataTableReader reader)
        {
            this.Id = Convert.ToInt32(reader["UserId"]);
            this.Name = Convert.ToString(reader["UserName"]);
            this.FirstName = Convert.ToString(reader["FirstName"]);
            this.LastName = Convert.ToString(reader["LastName"]);
            this.UserRole = new RoleEntity();
            this.UserRole.Id = Convert.ToInt32(reader["RoleId"]);
            this.UserRole.Name = Convert.ToString(reader["RoleName"]);

            this.UserLocation = new LocationEntity();
            //this.UserLocation.Id = Convert.ToInt32(reader["LocId"]);
            //this.UserLocation.Name = Convert.ToString(reader["LocName"]);

            //if (ColumnExists(reader, "locationSpecific"))
            //    this.UserlocationSpecific = Convert.ToBoolean(reader["locationSpecific"]);

            if (reader["EmailId"] != DBNull.Value)
                this.EmailId = Convert.ToString(reader["EmailId"]);

            if (ColumnExists(reader, "PortName"))
                if (reader["PortName"] != DBNull.Value)
                    this.PortName = Convert.ToString(reader["PortName"]);

            if (ColumnExists(reader, "PortID"))
                if (reader["PortID"] != DBNull.Value)
                    this.PortID = Convert.ToInt32(reader["PortID"]);

            if (ColumnExists(reader, "UserActive") && reader["UserActive"] != DBNull.Value) this.IsActive = Convert.ToBoolean(reader["UserActive"]);
            if (ColumnExists(reader, "AllowMutipleLocation") && reader["AllowMutipleLocation"] != DBNull.Value) this.AllowMutipleLocation = Convert.ToBoolean(reader["AllowMutipleLocation"]);
        }

        #endregion

        #region Private Methods

        private bool HasColumn(DataTableReader reader, string columnName)
        {
            try
            {
                return reader.GetOrdinal(columnName) >= 0;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        public bool ColumnExists(IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).ToUpper() == columnName.ToUpper())
                {
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}
