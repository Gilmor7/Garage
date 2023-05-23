using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected readonly Dictionary<string, string> r_Requirements = null;

        private readonly string r_LicenseNumber;
        private string m_ModelName;
        protected readonly List<Wheel> r_Wheels;
        protected VehicleEnergySource m_EnergySource;
        private float m_CurrentEnergyPercentage;

        protected Vehicle(string i_LicenseNumber)
        {
            r_LicenseNumber = i_LicenseNumber;
            r_Wheels = new List<Wheel>();
            r_Requirements = new Dictionary<string, string>() { };
        }

        public List<Wheel> Wheels
        {
            get
            {
                return r_Wheels;
            }
        }
        
        public string ModelName
        {
            get
            {
                return m_ModelName;
            }
        }

        public string LicenseNumber
        {
            get
            {
                return r_LicenseNumber;
            }
        }

        public VehicleEnergySource EnergySource
        {
            get
            {
                return m_EnergySource;
            }
        }

        public float CurrentEnergyPercentage
        {
            get
            {
                return m_CurrentEnergyPercentage;
            }
        }

        public Dictionary<string, string> Requirments
        {
            get
            {
                return r_Requirements;
            }
        }

        public void InflateWheelsToMax()
        {
            foreach(Wheel wheel in r_Wheels)
            {
                wheel.InflateToMax();
            }
        }

        private void setCurrentEnergyPercentage()
        {
            m_CurrentEnergyPercentage = (m_EnergySource.CurrentAmount / m_EnergySource.MaxCapacity) * 100f;
        }

        public void FillEnergySource(float i_AmountToAdd)
        {
            if(i_AmountToAdd < 0)
            {
                throw new ArgumentException("The amount to add must be positive");
            }
            m_EnergySource.Fill(i_AmountToAdd);
            setCurrentEnergyPercentage();
        }

        public virtual void SetMyRequirements()
        {
            r_Requirements.Add("modelName", "Model name:");
            Dictionary<string, string> wheelsRequirements = r_Wheels[0].GetRequirements();
            Dictionary<string, string> energySourceRequirements = m_EnergySource.GetRequirements();

            Utils.MergeTwoStringsDictionaries(r_Requirements, wheelsRequirements);
            Utils.MergeTwoStringsDictionaries(r_Requirements, energySourceRequirements);
        }

        public virtual void SetValuesFromRequirements(Dictionary<string, string> i_Requirements)
        {
            foreach(Wheel wheel in r_Wheels)
            {
                wheel.SetValuesFromRequirements(i_Requirements);
            }

            m_EnergySource.SetValuesFromRequirements(i_Requirements);
            setCurrentEnergyPercentage();

            m_ModelName = i_Requirements["modelName"];
        }
        
        protected void InitializeWheels(int i_NumOfWheels, float i_MaxWheelAirPressure)
        {
            for(int i = 0; i < i_NumOfWheels; i++)
            {
                r_Wheels.Add(new Wheel(i_MaxWheelAirPressure));
            }
        }

        public override string ToString()
        {
            string wheelsData = r_Wheels[0].ToString();
            string vehicleInfo = string.Format(
                @"License Number: {0}
Model name: {1}
Number of wheels: {2}
{3}
{4}
Percentage of energy left: {5}%",
                r_LicenseNumber,
                m_ModelName,
                r_Wheels.Count,
                wheelsData,
                m_EnergySource.ToString(),
                m_CurrentEnergyPercentage);

            return vehicleInfo;
        }
    }
}
