﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VPR.Common;
using System.Data;

namespace VPR.Entity
{
    public class VesselMovementEntity
    {
        public int VesselId { get; set; }
        public int PASTranID { get; set; }
        public string VesselActivity { get; set; }
        public string MovementType { get; set; }
        public string PortName { get; set; }
        public DateTime? MovementDate { get; set; }
        public string Movement { get; set; }
        public string VesselName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool AutoGenerated { get; set; }

        public VesselMovementEntity()
        {

        }

        public VesselMovementEntity(DataTableReader reader)
        {
            if (ColumnExists(reader, "fk_VesselID"))
                if (reader["fk_VesselID"] != DBNull.Value)
                    this.VesselId = Convert.ToInt32(reader["fk_VesselID"]);


            if (ColumnExists(reader, "pk_PASTranID"))
                this.PASTranID = Convert.ToInt32(reader["pk_PASTranID"]);

            if (ColumnExists(reader, "PortName"))
                if (reader["PortName"] != DBNull.Value)
                    this.PortName = Convert.ToString(reader["PortName"]);

            if (ColumnExists(reader, "VesselName"))
                this.VesselName = Convert.ToString(reader["VesselName"]);

            if (ColumnExists(reader, "MovementType"))
                if (reader["MovementType"] != DBNull.Value)
                    this.MovementType = Convert.ToString(reader["MovementType"]);

            if (ColumnExists(reader, "Movement"))
                if (reader["Movement"] != DBNull.Value)
                    this.Movement = Convert.ToString(reader["Movement"]);

            if (ColumnExists(reader, "MovementDate"))
                if (reader["MovementDate"] != DBNull.Value)
                    this.MovementDate = Convert.ToDateTime(reader["MovementDate"]);

            if (ColumnExists(reader, "VesselActivity"))
                if (reader["VesselActivity"] != DBNull.Value)
                    this.VesselActivity = Convert.ToString(reader["VesselActivity"]);

            if (ColumnExists(reader, "fk_UserAdded"))
                if (reader["fk_UserAdded"] != DBNull.Value)
                    this.CreatedBy = Convert.ToInt32(reader["fk_UserAdded"]);

            if (ColumnExists(reader, "Autogenerated"))
                if (reader["Autogenerated"] != DBNull.Value)
                    this.AutoGenerated = Convert.ToBoolean(reader["Autogenerated"]);

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