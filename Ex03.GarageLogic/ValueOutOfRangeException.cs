﻿using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private readonly float r_MinValue;
        private readonly float r_MaxValue;

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue, string i_Msg) : base(i_Msg)
        {
            r_MinValue = i_MinValue;
            r_MaxValue = i_MaxValue;
        }

        public float MaxValue
        {
            get
            {
                return r_MaxValue;
            }
        }

        public float MinValue
        {
            get
            {
                return r_MinValue;
            }
        }
    }
}
