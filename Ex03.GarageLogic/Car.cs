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
            string colors = constructColorString();
            string numOfDoors = constructNumOfDoorsString();
            m_Requirements.Add("color", $"Color ({colors}):");
            m_Requirements.Add("numOfDoors", $"Number of doors ({numOfDoors}):");
        }
        
        private string constructColorString()
        {
            StringBuilder colorString = new StringBuilder();
            int numOfColors = Enum.GetNames(typeof(eColor)).Length;
            int i = 0;
            
            foreach (eColor color in Enum.GetValues(typeof(eColor)))
            {
                colorString.Append(color.ToString());
                if (i < numOfColors - 1)
                {
                    colorString.Append(", ");
                }
                
                i++;
            }
            
            return colorString.ToString();
        }
        
        private string constructNumOfDoorsString()
        {
            StringBuilder numOfDoorsString = new StringBuilder();
            int numOfNumOfDoors = Enum.GetNames(typeof(eNumOfDoors)).Length;
            int i = 0;
            
            foreach (eNumOfDoors numOfDoors in Enum.GetValues(typeof(eNumOfDoors)))
            {
                numOfDoorsString.Append(numOfDoors.ToString());
                if (i < numOfNumOfDoors - 1)
                {
                    numOfDoorsString.Append(", ");
                }
                
                i++;
            }
            
            return numOfDoorsString.ToString();
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
    }
}
