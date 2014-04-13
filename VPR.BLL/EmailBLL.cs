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

            Tag_Untag_CargoGroups(EmailGroup.CargoGroupList, EmailGroupId, false);
        }

        public IEmailGroup GetEmailGroup(int GroupId)
        {
            return EmailDAL.GetEmailGroup(GroupId);
        }

        public List<IEmailGroup> GetEmailGroups(SearchCriteria searchCriteria)
        {
            return EmailDAL.GetEmailGroups(searchCriteria);
        }

        public void DeleteEmailOrEmailGroup(int Id, bool IsEmail)
        {
            EmailDAL.DeleteEmailOrEmailGroup(Id, IsEmail);
        }

        public DataTable GetAllCargoGroup()
        {
            return EmailDAL.GetAllCargoGroup();
        }

        #endregion

        #region Email
        public bool IsEmailExists(string EmailId)
        {
            return EmailDAL.IsEmailExists(EmailId);
        }

        public List<ICargoGroup> GetListOfAvailableCargoGroup(int Id, bool IsEmail)
        {
            return EmailDAL.GetListOfAvailableCargoGroup(Id, IsEmail);
        }
        public List<ICargoGroup> GetListOfTaggedCargoGroup(int Id, bool IsEmail)
        {
            return EmailDAL.GetListOfTaggedCargoGroup(Id, IsEmail);
        }

        public void SaveEmail(IEmail Email)
        {
            int EmailId = 0;

            EmailId = EmailDAL.SaveEmail(Email);

            Tag_Untag_CargoGroups(Email.CargoGroupList, EmailId, true);
        }

        private void Tag_Untag_CargoGroups(List<ICargoGroup> CargoGroups, int Id, bool IsEmail)
        {
            CargoGroups = CargoGroups.Where(e => e.IsAdded == true || e.IsRemoved == true).ToList();

            foreach (ICargoGroup group in CargoGroups)
            {
                if (group.IsAdded)
                    EmailDAL.Tag_Untag_CargoGroup(group.CargoGroupID, Id, true, IsEmail);
                else if (group.IsRemoved)
                    EmailDAL.Tag_Untag_CargoGroup(group.CargoGroupID, Id, false, IsEmail);
            }
        }

        public IEmail GetEmail(int EmailId)
        {
            return EmailDAL.GetEmail(EmailId);
        }

        public List<IEmail> GetEmails(SearchCriteria searchCriteria)
        {
            return EmailDAL.GetEmails(searchCriteria);
        }

        #endregion
    }
}
