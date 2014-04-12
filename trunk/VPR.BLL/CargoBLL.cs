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
    public class CargoBLL
    {
        public void SaveCargo(ICargoGroup Cargo)
        {
            int CargoId = 0;

            CargoId = CargoGroupDAL.SaveCargo(Cargo);
            //Tagg_UnTagg_Emails(EmailGroup.EmailList, EmailGroupId);
        }

        public bool IsCargoExists(string CargoName)
        {
            return CargoGroupDAL.IsCargoExists(CargoName);
        }

        public ICargoGroup GetCargo(int CargoId)
        {
            return CargoGroupDAL.GetCargo(CargoId);
        }

        public DataTable GetAllCargoSubGroup(int CargoGroupID)
        {
            return CargoGroupDAL.GetAllCargoSubGroup(CargoGroupID);
        }


    }
}
