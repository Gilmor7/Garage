using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class VehicleEnergySource
    {
        protected Dictionary<string, string> m_Requirements = new Dictionary<string, string>()
                                                                         {
                                                                             { "currentAmount", "Current energy amount" },
                                                                         };
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
                //TODO: maybe remove
                setCurrentAmount(value, MaxCapacity);
            }
        } 
        
        public float MaxCapacity
        {
            get
            {
                return r_MaxCapacity;
            }
        }

        public void Fill(float i_AmountToAdd)
        {
            float maxPossibleAmount = GetMaxAmountToFill();
            float amountToSet = CurrentAmount + i_AmountToAdd;


            setCurrentAmount(amountToSet, maxPossibleAmount);
        }

        private void setCurrentAmount(float i_AmountToSet, float i_MaxPossibleAmount)
        {
            bool isValidAmount = i_AmountToSet <= MaxCapacity && i_AmountToSet > k_MinAmount;

            if (isValidAmount)
            {
                m_CurrentAmount = i_AmountToSet;
            }
            else
            {
                string message = string.Format(
                    @"The Amount you are trying to fill is out of range.
The allowed amount is between {0} to {1}",
                    k_MinAmount,
                    i_MaxPossibleAmount);

                throw new ValueOutOfRangeException(k_MinAmount, i_MaxPossibleAmount, message);
            }
        }

        public Dictionary<string, string> GetRequirements()
        {
            SetMyRequirements();

            return m_Requirements;
        }

        protected abstract void SetMyRequirements();

        public virtual void SetValuesFromRequirmentes(Dictionary<string, string> i_Requirements)
        {
            string currentEnergyAmount = i_Requirements["currentAmount"];

            if(!float.TryParse(currentEnergyAmount, out float parsedAmount))
            {
                throw new ArgumentException("Invalid amount");
            }

            m_CurrentAmount = parsedAmount;
        }

        protected abstract float GetMaxAmountToFill();

        public abstract override string ToString();
    }
}
