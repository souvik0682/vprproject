using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VPR.Common;
using System.Data;

namespace VPR.Entity
{
    [Serializable]
    public class CargoGroupEntity : ICargoGroup
    {
        public int  pk_CargoGroupID { get; set; }

        public int CargoGroupID { get; set; }

        public int fk_CargoGroupID { get; set; }

        public string CargoGroupName { get; set; }

        public bool GroupStatus { get; set; }

        public int pk_CargoSubGroupID { get; set; }

        public int fk_CargoSubGroupID { get; set; }

        public string CargoSubGroupName { get; set; }

        public int pk_CargoID { get; set; }

        public string CargoName { get; set; }

        public bool CargoStatus { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public bool IsRemoved { get; set; }

        public bool IsAdded { get; set; }

        public CargoGroupEntity()
        {

        }
        public CargoGroupEntity(DataTableReader reader)
        {
            if (ColumnExists(reader, "pk_CargoGroupID"))
                if (reader["pk_CargoGroupID"] != DBNull.Value)
                    this.pk_CargoGroupID = Convert.ToInt32(reader["pk_CargoGroupID"]);

            if (ColumnExists(reader, "pk_CargoGroupID"))
                if (reader["pk_CargoGroupID"] != DBNull.Value)
                    this.CargoGroupID = Convert.ToInt32(reader["pk_CargoGroupID"]);

            if (ColumnExists(reader, "fk_GroupID"))
                if (reader["fk_GroupID"] != DBNull.Value)
                    this.fk_CargoGroupID = Convert.ToInt32(reader["fk_GroupID"]);

            if (ColumnExists(reader, "fk_SubGroupID"))
                if (reader["fk_SubGroupID"] != DBNull.Value)
                    this.fk_CargoSubGroupID = Convert.ToInt32(reader["fk_SubGroupID"]);

            if (ColumnExists(reader, "pk_CargoSubGroupID"))
                if (reader["pk_CargoSubGroupID"] != DBNull.Value)
                    this.pk_CargoSubGroupID = Convert.ToInt32(reader["pk_CargoSubGroupID"]);

            if (ColumnExists(reader, "CargoGroupName"))
                if (reader["CargoGroupName"] != DBNull.Value)
                    this.CargoGroupName = Convert.ToString(reader["CargoGroupName"]);
            if (ColumnExists(reader, "GroupStatus"))
                this.GroupStatus = Convert.ToBoolean(reader["GroupStatus"]);
           
            if (ColumnExists(reader, "CargoSubGroupName"))
                if (reader["CargoSubGroupName"] != DBNull.Value)
                    this.CargoSubGroupName = Convert.ToString(reader["CargoSubGroupName"]);

            if (ColumnExists(reader, "CargoName"))
                if (reader["CargoName"] != DBNull.Value)
                    this.CargoName = Convert.ToString(reader["CargoName"]);

            if (ColumnExists(reader, "pk_CargoID"))
                if (reader["pk_CargoID"] != DBNull.Value)
                    this.pk_CargoID = Convert.ToInt32(reader["pk_CargoID"]);

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

            if (ColumnExists(reader, "CargoStatus"))
                this.CargoStatus = Convert.ToBoolean(reader["CargoStatus"]);
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