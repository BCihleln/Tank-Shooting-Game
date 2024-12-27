using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    /// <summary>
    /// Made by myself.
    /// Generates a Random Vector inside the given range.
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="range">generates x,y,z value form [-range,range]</param>
    /// <returns></returns>
    public static void RandomVector3(ref this Vector3 vector,float range)
    {
        vector.Set(
            Random.Range(-range, range),
            Random.Range(-range, range),
            Random.Range(-range, range));
    }

    /// <summary>
    /// Random Vector3 instructs seperately by given x,y,z range 
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="xRange"></param>
    /// <param name="yRange"></param>
    /// <param name="zRange"></param>
    public static void RandomVector3(ref this Vector3 vector, float xRange,float yRange,float zRange)
    {
        vector.Set(
            Random.Range(-xRange, xRange),
            Random.Range(-yRange, yRange),
            Random.Range(-zRange, zRange));
    }
}
