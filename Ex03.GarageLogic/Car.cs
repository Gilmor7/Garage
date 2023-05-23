using System;
using System.Collections.Generic;
using System.Text;

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
        
        private const float k_MaxWheelAirPressureInCar = 33f;
        private const int k_NumOfWheelsOnCar = 5;
        private eColor m_Color;
        private eNumOfDoors m_NumOfDoors;

        protected Car(string i_LicensePlate) : base(i_LicensePlate)
        {
            InitializeWheels(k_NumOfWheelsOnCar, k_MaxWheelAirPressureInCar);
        }
        
        public override void SetMyRequirements()
        {
            string colors = Utils.constructEnumMessageString<eColor>();
            string numOfDoors = Utils.constructEnumMessageString<eNumOfDoors>();

            base.SetMyRequirements();
            r_Requirements.Add("color", $"Color ({colors}):");
            r_Requirements.Add("numOfDoors", $"Number of doors ({numOfDoors}):");
        }

        public override void SetValuesFromRequirements(Dictionary<string, string> i_Requirements)
        {
            base.SetValuesFromRequirements(i_Requirements);
            string color = i_Requirements["color"];
            string numOfDoors = i_Requirements["numOfDoors"];
            
            if (!Enum.TryParse(color, out eColor parsedColor))
            {
                throw new FormatException("Invalid color");
            }
            
            if (!Enum.TryParse(numOfDoors, out eNumOfDoors parsedNumOfDoors))
            {
                throw new FormatException("Invalid number of doors");
            }
            
            if(!Enum.IsDefined(typeof(eColor), parsedColor))
            {
                throw new FormatException("Invalid color");
            }
            
            if(!Enum.IsDefined(typeof(eNumOfDoors), parsedNumOfDoors))
            {
                throw new FormatException("Invalid number of doors");
            }
            
            m_Color = parsedColor;
            m_NumOfDoors = parsedNumOfDoors;
        }

        public override string ToString()
        {
            string carInfo = string.Format(
                @"{0}
Car color: {1}
Number of doors: {2}",
                base.ToString(),
                m_Color.ToString(),
                m_NumOfDoors.ToString());

            return carInfo;
        }
    }
}
