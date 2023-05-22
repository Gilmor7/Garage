using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Motorcycle : Vehicle
    {
        public enum eLicenseType
        {
            A1,
            A2,
            AA,
            B1,
        }

        private const float k_MaxWheelAirPressure = 31;
        private const float k_NumOfWheels = 2;
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;

        protected Motorcycle(string i_LicenseNumber) : base(i_LicenseNumber)
        {
            initializeWheels();
        }

        private void initializeWheels()
        {
            for(int i = 0; i < k_NumOfWheels; i++)
            {
                r_Wheels.Add(new Wheel(k_MaxWheelAirPressure));
            }
        }

        public override void SetMyRequirements()
        {
            base.SetMyRequirements();
            m_Requirements.Add("licenseType", "License type");
            m_Requirements.Add("engineVolume", "Engine volume in cc.");
        }

        public override void SetValuesFromRequirmentes(Dictionary<string, string> i_Requirements)
        {
            base.SetValuesFromRequirmentes(i_Requirements);
            string licenseType = i_Requirements["licenseType"];
            string engineVolume = i_Requirements["engineVolume"];
            
            if(!Enum.TryParse(licenseType, out eLicenseType parsedLicenseType))
            {
                throw new FormatException("Invalid license type");
            }
            
            if(!Enum.IsDefined(typeof(eLicenseType), parsedLicenseType))
            {
                throw new ArgumentException("Invalid license type");
            }
            
            if(!int.TryParse(engineVolume, out int parsedEngineVolume))
            {
                throw new FormatException("Invalid engine volume");
            }
            
            if(parsedEngineVolume < 0)
            {
                throw new ArgumentException("Invalid engine volume");
            }

            m_LicenseType = parsedLicenseType;
            m_EngineVolume = parsedEngineVolume;

        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
