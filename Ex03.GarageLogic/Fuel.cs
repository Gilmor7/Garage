using System;

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

        public override string ToString()
        {
            string fuelTankInfo = string.Format(
                @"Fuel Type: {0}
Current amount of fuel: {1}
Max amount of fuel: {2}",
                FuelTypeToString(r_FuelType),
                CurrentAmount,
                MaxCapacity);

            return fuelTankInfo;
        }

        public static string FuelTypeToString(eFuelType i_FuelType)
        {
            string fuelType = null;

            switch(i_FuelType)
            {
                case eFuelType.Octan95:
                    fuelType = "Octan 95";
                    break;
                case eFuelType.Octan96:
                    fuelType = "Octan 96";
                    break;
                case eFuelType.Octan98:
                    fuelType = "Octan 98";
                    break;
                case eFuelType.Soler:
                    fuelType = "Soler";
                    break;
            }

            return fuelType;
        }
    }
}
