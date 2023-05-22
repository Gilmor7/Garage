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

        public override void SetRequirments(Dictionary<string, string> i_Requirements)
        {
            base.SetRequirments(i_Requirements);
            string licenseType = i_Requirements["licenseType"];
            string engineVolume = i_Requirements["engineVolume"];


        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
