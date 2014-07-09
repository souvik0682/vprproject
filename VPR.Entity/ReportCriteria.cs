using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VPR.Utilities;

namespace VPR.Entity
{
    [Serializable]
    public class ReportCriteria 
    {
        #region Public Properties

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int CountryId { get; set; }
        public string TransactionType { get; set; }
        public int CargoId { get; set; }
        public int CargoGroupId { get; set; }
        public int SubGroupID { get; set; }
        public int PortId { get; set; }
        public string CountryName { get; set; }
        public string Activity { get; set; }


        #endregion

        #region Constructor

        #endregion

        #region Public Methods

        public void Clear()
        {

        }

        #endregion
    }
}
