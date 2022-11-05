using System;
using UnityEngine;

namespace Planets.Needs
{
    [CreateAssetMenu(fileName = "NewNeed", menuName = "Needs/Need", order = 0)]
    public class Need : ScriptableObject
    {
        public NeedKind needKind;
        public Sprite sprite;
    }

    public enum NeedKind
    {
        Drink1,
        Drink2,
        Drink3,
        Music1,
        Music2,
        Music3,
        Person1,
        Person2,
        Person3
    }
}
