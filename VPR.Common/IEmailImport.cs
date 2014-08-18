using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VPR.Common
{
    public interface IEmailImport
    {
        string Name { get; set; }
        string EmailID { get; set; }
    }
}
