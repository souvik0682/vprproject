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

        #region Email Group

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

                oDq.AddIntegerParam("@CargoGroupID", EmailGroup.CargoGroupID);
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
            string strExecution = "uspGetEmailGroupList";
            List<IEmailGroup> lstEg = new List<IEmailGroup>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@EmailGroup", 100, searchCriteria.EmailGroup);
                oDq.AddVarcharParam("@Country", 100, searchCriteria.Country);
                oDq.AddVarcharParam("@CargoGroup", 100, searchCriteria.CargoGroup);
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

        public static void DeleteEmailOrEmailGroup(int Id, bool IsEmail)
        {
            string strExecution = "uspDeleteEmailOrEmailGroup";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@Id", Id);
                oDq.AddBooleanParam("@IsEmail", IsEmail);
                oDq.RunActionQuery();
            }
        }

        public static DataTable GetAllCargoGroup()
        {
            string ProcName = "uspGetAllCargoGroups";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            return dquery.GetTable();
        }

        #endregion

        #region Email
        public static bool IsEmailExists(string EmailId)
        {
            string strExecution = "usp_EmailExists";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@EmailID", 200, EmailId);

                return Convert.ToBoolean(oDq.GetScalar());
            }
        }

        public static int SaveEmail(IEmail EmailGroup)
        {
            int emailGroupId = 0;
            string strExecution = "usp_SaveEmail";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                if (EmailGroup.Id > 0)
                    oDq.AddIntegerParam("@Id", EmailGroup.Id);

                oDq.AddVarcharParam("@Suffix", 500, EmailGroup.Suffix);
                oDq.AddVarcharParam("@Salutation", 500, EmailGroup.Salutation);
                oDq.AddVarcharParam("@Name", 500, EmailGroup.Name);
                oDq.AddVarcharParam("@EmailId", 500, EmailGroup.EmailId);
                oDq.AddVarcharParam("@EmailId1", 2000, EmailGroup.EmailId1);
                oDq.AddVarcharParam("@EmailId2", 1, EmailGroup.EmailId2);
                oDq.AddVarcharParam("@EmailId3", 1, EmailGroup.EmailId3);
                oDq.AddVarcharParam("@Company", 50, EmailGroup.Company);
                oDq.AddVarcharParam("@CompanyAbbr", 50, EmailGroup.CompanyAbbr);
                oDq.AddBooleanParam("@MailStatus", EmailGroup.MailStatus);
                oDq.AddIntegerParam("@CountryId", EmailGroup.CountryId);

                oDq.AddIntegerParam("@CreatedBy", EmailGroup.CreatedBy);
                oDq.AddIntegerParam("@ModifiedBy", EmailGroup.ModifiedBy);

                emailGroupId = Convert.ToInt32(oDq.GetScalar());
                return emailGroupId;
            }
        }

        public static List<ICargoGroup> GetListOfAvailableCargoGroup(int Id, bool IsEmail)
        {
            string strExecution = "usp_GetListOfAvailableCargoSubGroup";
            List<ICargoGroup> lstEmail = new List<ICargoGroup>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                if (Id > 0)
                    oDq.AddIntegerParam("@Id", Id);

                oDq.AddBooleanParam("@IsEmail", IsEmail);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    ICargoGroup email = new CargoGroupEntity(reader);
                    lstEmail.Add(email);
                }

                reader.Close();
            }

            return lstEmail;
        }

        public static List<ICargoGroup> GetListOfTaggedCargoGroup(int Id, bool IsEmail)
        {
            string strExecution = "usp_GetListOfTaggedCargoSubGroup";
            List<ICargoGroup> lstEmail = new List<ICargoGroup>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@Id", Id);
                oDq.AddBooleanParam("@IsEmail", IsEmail);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    ICargoGroup email = new CargoGroupEntity(reader);
                    lstEmail.Add(email);
                }

                reader.Close();
            }

            return lstEmail;
        }

        public static void Tag_Untag_CargoGroup(int CargoGroupId, int Id, bool IsTag, bool IsEmail)
        {
            string strExecution = "usp_Tag_Untag_CargoGroup";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@CargoGroupId", CargoGroupId);
                oDq.AddIntegerParam("@Id", Id);
                oDq.AddBooleanParam("@IsTag", IsTag);
                oDq.AddBooleanParam("@IsEmail", IsEmail);

                oDq.RunActionQuery();
            }
        }

        public static IEmail GetEmail(int EmailId)
        {
            string strExecution = "usp_GetEmail";
            IEmail objGroup = new Email();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@EmailId", EmailId);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    objGroup = new Email(reader);
                }
                reader.Close();
            }
            return objGroup;
        }

        public static List<IEmail> GetEmails(SearchCriteria searchCriteria)
        {
            string strExecution = "uspGetEmailList";
            List<IEmail> lstEg = new List<IEmail>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@Name", 500, searchCriteria.Name);
                oDq.AddVarcharParam("@EmailId", 500, searchCriteria.EmailId);
                oDq.AddVarcharParam("@Company", 100, searchCriteria.Company);
                oDq.AddVarcharParam("@CargoGroup", 100, searchCriteria.CargoGroup);

                oDq.AddVarcharParam("@SortExpression", 100, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 100, searchCriteria.SortDirection);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IEmail eg = new Email(reader);
                    lstEg.Add(eg);
                }
                reader.Close();
            }
            return lstEg;
        }

        #endregion
    }
}
