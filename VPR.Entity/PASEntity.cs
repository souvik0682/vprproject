using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VPR.Common;
using System.Data;
namespace VPR.Entity
{
    public class PASEntity
    {
        public int PASTranId { get; set; }
        public string VesselActivity { get; set; }
        public int CoastId { get; set; }
        public string Movement { get; set; }
        public bool MovementType { get; set; }
        public DateTime? MovementDate { get; set; }
        public int PortId { get; set; }
        public int VesselID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public decimal Quantity { get; set; }
        public DateTime ETA { get; set; }
        public DateTime ETB { get; set; }
        public DateTime ETC { get; set; }

        public PASEntity()
        {
            
        }

         public PASEntity(DataTableReader reader)
        {
            if (ColumnExists(reader, "pk_PASTranID"))
                if (reader["pk_PASTranID"] != DBNull.Value)
                    this.PASTranId = Convert.ToInt32(reader["pk_PASTranID"]);

            if (ColumnExists(reader, "fk_CoastID"))
                if (reader["fk_CoastID"] != DBNull.Value)
                    this.CoastId = Convert.ToInt32(reader["fk_CoastID"]);

            if (ColumnExists(reader, "fk_PortID"))
                if (reader["fk_PortID"] != DBNull.Value)
                    this.PortId = Convert.ToInt32(reader["fk_PortID"]);

            if (ColumnExists(reader, "ActivityName"))
                if (reader["ActivityName"] != DBNull.Value)
                    this.VesselActivity = Convert.ToString(reader["ActivityName"]);

            if (ColumnExists(reader, "fk_VesselID"))
                if (reader["fk_VesselID"] != DBNull.Value)
                    this.VesselID = Convert.ToInt32(reader["fk_VesselID"]);

            if (ColumnExists(reader, "quantity"))
                if (reader["quantity"] != DBNull.Value)
                    this.Quantity = Convert.ToDecimal(reader["quantity"]);

            if (ColumnExists(reader, "ETA"))
                if (reader["ETA"] != DBNull.Value)
                    this.ETA = Convert.ToDateTime(reader["ETA"]);

            if (ColumnExists(reader, "ETB"))
                if (reader["ETB"] != DBNull.Value)
                    this.ETB = Convert.ToDateTime(reader["ETB"]);

            if (ColumnExists(reader, "ETC"))
                if (reader["ETC"] != DBNull.Value)
                    this.ETC = Convert.ToDateTime(reader["ETC"]);

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
