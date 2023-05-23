using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricCar : Car
    {
        private const float k_MaxBatteryTime = 5.2f;

        public ElectricCar(string i_LicenseNumber) : base(i_LicenseNumber)
        {
            m_EnergySource = new Battery(k_MaxBatteryTime);
        }

        public override string ToString()
        {
            string info = string.Format(
                @"Vehicle type: Electric Car
{0}",
                base.ToString());

            return info;
        }
    }
}
