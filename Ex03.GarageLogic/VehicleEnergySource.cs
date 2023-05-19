using System;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Ex03.GarageLogic
{
    public abstract class VehicleEnergySource
    {
        private const float k_MinAmount = 0;
        private readonly float r_MaxCapacity;
        private float m_CurrentAmount;

        protected VehicleEnergySource(float i_MaxCapacity)
        {
            r_MaxCapacity = i_MaxCapacity;
        }

        public float CurrentAmount
        {
            get
            {
                return m_CurrentAmount;
            }
            set
            {
                bool isValidAmount = value <= r_MaxCapacity;

                if(isValidAmount)
                {
                    m_CurrentAmount = value;
                }
                else
                {
                    string actionName = "Set";
                    string message = buildOutOfRangeMessage(actionName, k_MinAmount, MaxCapacity);

                    throw new ValueOutOfRangeException(k_MinAmount, MaxCapacity, message);
                }
            }
        } 
        
        public float MaxCapacity
        {
            get
            {
                return r_MaxCapacity;
            }
        }

        public void Fill(float i_CurrentAmount)
        {
            float maxCapacity = GetMaxAmountToFill();
            bool isValidAmount = i_CurrentAmount <= maxCapacity && i_CurrentAmount > k_MinAmount;

            if(isValidAmount)
            {
                m_CurrentAmount = i_CurrentAmount;
            }
            else
            {
                string actionName = "Fill";
                string message = buildOutOfRangeMessage(actionName, k_MinAmount, maxCapacity);

                throw new ValueOutOfRangeException(k_MinAmount, maxCapacity, message);
            }
        }

        private string buildOutOfRangeMessage(string i_ActionName, float i_MinAmount, float i_MaxAmount)
        {
            return string.Format(
                @"The Amount you are trying to {0} is out of range.
The allowed amount is between {1} to {2}",
                i_ActionName,
                i_MinAmount,
                i_MaxAmount);
        }

        protected abstract float GetMaxAmountToFill();

        public new abstract string ToString();
    }
}
