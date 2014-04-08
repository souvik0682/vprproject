﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VPR.Common;

namespace VPR.Entity
{
    public class LocationEntity : ILocation
    {
        #region ILocation Members

        public IAddress LocAddress
        {
            get;
            set;
        }

        public string Abbreviation
        {
            get;
            set;
        }

        public string Phone
        {
            get;
            set;
        }

        public int? PGRFreeDays
        {
            get;
            set;
        }

        public string CanFooter
        {
            get;
            set;
        }

        public string SlotFooter
        {
            get;
            set;
        }

        public string CartingFooter
        {
            get;
            set;
        }

        public string PickUpFooter
        {
            get;
            set;
        }

        public string CustomHouseCode
        {
            get;
            set;
        }

        public string GatewayPort
        {
            get;
            set;
        }

        public string ICEGateLoginD
        {
            get;
            set;
        }

        public string PCSLoginID
        {
            get;
            set;
        }

        public string ISO20
        {
            get;
            set;
        }

        public string ISO40
        {
            get;
            set;
        }

        public char IsActive
        {
            get;
            set;
        }

        public int DefaultLocation
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

        #region Constructors

        public LocationEntity()
        {
            this.LocAddress = new AddressEntity();
        }

        public LocationEntity(DataTableReader reader)
        {
            this.Id = Convert.ToInt32(reader["Id"]);
            this.Name = Convert.ToString(reader["Name"]);
            this.LocAddress = new AddressEntity(reader);
            this.Abbreviation = Convert.ToString(reader["LocAbbr"]);
            this.Phone = Convert.ToString(reader["LocPhone"]);
            this.IsActive = Convert.ToChar(reader["Active"]);

            if (ColumnExists(reader, "DefaultLocID"))
            {
                if (reader["DefaultLocID"] != DBNull.Value)
                    this.DefaultLocation = Convert.ToInt32(reader["DefaultLocID"]);
            }
        }

        #endregion

        public bool ColumnExists(IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i) == columnName)
                {
                    return true;
                }
            }

            return false;
        }
    }


}
