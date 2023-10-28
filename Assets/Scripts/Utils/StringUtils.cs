using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringUtils
{
   public static Vector3 Vec3FromString(string input)
   {
        if (input.StartsWith("(") && input.EndsWith(")"))
        {
            input = input.Substring(1, input.Length - 2);
        }

        // split the items
        string[] sArray = input.Split(',');

        return new Vector3(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2])); ;
    }
}
