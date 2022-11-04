using System;
using UnityEngine;

namespace Planets.Needs
{
    [CreateAssetMenu(fileName = "NewNeed", menuName = "Needs/Need", order = 0)]
    public class Need : ScriptableObject
    {
        public NeedKind needKind;
        public Sprite needSprite;
    }

    public enum NeedKind
    {
        Saufi1,
        Saufi2,
        Saufi3,
        Mucke1,
        Mucke2,
        Mucke3,
        People1,
        People2,
        People3
    }
}
