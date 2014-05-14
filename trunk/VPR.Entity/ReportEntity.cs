using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VPR.Common;
using System.Data.SqlClient;
using System.Data.Common;

namespace VPR.Entity
{
    public class ReportEntity : IReport
    {
        #region IReport Members

        public string VesselName { get; set; }
        public string PortName { get; set; }
        public string Activity { get; set; }
        public string PrevPort { get; set; }
        public string NextPort { get; set; }
        public string ArrivalDate { get; set; }
        public string BerthDate { get; set; }
        public string SailingDate { get; set; }
        public string OwnerName { get; set; }
        public string Remarks { get; set; }
        public string CargoName { get; set; }
        public decimal Quantity { get; set; }

        #endregion

        #region Constructors

        public ReportEntity()
        {

        }

        public ReportEntity(DbDataReader reader)
        {

        }

        #endregion

        #region Public Methods

        public void CargoReport(DbDataReader reader)
        {
            this.VesselName = Convert.ToString(reader["VesselName"]);
            this.PortName = Convert.ToString(reader["PortName"]);
            this.PrevPort = Convert.ToString(reader["PrevPort"]);
            this.NextPort = Convert.ToString(reader["NextPort"]);
            this.ArrivalDate = Convert.ToString(reader["ArrivalDate"]);
            this.BerthDate = Convert.ToString(reader["BerthDate"]);
            this.SailingDate = Convert.ToString(reader["SailingDate"]);
            this.OwnerName = Convert.ToString(reader["Owner"]);
            this.CargoName = Convert.ToString(reader["CargoName"]);
            this.Quantity = Convert.ToDecimal(reader["Quantity"]);

        }


        #endregion
    }
}
