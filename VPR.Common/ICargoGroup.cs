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
        //bool IsRemoved { get; set; }
        //bool IsAdded { get; set; }
    }
}
