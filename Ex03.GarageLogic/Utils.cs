using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Utils
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
    }
}
