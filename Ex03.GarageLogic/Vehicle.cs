using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private readonly string r_LicenseNumber;
        private string m_ModelName;
        protected readonly List<Wheel> r_Wheels;
        protected VehicleEnergySource m_EnergySource;
        private float m_CurrentEnergyPercentage;

        protected Vehicle(string i_LicenseNumber)
        {
            r_LicenseNumber = i_LicenseNumber;
            r_Wheels = new List<Wheel>();
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

        public float CurrentEnergyPercentage
        {
            get
            {
                return m_CurrentEnergyPercentage;
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
    }
}
