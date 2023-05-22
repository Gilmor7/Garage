using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Truck: Vehicle
    {
        private const int k_NumOfWheelsOnTruck = 14;
        private const float k_MaxWheelAirPressureInTruck = 26;
        private bool m_IsCarryingToxicMaterials;
        private float m_CargoVolume;
        protected Truck(string i_LicenseNumber) : base(i_LicenseNumber)
        {
            m_NumOfWheels = k_NumOfWheelsOnTruck;
            m_MaxWheelAirPressure = k_MaxWheelAirPressureInTruck;
            initializeWheels();
        }
        
        public override void SetMyRequirements()
        {
            base.SetMyRequirements();
            m_Requirements.Add("isCarryingToxicMaterials", "Is carrying toxic materials? (true/false)");
            m_Requirements.Add("cargoVolume", "Cargo volume");
        }
    }
}
