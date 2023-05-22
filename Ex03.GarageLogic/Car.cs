using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Car : Vehicle
    {
        protected Car(string i_LicenseNumber) : base(i_LicenseNumber)
        {
        }
    }
}
