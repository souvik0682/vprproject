using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VPR.DAL;
using VPR.Common;
using VPR.Entity;
using VPR.Utilities;
using System.Web;
using VPR.Utilities.ResourceManager;
using VPR.Utilities.Cryptography;
using System.Data;

namespace VPR.BLL
{
    public class EmailBLL
    {
        #region Email Group

        public bool IsEmailGroupExists(string GroupName)
        {
            return EmailDAL.IsEmailGroupExists(GroupName);
        }

        public void SaveEmailGroup(IEmailGroup EmailGroup)
        {
            int EmailGroupId = 0;

            EmailGroupId = EmailDAL.SaveEmailGroup(EmailGroup);

            //Tagg_UnTagg_Emails(EmailGroup.EmailList, EmailGroupId);
        }

        public IEmailGroup GetEmailGroup(int GroupId)
        {
            return EmailDAL.GetEmailGroup(GroupId);
        }

        public List<IEmailGroup> GetEmailGroups(SearchCriteria searchCriteria)
        {
            return EmailDAL.GetEmailGroups(searchCriteria);
        }

        public void DeleteEmailGroup(int EmailGroupId)
        {
            EmailDAL.DeleteEmailGroup(EmailGroupId);
        }

        public DataTable GetAllCargoGroup()
        {
            return EmailDAL.GetAllCargoGroup();
        }

        #endregion

        #region Email

        public List<IEmail> GetListOfAvailableEmail(int CountryId, int EmailGroupId)
        {
            return EmailDAL.GetListOfAvailableEmail(CountryId, EmailGroupId);
        }
        public List<IEmail> GetListOfTaggedEmail(int EmailGroupId)
        {
            return EmailDAL.GetListOfTaggedEmail(EmailGroupId);
        }
        private void Tagg_UnTagg_Emails(List<IEmail> Emails, int EmailGroupId)
        {
            Emails = Emails.Where(e => e.IsAdded == true || e.IsRemoved == true).ToList();

            foreach (IEmail Email in Emails)
            {
                if (Email.IsAdded)
                    EmailDAL.Tag_Untag_Email(Email.Id, EmailGroupId, true);
                else if (Email.IsRemoved)
                    EmailDAL.Tag_Untag_Email(Email.Id, EmailGroupId, false);
            }
        }

        #endregion
    }
}
