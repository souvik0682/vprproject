using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VPR.Common;
using System.Data;

namespace VPR.Entity
{
    [Serializable]
    public class Email : IEmail
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string EmailId { get; set; }

        public string Company { get; set; }

        public int CountryId { get; set; }

        public int AttachmentId { get; set; }

        public bool MailStatus { get; set; }

        public string CompanyAbbr { get; set; }

        public bool IsRemoved { get; set; }

        public bool IsAdded { get; set; }

        public string Suffix { get; set; }

        public string Salutation { get; set; }

        public string EmailId1 { get; set; }

        public string EmailId2 { get; set; }

        public string EmailId3 { get; set; }

        public string CargoGroup { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public List<ICargoGroup> CargoGroupList { get; set; }

        public Email()
        {
        }

        public Email(DataTableReader reader)
        {
            if (ColumnExists(reader, "pk_EmailID"))
                if (reader["pk_EmailID"] != DBNull.Value)
                    this.Id = Convert.ToInt32(reader["pk_EmailID"]);

            if (ColumnExists(reader, "Suffix"))
                this.Suffix = Convert.ToString(reader["Suffix"]);

            if (ColumnExists(reader, "Salutation"))
                this.Salutation = Convert.ToString(reader["Salutation"]);

            if (ColumnExists(reader, "ReceiverName"))
                this.Name = Convert.ToString(reader["ReceiverName"]);

            if (ColumnExists(reader, "emailIDActive"))
                this.EmailId = Convert.ToString(reader["emailIDActive"]);

            if (ColumnExists(reader, "emailIDAdd1"))
                this.EmailId1 = Convert.ToString(reader["emailIDAdd1"]);

            if (ColumnExists(reader, "emailIDAdd2"))
                this.EmailId2 = Convert.ToString(reader["emailIDAdd2"]);

            if (ColumnExists(reader, "emailIDAdd3"))
                this.EmailId3 = Convert.ToString(reader["emailIDAdd3"]);

            if (ColumnExists(reader, "CompanyName"))
                this.Company = Convert.ToString(reader["CompanyName"]);

            if (ColumnExists(reader, "CompanyAbbr"))
                this.CompanyAbbr = Convert.ToString(reader["CompanyAbbr"]);

            if (ColumnExists(reader, "CargoGroup"))
                this.CargoGroup = Convert.ToString(reader["CargoGroup"]);

            if (ColumnExists(reader, "fk_countryID"))
                if (reader["fk_countryID"] != DBNull.Value)
                    this.CountryId = Convert.ToInt32(reader["fk_countryID"]);

            if (ColumnExists(reader, "MailStatus"))
                if (reader["MailStatus"] != DBNull.Value)
                    this.MailStatus = Convert.ToBoolean(reader["MailStatus"]);

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
