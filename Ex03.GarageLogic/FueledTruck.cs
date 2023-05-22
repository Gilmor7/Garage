using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FueledTruck : Truck
    {
        private const float k_FuelTankCapacity = 135f;
        private const Fuel.eFuelType k_FuelType = Fuel.eFuelType.Soler;
        
        public FueledTruck(string i_LicensePlate) : base(i_LicensePlate)
        {
            m_EnergySource = new Fuel(k_FuelType, k_FuelTankCapacity);
        }

        public override string ToString()
        {
            string info = string.Format(
                @"Vehicle type: Truck
{0}",
                base.ToString());

            return info;
        }
    }
}
