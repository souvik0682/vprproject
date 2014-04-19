using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VPR.Common;
using System.Data;


namespace VPR.Entity
{
    public class VesselStatus
    {
        public int VesselId { get; set; }
        public string Activity { get; set; }
        public string Vessel { get; set; }
        public string LOA { get; set; }
        public string BirthNo { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public DateTime? BerthDate { get; set; }
        public DateTime? DischargeDate { get; set; }
        public DateTime? LoadingDate { get; set; }
        public string Cargo { get; set; }
        public string CargoQuantity { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
