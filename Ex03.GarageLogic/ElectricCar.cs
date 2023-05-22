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

        public ElectricCar(string i_LicenseNumber, eColor i_Color, eNumOfDoors i_NumOfDoors) : base(i_LicenseNumber, i_Color, i_NumOfDoors)
        {
            m_EnergySource = new Battery(k_MaxBatteryTime);
        }
    }
}
