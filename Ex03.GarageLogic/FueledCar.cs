using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class FueledCar : Car
    {
        private const float k_FuelTankCapacity = 46f;
        private const Fuel.eFuelType k_FuelType = Fuel.eFuelType.Octan95;

        public FueledCar(string i_LicenseNumber, eColor i_Color, eNumOfDoors i_NumOfDoors) : base(i_LicenseNumber, i_Color, i_NumOfDoors)
        {
            m_EnergySource = new Fuel(k_FuelType, k_FuelTankCapacity);
        }
    }
}
