using System;
using System.Collections.Generic;
using System.Text;

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
            string licenseTypes = constructLicenseTypeString();
            m_Requirements.Add("licenseType", $"License type ({licenseTypes}):");
            m_Requirements.Add("engineVolume", "Engine volume in cc. (must be a positive number):");
        }

        private string constructLicenseTypeString()
        {
            StringBuilder licenseTypeString = new StringBuilder();
            int numOfLicenseTypes = Enum.GetNames(typeof(eLicenseType)).Length;
            int i = 0;

            foreach (eLicenseType licenseType in Enum.GetValues(typeof(eLicenseType)))
            {
                licenseTypeString.Append(licenseType.ToString());
                if (i < numOfLicenseTypes - 1)
                {
                    licenseTypeString.Append(", ");
                }

                i++;
            }

            return licenseTypeString.ToString();
        }

        public override void SetValuesFromRequirements(Dictionary<string, string> i_Requirements)
        {
            base.SetValuesFromRequirements(i_Requirements);
            string licenseType = i_Requirements["licenseType"];
            string engineVolume = i_Requirements["engineVolume"];

            if (!Enum.TryParse(licenseType, out eLicenseType parsedLicenseType))
            {
                throw new FormatException("Invalid license type");
            }

            if (!Enum.IsDefined(typeof(eLicenseType), parsedLicenseType))
            {
                throw new ArgumentException("Invalid license type");
            }

            if (!int.TryParse(engineVolume, out int parsedEngineVolume))
            {
                throw new FormatException("Invalid engine volume");
            }

            if (parsedEngineVolume < 0)
            {
                throw new ArgumentException("Invalid engine volume");
            }

            m_LicenseType = parsedLicenseType;
            m_EngineVolume = parsedEngineVolume;
        }

        public override string ToString()
        {
            string motorcycleInfo = string.Format(
                @"{0}
License type: {1}
Engine capacity in cc.: {2}",
                base.ToString(),
                m_LicenseType,
                m_EngineVolume);

            return motorcycleInfo;
        }
    }
}
