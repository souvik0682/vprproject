using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VPR.Common
{
    public interface IVendor : ICommon
    {
        int VendorId { get; set; }
        //string VendorSalutation { get; set; }
        string VendorName { get; set; }
        string VendorAddress1 { get; set; }
        string VendorAddress2 { get; set; }
        string City { get; set; }
        bool VendorActive { get; set; }
        string State { get; set; }
        string Phone { get; set; }
        string Mobile { get; set; }
        string EmailID { get; set; }
        string CountryName { get; set; }
        //string BankName { get; set; }
        int fk_CountryID { get; set; }
       
        //string PAN { get; set; }

    }
}
