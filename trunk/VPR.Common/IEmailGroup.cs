using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VPR.Common
{
    public interface IEmailGroup:ICommon
    {
        int EmailGroupId { get; set; }
        string GroupName { get; set; }
        int CountryId { get; set; }
        string Subject { get; set; }
        string MailBody { get; set; }
        string Attachment { get; set; }
        string Frequency { get; set; }
        bool GroupStatus { get; set; }
        DateTime? SendingDate { get; set; }
        string SendingTime { get; set; }
        string SendOn { get; set; }
        int CargoGroupID { get; set; }

        List<IEmail> EmailList { get; set; }
    }
}
