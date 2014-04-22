using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VPR.Common;
using System.Data;

namespace VPR.Entity
{
    public class VendorEntity : IVendor
    {
        #region IUser Members
        public int VendorId
        {
            get;
            set;
        }

        //public int VendorSalutation
        //{
        //    get;
        //    set;
        //}

        public string VendorName
        {
            get;
            set;
        }

        public string CountryName
        {
            get;
            set;
        }

        public string VendorAddress1
        {
            get;
            set;
        }

        public string VendorAddress2
        {
            get;
            set;
        }

        public bool VendorActive
        {
            get;
            set;
        }

        public string City
        {
            get;
            set;
        }

        public string State
        {
            get;
            set;
        }

        public string Phone
        {
            get;
            set;
        }

        public string EmailID
        {
            get;
            set;
        }

        public string Mobile
        {
            get;
            set;
        }

        //public string PAN
        //{
        //    get;
        //    set;
        //}

        //public string BankName
        //{
        //    get;
        //    set;
        //}

        public int fk_CountryID
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

        public VendorEntity()
        {

        }


        public VendorEntity(DataTableReader reader)
        {
            //this.CFSCode = Convert.ToString(reader["CFSCode"]);
            //this.CompanyID = Convert.ToInt32(reader["Company"]);
            //this.LocationID = Convert.ToInt32(reader["Location"]);
            this.fk_CountryID = Convert.ToInt32(reader["fk_CountryID"]);
            //this.Terminalid = Convert.ToInt32(reader["Terminal"]);
            this.VendorAddress1 = Convert.ToString(reader["AgentAddress1"]);
            this.VendorAddress2 = Convert.ToString(reader["AgentAddress2"]);
            this.CountryName = Convert.ToString(reader["CountryName"]);
            this.VendorId = Convert.ToInt32(reader["pk_AgentId"]);
            this.VendorName = Convert.ToString(reader["AgentName"]);
            //this.VendorSalutation = Convert.ToInt32(reader["Salutation"]);
            this.City = Convert.ToString(reader["AgentCity"]);
            //this.PAN = Convert.ToString(reader["PAN"]);
            this.State = Convert.ToString(reader["AgentState"]);
            this.Phone = Convert.ToString(reader["AgentPhone"]);
            //this.BankName = Convert.ToString(reader["BankName"]);
            this.EmailID = Convert.ToString(reader["AgentMailID"]);
            this.Mobile = Convert.ToString(reader["AgentMobile"]);
          
        }

        #endregion
    }
}
