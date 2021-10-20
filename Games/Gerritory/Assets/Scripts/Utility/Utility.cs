using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility 
{
   public static string[] ReadTextFile(string fileName)
   {
        System.IO.StreamReader file = new System.IO.StreamReader(fileName);
        List<string> strs = new List<string>();
        string line;
        while( (line = file.ReadLine())!=null )
        {
            strs.Add(line);
        }
        return strs.ToArray();
    }
}
