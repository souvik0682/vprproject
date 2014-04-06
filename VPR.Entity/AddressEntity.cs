﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VPR.Common;

namespace VPR.Entity
{
    public class AddressEntity : IAddress
    {
        #region IAddress Members

        public string Address
        {
            get;
            set;
        }

        public string City
        {
            get;
            set;
        }

        public string Pin
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public AddressEntity()
        {

        }

        public AddressEntity(DataTableReader reader)
        {
            this.Address = Convert.ToString(reader["Address"]);
            this.City = Convert.ToString(reader["City"]);
            this.Pin = Convert.ToString(reader["Pin"]);
        }

        #endregion
    }
}
