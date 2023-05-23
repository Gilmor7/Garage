using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public sealed class Utils
    {
        public static void MergeTwoStringsDictionaries(Dictionary<string, string> i_Target, Dictionary<string, string> i_Source)
        {
            foreach(KeyValuePair<string, string> item in i_Source)
            {
                if (i_Target.ContainsKey(item.Key))
                {
                    i_Target[item.Key] = item.Value;
                }
                else
                {
                    i_Target.Add(item.Key, item.Value);
                }
            }
        }
        
        public static string constructEnumMessageString<TEnum>() where TEnum : Enum
        {
            StringBuilder enumMessageString = new StringBuilder();
            int numOfEnumValues = Enum.GetNames(typeof(TEnum)).Length;
            int i = 0;
            
            foreach (TEnum enumValue in Enum.GetValues(typeof(TEnum)))
            {
                enumMessageString.Append(enumValue.ToString());
                if (i < numOfEnumValues - 1)
                {
                    enumMessageString.Append(", ");
                }
                
                i++;
            }
            
            return enumMessageString.ToString();
        }
    }
}
