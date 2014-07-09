using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VPR.Common;
using VPR.Utilities;

namespace VPR.Entity
{
    [Serializable]
    public class SearchCriteria : ISearchCriteria
    {
        #region Public Properties

        public string StringOption1 { get; set; }
        public string StringOption2 { get; set; }
        public string StringOption3 { get; set; }
        public string StringOption4 { get; set; }
        public string StringOption5 { get; set; }

        public int IntegerOption1 { get; set; }
        public int IntegerOption2 { get; set; }
        public int IntegerOption3 { get; set; }

        public string LocationFrom { get; set; }
        public string LocationTo { get; set; }
        public string ContainerSize { get; set; }
        public string RoleName { get; set; }
        public DateTime? Date { get; set; }

        public string ChargeName { get; set; }
        public char ChargeType { get; set; }
        public string LineName { get; set; }

        public string IGMBLNo { get; set; }
        public string LineBLNo { get; set; }
        public DateTime? BLDate { get; set; }
        public string ContainerNo { get; set; }
        public string Vessel { get; set; }
        public string Voyage { get; set; }
        public int VoyageID { get; set; }

        public string POL { get; set; }
        public string POD { get; set; }
        public string Location { get; set; }
	
	    public string InvoiceNo { get; set; }
        public string BLNo { get; set; }
        public string BookingNo { get; set; }
        public string ImportExport { get; set; }

        public string SlotOperatorName { get; set; }
        public string AgentName { get; set; }
        public string ServiceName { get; set; }
        public string Terminal { get; set; }
        public int LocationID { get; set; }
        public IList<string> StringParams { get; set; }

        public string DONumber { get; set; }
        public string BookingRef { get; set; }

        public string LocAbbr
        {
            get;
            set;
        }

        public string LocName
        {
            get;
            set;
        }

        public string AreaName
        {
            get;
            set;
        }

        public string GroupName
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        public string CustomerName
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string ExecutiveName
        {
            get;
            set;
        }

        public int UserId
        {
            get;
            set;
        }

        public string SortExpression
        {
            get;
            set;
        }

        public string SortDirection
        {
            get;
            set;
        }

        public PageName CurrentPage
        {
            get;
            set;
        }

        public int PageIndex
        {
            get;
            set;
        }

        public int PageSize
        {
            get;
            set;
        }

        public string EdgeBLNumber { get; set; }
        public string RefBLNumber { get; set; }

        //Souvik
        public string EmailGroup { get; set; }
        public string Country { get; set; }
        public string EmailId { get; set; }
        public string Subject { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string VesselName { get; set; }
        public string Port { get; set; }
        public Int32 portID { get; set; }
        public string Berth { get; set; }
        public string VesselStatus { get; set; }

        // tapas
        public string CargoGroup { get; set; }
        public string CargoSubGroup { get; set; }
        public string CargoName { get; set; }
        public string CargoGroupType { get; set; }
        public string Agent { get; set; }
        public string Banner { get; set; }
        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
        public string ActivityName { get; set; }

        #endregion

        #region Constructor

        public SearchCriteria()
        {
            StringParams = new List<string>();
        }

        #endregion

        #region Public Methods

        public void Clear()
        {
            this.RoleName = string.Empty;
            this.AreaName = string.Empty;
            this.CustomerName = string.Empty;
            this.ExecutiveName = string.Empty;
            this.FirstName = string.Empty;
            this.GroupName = string.Empty;
            this.LocAbbr = string.Empty;
            this.LocName = string.Empty;
            this.SortDirection = string.Empty;
            this.SortExpression = string.Empty;
            this.UserId = 0;
            this.UserName = string.Empty;
            this.CurrentPage = 0;
            this.PageIndex = 0;
            this.PageSize = 0;
            this.BLDate = null;
            this.ContainerNo = string.Empty;
            this.IGMBLNo = string.Empty;
            this.LineBLNo = string.Empty;
            this.POD = string.Empty;
            this.POL = string.Empty;
            this.Vessel = string.Empty; 
            this.Voyage = string.Empty;
            this.SlotOperatorName = string.Empty;
            this.AgentName = string.Empty;
            this.ServiceName = string.Empty;
            this.Terminal = string.Empty;
            this.VoyageID = 0;
            this.DONumber = string.Empty;
            this.EdgeBLNumber = string.Empty;
            this.RefBLNumber = string.Empty;

            //Souvik
            this.EmailGroup = string.Empty;
            this.Country = string.Empty;
            this.EmailId = string.Empty;
            this.Subject = string.Empty;

            // tapas
            this.CargoGroup = string.Empty;
            this.CargoSubGroup = string.Empty;
            this.CargoGroupType = string.Empty;
            this.Banner = string.Empty;
            this.DocumentName = string.Empty;
            this.DocumentType = string.Empty;
            this.ActivityName = string.Empty;
            this.portID = 0;
            
        }

        #endregion      
    }
}
