using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class FueledCar : Car
    {
        private const float k_FuelTankCapacity = 46f;
        private const Fuel.eFuelType k_FuelType = Fuel.eFuelType.Octan95;

        public FueledCar(string i_LicenseNumber) : base(i_LicenseNumber)
        {
            m_EnergySource = new Fuel(k_FuelType, k_FuelTankCapacity);
        }

        public override string ToString()
        {
            string info = string.Format(
                @"Vehicle type: Fuel Car
{0}",
                base.ToString());

            return info;
        }
    }
}
