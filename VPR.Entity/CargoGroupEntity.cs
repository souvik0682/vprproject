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
        public int pk_CargoGroupID { get; set; }

        public int fk_CargoGroupID { get; set; }

        public string CargoGroupName { get; set; }

        public int pk_CargoSubGroupID { get; set; }

        public string CargoSubGroupName { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public CargoGroupEntity()
        {

        }
        public CargoGroupEntity(DataTableReader reader)
        {
            if (ColumnExists(reader, "pk_CargoGroupID"))
                if (reader["pk_CargoGroupID"] != DBNull.Value)
                    this.pk_CargoGroupID = Convert.ToInt32(reader["pk_CargoGroupID"]);

            if (ColumnExists(reader, "fk_CargoGroupID"))
                if (reader["fk_CargoGroupID"] != DBNull.Value)
                    this.fk_CargoGroupID = Convert.ToInt32(reader["fk_CargoGroupID"]);

            if (ColumnExists(reader, "pk_CargoSubGroupID"))
                if (reader["pk_CargoSubGroupID"] != DBNull.Value)
                    this.pk_CargoGroupID = Convert.ToInt32(reader["pk_CargoSubGroupID"]);

            if (ColumnExists(reader, "CargoGroupName"))
                this.CargoGroupName = Convert.ToString(reader["CargoGroupName"]);

            if (ColumnExists(reader, "CargoSubGroupName"))
                this.CargoSubGroupName = Convert.ToString(reader["CargoSubGroupName"]);

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