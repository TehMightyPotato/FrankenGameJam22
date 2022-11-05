using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Extensions
{
    public static class ListExtensions
    {
        public static T GetRandom<T>(this List<T> list)
        {
            return list[Mathf.FloorToInt(Random.Range(0, list.Count))];
        }
    }
}
