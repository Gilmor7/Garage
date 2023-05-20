using System;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private const float k_MinAirPressure = 0;
        private readonly float r_MaxAirPressure;
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;

        public Wheel(float i_MaxAirPressure)
        {
            r_MaxAirPressure = i_MaxAirPressure;
        }

        public float MaxAirPressure
        {
            get
            {
                return r_MaxAirPressure;
            }
        }
        
        public string ManufacturerName
        {
            get
            {
                return m_ManufacturerName;
            }
        }
        
        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }
            set
            {
                setCurrentAirPressure(value, r_MaxAirPressure);
            }
        }

        public void Inflate(float i_AirPressureToAdd)
        {
            float maxAirPressureToAdd = getMaxAirPressureToAdd();
            float airPressureToSet = m_CurrentAirPressure + i_AirPressureToAdd;

            setCurrentAirPressure(airPressureToSet, maxAirPressureToAdd);
        }

        public void InflateToMax()
        {
            m_CurrentAirPressure = r_MaxAirPressure;
        }

        private void setCurrentAirPressure(float i_PressureToSet, float i_MaxPossiblePressure)
        {
            bool isValidAmount = i_PressureToSet <= MaxAirPressure && i_PressureToSet > k_MinAirPressure;

            if (isValidAmount)
            {
                m_CurrentAirPressure = i_PressureToSet;
            }
            else
            {
                string message = string.Format(
                    @"The Amount of air pressure you are trying to inflate is out of range.
The allowed amount is between {0} to {1}",
                    k_MinAirPressure,
                    i_MaxPossiblePressure);

                throw new ValueOutOfRangeException(k_MinAirPressure, i_MaxPossiblePressure, message);
            }
        }

        private float getMaxAirPressureToAdd()
        {
            return r_MaxAirPressure - m_CurrentAirPressure;
        }

        public override string ToString()
        {
            string wheelInfo = string.Format(
                @"Manufacturer name: {0}
Current air pressure: {1}
Max air pressure: {2}",
                m_ManufacturerName,
                m_CurrentAirPressure,
                r_MaxAirPressure);

            return wheelInfo;
        }
    }
}
