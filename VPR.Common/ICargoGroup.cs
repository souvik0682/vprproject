﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VPR.Common
{
    public interface ICargoGroup : ICommon
    {
        int pk_CargoGroupID { get; set; }
        int CargoGroupID { get; set; }

        string CargoGroupName { get; set; }
        bool GroupStatus { get; set; }

        bool IsRemoved { get; set; }

        bool IsAdded { get; set; }
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
