﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VPR.Common
{
    public interface IContactPerson
    {
        string Name { get; set; }
        string Designation { get; set; }
        string Mobile { get; set; }
        string EmailId { get; set; }
    }
}
