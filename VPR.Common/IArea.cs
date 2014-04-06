﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VPR.Common
{
    public interface IArea : IBase<int>, ICommon
    {
        string PinCode { get; set; }
        ILocation Location { get; set; }
        char IsActive { get; set; }
    }
}
