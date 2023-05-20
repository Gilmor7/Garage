using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Fuel : VehicleEnergySource
    {
        public enum eFuelType
        {
            Octan95,
            Octan96,
            Octan98,
            Soler,
        }

        private readonly eFuelType r_FuelType;

        public Fuel(eFuelType i_FuelType, float i_MaxCapacity)
            : base(i_MaxCapacity)
        {
            r_FuelType = i_FuelType;
        }

        public eFuelType FuelType
        {
            get
            {
                return r_FuelType;
            }
        }

        protected override float GetMaxAmountToFill()
        {
            return MaxCapacity - CurrentAmount;
        }

        protected override void SetMyRequirements()
        {
            m_Requirements["currentAmount"] = "Current fuel level in liters";
            m_Requirements.Add("fuelType", "Fuel Type");
        }

        public override string ToString()
        {
            string fuelTankInfo = string.Format(
                @"Fuel Type: {0}
Current amount of fuel: {1}
Max amount of fuel: {2}",
                r_FuelType.ToString(),
                CurrentAmount,
                MaxCapacity);

            return fuelTankInfo;
        }
    }
}
