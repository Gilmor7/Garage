﻿using System;

namespace Ex03.GarageLogic
{
    public class ElectricMotorcycle : Motorcycle
    {
        private const float k_MaxHoursBatteryTime = 2.6f;

        public ElectricMotorcycle(string i_LicenseNumber) : base(i_LicenseNumber)
        {
            m_EnergySource = new Battery(k_MaxHoursBatteryTime);
        }

        public override string ToString()
        {
            string info = string.Format(
                @"Vehicle type: Electric Motorcycle
{0}",
                base.ToString());

            return info;
        }
    }
}
