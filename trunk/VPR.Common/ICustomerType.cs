using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VPR.Common
{
    public interface ICustomerType : IBase<int>
    {
        char IsActive { get; set; }
    }
}
