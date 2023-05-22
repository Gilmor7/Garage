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

        private const float k_MaxWheelAirPressureInMotorcycle = 31;
        private const int k_NumOfWheelsOnMotorcycle = 2;
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;

        protected Motorcycle(string i_LicenseNumber) : base(i_LicenseNumber)
        {
            m_NumOfWheels = k_NumOfWheelsOnMotorcycle;
            m_MaxWheelAirPressure = k_MaxWheelAirPressureInMotorcycle;
            initializeWheels();
        }

        public override void SetMyRequirements()
        {
            base.SetMyRequirements();
            m_Requirements.Add("licenseType", "License type");
            m_Requirements.Add("engineVolume", "Engine volume in cc.");
        }

        public override void SetValuesFromRequirements(Dictionary<string, string> i_Requirements)
        {
            base.SetValuesFromRequirements(i_Requirements);
            string licenseType = i_Requirements["licenseType"];
            string engineVolume = i_Requirements["engineVolume"];

            if (!Enum.TryParse(licenseType, out eLicenseType parsedLicenseType))
            {
                throw new FormatException("Invalid license type, must be one of the following: A1, A2, AA, B1");
            }

            if (!Enum.IsDefined(typeof(eLicenseType), parsedLicenseType))
            {
                throw new ArgumentException("Invalid license type, must be one of the following: A1, A2, AA, B1");
            }

            if (!int.TryParse(engineVolume, out int parsedEngineVolume))
            {
                throw new FormatException("Invalid engine volume, must be a number");
            }

            if (parsedEngineVolume < 0)
            {
                throw new ArgumentException("Invalid engine volume, must be a positive number");
            }

            m_LicenseType = parsedLicenseType;
            m_EngineVolume = parsedEngineVolume;

        }
    }
}
