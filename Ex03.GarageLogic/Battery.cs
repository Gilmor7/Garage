using System;

namespace Ex03.GarageLogic
{
    public class Battery : VehicleEnergySource
    {
        public Battery(float i_MaxCapacity) : base(i_MaxCapacity)
        {
        }

        protected override float GetMaxAmountToFill()
        {
            return (MaxCapacity - CurrentAmount) * 60; // Convert maximum time to charge from hours to minutes.
        }

        public override string ToString()
        {
            string batteryInfo = string.Format(
                @"Battery time left in hours: {0}
Max battery time in hours: {1}",
                CurrentAmount,
                MaxCapacity);

            return batteryInfo;
        }
    }
}
