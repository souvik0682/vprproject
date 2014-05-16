using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VPR.Common
{
    public interface IReport
    {
        String VesselName { get; set; }
        String PortName { get; set; }
        String Activity { get; set; }
        String PrevPort { get; set; }
        String NextPort { get; set; }
        String ArrivalDate { get; set; }
        String BerthDate { get; set; }
        String SailingDate { get; set; }
        String OwnerName { get; set; }
        String Remarks { get; set; }
        String CargoName { get; set; }
        Decimal Quantity { get; set; }

    }
}
