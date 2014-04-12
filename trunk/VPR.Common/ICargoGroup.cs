using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VPR.Common
{
    public interface ICargoGroup : ICommon
    {
        string CargoGroupName { get; set; }
        int pk_CargoGroupID { get; set; }
        int fk_CargoGroupID { get; set; }
        string CargoSubGroupName { get; set; }
        int pk_CargoSubGroupID { get; set; }
        int fk_CargoSubGroupID { get; set; }
        string CargoName { get; set; }
        int pk_CargoID { get; set; }
        bool CargoStatus { get; set; }

        //bool IsRemoved { get; set; }
        //bool IsAdded { get; set; }
    }
}
