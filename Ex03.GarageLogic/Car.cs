using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Car: Vehicle
    {
        public enum eColor
        {
            White,
            Black,
            Yellow,
            Red
        }
        
        public enum eNumOfDoors
        {
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5
        }
        
        private const float k_MaxWheelAirPressureInCar = 33;
        private const int k_NumOfWheelsOnCar = 5;
        private eColor m_Color;
        private eNumOfDoors m_NumOfDoors;

        public Car(string i_LicensePlate, eColor i_Color, eNumOfDoors i_NumOfDoors) : base(i_LicensePlate)
        {
            m_NumOfWheels = k_NumOfWheelsOnCar;
            m_MaxWheelAirPressure = k_MaxWheelAirPressureInCar;
            initializeWheels();
            m_Color = i_Color;
            m_NumOfDoors = i_NumOfDoors;
        }
        
        public override void SetMyRequirements()
        {
            base.SetMyRequirements();
            m_Requirements.Add("color", "Color (White, Black, Yellow, Red)");
            m_Requirements.Add("numOfDoors", "Number of doors (2, 3, 4, 5)");
        }
        
        public override void SetValuesFromRequirements(Dictionary<string, string> i_Requirements)
        {
            base.SetValuesFromRequirements(i_Requirements);
            string color = i_Requirements["color"];
            string numOfDoors = i_Requirements["numOfDoors"];
            
            if (!Enum.TryParse(color, out eColor parsedColor))
            {
                throw new FormatException("Invalid color, must be one of: White, Black, Yellow, Red");
            }
            
            if (!Enum.IsDefined(typeof(eColor), parsedColor))
            {
                throw new ArgumentException("Invalid color, must be one of: White, Black, Yellow, Red");
            }
            
            if (!Enum.TryParse(numOfDoors, out eNumOfDoors parsedNumOfDoors))
            {
                throw new FormatException("Invalid number of doors, must be one of: 2, 3, 4, 5");
            }
            
            if (!Enum.IsDefined(typeof(eNumOfDoors), parsedNumOfDoors))
            {
                throw new ArgumentException("Invalid number of doors, must be one of: 2, 3, 4, 5");
            }
            
            m_Color = parsedColor;
            m_NumOfDoors = parsedNumOfDoors;
        }
    }
}
