using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace VPR.Entity
{
    [Serializable]
    public class VesselPosition
    {

        public string VesselName { get; set; }
        public string BerthName { get; set; }
        public string Names { get; set; }
        public string ArrivalDate { get; set; }
        public string BerthDate { get; set; }
        public string ETC { get; set; }
        public string Remarks { get; set; }
        public string PortName { get; set; }
        public string ActivityStatus { get; set; }

        public VesselPosition(DataTableReader reader)
        {
            this.VesselName = Convert.ToString(reader["VesselName"]);
            this.BerthName = Convert.ToString(reader["BerthName"]);
            this.Names = Convert.ToString(reader["Names"]);
            this.ArrivalDate = Convert.ToString(reader["ArrivalDate"]);
            this.BerthDate = Convert.ToString(reader["BerthDate"]);
            this.ETC = Convert.ToString(reader["ETC"]);
            this.Remarks = Convert.ToString(reader["Remarks"]);
            this.PortName = Convert.ToString(reader["PortName"]);
            this.ActivityStatus = Convert.ToString(reader["ActivityStatus"]);

        }
    }
}
