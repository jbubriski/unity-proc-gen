using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Extensions
{
    #region Arrays

    public static int RandomElement(this int[] array)
    {
        return array[Random.Range(0, array.Length)];
    }

    public static string RandomElement(this string[] array)
    {
        return array[Random.Range(0, array.Length)];
    }

    public static T RandomElement<T>(this T[] array)
    {
        return array[Random.Range(0, array.Length)];
    }

    public static void Shuffle(this int[] deck)
    {
        for (int i = 0; i < deck.Length; i++)
        {
            var temp = deck[i];
            var randomIndex = Random.Range(0, deck.Length);

            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    #endregion

    #region Vectors

    public static Vector3 RandomXZ(int scale)
    {
        return new Vector3(Random.value, 0, Random.value) * scale;
    }

    public static Vector3 RandomXYZ(int scale)
    {
        return new Vector3(Random.value, Random.value, Random.value) * scale;
    }

    public static Vector3 RandomSpherePoint(int radius)
    {
         return Random.insideUnitSphere * radius;
    }
    
    #endregion
}
