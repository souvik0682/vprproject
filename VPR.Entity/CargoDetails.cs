using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VPR.Common;
using System.Data;

namespace VPR.Entity
{
    [Serializable]
    public class CargoDetails
    {
        public int CargoVesselId { get; set; }
        public int VesselId { get; set; }
        public decimal Quantiry { get; set; }
        public int CargoId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsNew { get; set; }
    }
}
