using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VPR.Common
{
    public interface IEmail : ICommon
    {
        int Id { get; set; }
        string Suffix { get; set; }
        string Salutation { get; set; }
        string Name { get; set; }
        string EmailId { get; set; }
        string EmailId1 { get; set; }
        string EmailId2 { get; set; }
        string EmailId3 { get; set; }
        string Company { get; set; }
        int CountryId { get; set; }
        int AttachmentId { get; set; }
        bool MailStatus { get; set; }
        string CompanyAbbr { get; set; }

        bool IsRemoved { get; set; }
        bool IsAdded { get; set; }
    }
}
