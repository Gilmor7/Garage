using System;

namespace Ex03.GarageLogic
{
    public class FueledMotorcycle : Motorcycle
    {
        private const float k_FuelTankCapacity = 6.4f;
        private const Fuel.eFuelType k_FuelType = Fuel.eFuelType.Octan98;

        public FueledMotorcycle(string i_LicenseNumber) : base(i_LicenseNumber)
        {
            m_EnergySource = new Fuel(k_FuelType, k_FuelTankCapacity);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
