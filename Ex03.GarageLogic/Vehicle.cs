using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected Dictionary<string, string> m_Requirements = null;

        private readonly string r_LicenseNumber;
        private string m_ModelName;
        protected readonly List<Wheel> r_Wheels;
        protected VehicleEnergySource m_EnergySource;
        private float m_CurrentEnergyPercentage;
        protected float m_MaxWheelAirPressure;
        protected int m_NumOfWheels;

        protected Vehicle(string i_LicenseNumber)
        {
            r_LicenseNumber = i_LicenseNumber;
            r_Wheels = new List<Wheel>();
            m_Requirements = new Dictionary<string, string>() { };
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
                return m_Requirements;
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
            m_EnergySource.Fill(i_AmountToAdd);
            setCurrentEnergyPercentage();
        }

        public virtual void SetMyRequirements()
        {
            m_Requirements.Add("modelName", "Model name:");
            Dictionary<string, string> wheelsRequirements = r_Wheels[0].GetRequirements();
            Dictionary<string, string> energySourceRequirements = m_EnergySource.GetRequirements();

            Utils.MergeTwoStringsDictionaries(m_Requirements, wheelsRequirements);
            Utils.MergeTwoStringsDictionaries(m_Requirements, energySourceRequirements);
        }

        public override string ToString()
        {
            StringBuilder wheelsData = new StringBuilder();
            foreach (Wheel wheel in Wheels)
            {
                wheelsData.AppendLine(wheel.ToString());
            }

            string vehicleInfo = string.Format(
                @"License Number: {0}
Model name: {1}
Percentage of energy left: {2}%
{3}{4}",
                r_LicenseNumber,
                m_ModelName,
                m_CurrentEnergyPercentage,
                wheelsData,
                m_EnergySource.ToString());

            return vehicleInfo;
        }

        public virtual void SetValuesFromRequirements(Dictionary<string, string> i_Requirements)
        {
            foreach(Wheel wheel in r_Wheels)
            {
                wheel.SetValuesFromRequirmentes(i_Requirements);
            }

            m_EnergySource.SetValuesFromRequirmentes(i_Requirements);
            setCurrentEnergyPercentage();

            m_ModelName = i_Requirements["modelName"];
        }
        
        protected void initializeWheels()
        {
            for(int i = 0; i < m_NumOfWheels; i++)
            {
                r_Wheels.Add(new Wheel(m_MaxWheelAirPressure));
            }
        }
    }
}
