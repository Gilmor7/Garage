using System;

namespace Ex03.GarageLogic
{
    public class ElectricMotorcycle : Motorcycle
    {
        private const float k_MaxHoursBatteryTime = 6.4f;

        public ElectricMotorcycle(string i_LicenseNumber) : base(i_LicenseNumber)
        {
            m_EnergySource = new Battery(k_MaxHoursBatteryTime);
        }
    }
}
