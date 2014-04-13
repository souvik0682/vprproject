using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VPR.Common;
using System.Data;

namespace VPR.Entity
{
    public class EmailGroup : IEmailGroup
    {

        public int EmailGroupId { get; set; }
        public string GroupName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string Subject { get; set; }
        public string MailBody { get; set; }
        public string Attachment { get; set; }
        public string Frequency { get; set; }
        public bool GroupStatus { get; set; }
        public DateTime? SendingDate { get; set; }
        public string SendingTime { get; set; }
        public string SendOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int CargoGroupID { get; set; }

        public List<ICargoGroup> CargoGroupList { get; set; }

        public EmailGroup()
        {
        }
        public EmailGroup(DataTableReader reader)
        {
            if (ColumnExists(reader, "pk_EmailGroupID"))
                if (reader["pk_EmailGroupID"] != DBNull.Value)
                    this.EmailGroupId = Convert.ToInt32(reader["pk_EmailGroupID"]);

            if (ColumnExists(reader, "fk_CargoGroupID"))
                if (reader["fk_CargoGroupID"] != DBNull.Value)
                    this.CargoGroupID = Convert.ToInt32(reader["fk_CargoGroupID"]);

            if (ColumnExists(reader, "GroupName"))
                this.GroupName = Convert.ToString(reader["GroupName"]);

            if (ColumnExists(reader, "fk_CountryID"))
                if (reader["fk_CountryID"] != DBNull.Value)
                    this.CountryId = Convert.ToInt32(reader["fk_CountryID"]);

            if (ColumnExists(reader, "Subject"))
                this.Subject = Convert.ToString(reader["Subject"]);

            if (ColumnExists(reader, "emailBody"))
                this.MailBody = Convert.ToString(reader["emailBody"]);

            if (ColumnExists(reader, "Attachment"))
                this.Attachment = Convert.ToString(reader["Attachment"]);

            if (ColumnExists(reader, "Frequency"))
                this.Frequency = Convert.ToString(reader["Frequency"]);

            if (ColumnExists(reader, "SendingDate"))
                if (reader["SendingDate"] != DBNull.Value)
                    this.SendingDate = Convert.ToDateTime(reader["SendingDate"]);

            if (ColumnExists(reader, "SendingTime"))
                this.SendingTime = Convert.ToString(reader["SendingTime"]);

            if (ColumnExists(reader, "SendOn"))
                this.SendOn = Convert.ToString(reader["SendOn"]);

            if (ColumnExists(reader, "EmailGroupStatus"))
                this.GroupStatus = Convert.ToBoolean(reader["EmailGroupStatus"]);

            if (ColumnExists(reader, "fk_UserAdded"))
                if (reader["fk_UserAdded"] != DBNull.Value)
                    this.CreatedBy = Convert.ToInt32(reader["fk_UserAdded"]);

            if (ColumnExists(reader, "fk_UserLastEdited"))
                if (reader["fk_UserLastEdited"] != DBNull.Value)
                    this.ModifiedBy = Convert.ToInt32(reader["fk_UserLastEdited"]);

            if (ColumnExists(reader, "AddedOn"))
                if (reader["AddedOn"] != DBNull.Value)
                    this.CreatedOn = Convert.ToDateTime(reader["AddedOn"]);

            if (ColumnExists(reader, "EditedOn"))
                if (reader["EditedOn"] != DBNull.Value)
                    this.ModifiedOn = Convert.ToDateTime(reader["EditedOn"]);

            if (ColumnExists(reader, "CountryName"))
                this.CountryName = Convert.ToString(reader["CountryName"]);
        }

        public bool ColumnExists(IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i) == columnName)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
