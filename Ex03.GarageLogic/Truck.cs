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
            m_Requirements.Add("cargoVolume", "Cargo volume (must be a positive number)");
        }
        
        public override void SetValuesFromRequirements(Dictionary<string, string> i_Requirements)
        {
            base.SetValuesFromRequirements(i_Requirements);
            string isCarryingToxicMaterials = i_Requirements["isCarryingToxicMaterials"];
            string cargoVolume = i_Requirements["cargoVolume"];
            
            if (!bool.TryParse(isCarryingToxicMaterials, out bool parsedIsCarryingToxicMaterials))
            {
                throw new FormatException("Invalid input, is carrying toxic materials must be true or false");
            }
            
            if (!float.TryParse(cargoVolume, out float parsedCargoVolume))
            {
                throw new FormatException("Invalid input, cargo volume must be a number");
            }
            
            if (parsedCargoVolume < 0)
            {
                throw new ArgumentException("Invalid input, cargo volume must be positive");
            }
            
            m_IsCarryingToxicMaterials = parsedIsCarryingToxicMaterials;
            m_CargoVolume = parsedCargoVolume;
        }
    }
}
