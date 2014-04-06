using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VPR.Common;
using VPR.DAL.DbManager;
using VPR.Entity;

namespace VPR.DAL
{
    public sealed class EmailDAL
    {
        private EmailDAL()
        {
        }

        public static List<IEmail> GetListOfAvailableEmail(int CountryId, int EmailGroupId)
        {
            string strExecution = "usp_GetListOfAvailableEmail";
            List<IEmail> lstEmail = new List<IEmail>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@CountryId", CountryId);

                if (EmailGroupId > 0)
                    oDq.AddIntegerParam("@EmailGroupId", EmailGroupId);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IEmail email = new Email(reader);
                    lstEmail.Add(email);
                }

                reader.Close();
            }

            return lstEmail;
        }

        public static List<IEmail> GetListOfTaggedEmail(int EmailGroupId)
        {
            string strExecution = "usp_GetListOfTaggedEmail";
            List<IEmail> lstEmail = new List<IEmail>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@EmailGroupId", EmailGroupId);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IEmail email = new Email(reader);
                    lstEmail.Add(email);
                }

                reader.Close();
            }

            return lstEmail;
        }

        public static bool IsEmailGroupExists(string GroupName)
        {
            string strExecution = "usp_EmailGroupExists";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@GroupName", 500, GroupName);

                return Convert.ToBoolean(oDq.GetScalar());
            }
        }

        public static int SaveEmailGroup(IEmailGroup EmailGroup)
        {
            int emailGroupId = 0;
            string strExecution = "usp_SaveEmailGroup";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                if (EmailGroup.EmailGroupId > 0)
                    oDq.AddIntegerParam("@EmailGroupId", EmailGroup.EmailGroupId);

                oDq.AddVarcharParam("@GroupName", 500, EmailGroup.GroupName);
                oDq.AddIntegerParam("@CountryId", EmailGroup.CountryId);
                oDq.AddVarcharParam("@Subject", 500, EmailGroup.Subject);
                oDq.AddVarcharParam("@MailBody", 2000, EmailGroup.MailBody);
                oDq.AddVarcharParam("@Attachment", 1, EmailGroup.Attachment);
                oDq.AddVarcharParam("@Frequency", 1, EmailGroup.Frequency);

                if (EmailGroup.SendingDate.HasValue)
                    oDq.AddDateTimeParam("@SendingDate", EmailGroup.SendingDate);

                oDq.AddVarcharParam("@SendingTime", 50, EmailGroup.SendingTime);
                oDq.AddVarcharParam("@SendOn", 50, EmailGroup.SendOn);
                oDq.AddBooleanParam("@GroupStatus", EmailGroup.GroupStatus);
                oDq.AddIntegerParam("@CreatedBy", EmailGroup.CreatedBy);
                ///oDq.AddDateTimeParam("@CreatedOn", EmailGroup.CreatedOn);
                oDq.AddIntegerParam("@ModifiedBy", EmailGroup.ModifiedBy);
                //oDq.AddDateTimeParam("@ModifiedOn", EmailGroup.ModifiedOn);

                emailGroupId = Convert.ToInt32(oDq.GetScalar());
                return emailGroupId;
            }
        }

        public static void Tag_Untag_Email(int EmailId, int EmailGroupId, bool IsTag)
        {
            string strExecution = "usp_Tag_Untag_Email";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@EmailId", EmailId);
                oDq.AddIntegerParam("@EmailGroupId", EmailGroupId);
                oDq.AddBooleanParam("@IsTag", IsTag);

                oDq.RunActionQuery();
            }
        }

        public static IEmailGroup GetEmailGroup(int GroupId)
        {
            string strExecution = "usp_GetEmailGroup";
            IEmailGroup objGroup = new EmailGroup();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@GroupId", GroupId);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    objGroup = new EmailGroup(reader);
                }
                reader.Close();
            }
            return objGroup;
        }

        public static List<IEmailGroup> GetEmailGroups(SearchCriteria searchCriteria)
        {
            string strExecution = "[uspGetEmailGroupList]";
            List<IEmailGroup> lstEg = new List<IEmailGroup>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@EmailGroup", 100, searchCriteria.EmailGroup);
                oDq.AddVarcharParam("@Country", 100, searchCriteria.Country);
                oDq.AddVarcharParam("@EmailId", 100, searchCriteria.EmailId);
                oDq.AddVarcharParam("@Subject", 100, searchCriteria.Subject);

                oDq.AddVarcharParam("@SortExpression", 100, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 100, searchCriteria.SortDirection);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IEmailGroup eg = new EmailGroup(reader);
                    lstEg.Add(eg);
                }
                reader.Close();
            }
            return lstEg;
        }

        public static void DeleteEmailGroup(int EmailGroupId)
        {
            string strExecution = "uspDeleteEmailGroup";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBigIntegerParam("@EmailGroupId", EmailGroupId);
                oDq.RunActionQuery();
            }
        }
    }
}
