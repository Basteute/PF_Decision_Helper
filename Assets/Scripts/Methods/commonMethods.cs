using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public static class commonMethods
{
    public static string[] moveArrayStrNext(string[] array)
    {
        // init the final array
        string[] result = new string[array.Length];
        result[0] = array[array.Length - 1];

        // fill the final array
        for (int i = 1; i < array.Length; i++)
        {
            result[i] = array[i - 1];
        }

        return result;
    }

    public static float[] moveArrayFltNext(float[] array)
    {
        // init the final array
        float[] result = new float[array.Length];
        result[0] = array[array.Length - 1];

        // fill the final array
        for (int i = 1; i < array.Length; i++)
        {
            result[i] = array[i - 1];
        }

        return result;
    }

    public static bool[] moveArrayBoolNext(bool[] array)
    {
        // init the final array
        bool[] result = new bool[array.Length];
        result[0] = array[array.Length - 1];

        // fill the final array
        for (int i = 1; i < array.Length; i++)
        {
            result[i] = array[i - 1];
        }

        return result;
    }
}
