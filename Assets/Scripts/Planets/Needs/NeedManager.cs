using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Planets.Needs
{
    [CreateAssetMenu(fileName = "NeedManager", menuName = "Needs/NeedManager", order = 1)]
    public class NeedManager : ScriptableObject
    {
        [SerializeField] private List<Need> needs;
        
        public void GetRandomNeeds(int count, ref List<Need> list)
        {
           list =  needs.OrderBy(arg => Random.Range(0f,1f)).Take(count).ToList();
        }
    }
}
