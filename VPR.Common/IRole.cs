﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VPR.Common
{
    public interface IRole : IBase<int>
    {
        bool? LocationSpecific { get; set; }
        bool? RoleStatus { get; set; }
        bool Active { get; set; }
    }
}
