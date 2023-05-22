using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Truck : Vehicle
    {
        protected Truck(string i_LicenseNumber) : base(i_LicenseNumber)
        {
        }
    }
}
