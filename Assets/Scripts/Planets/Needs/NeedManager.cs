using System.Collections.Generic;
using Extensions;
using UnityEngine;

namespace Planets.Needs
{
    [CreateAssetMenu(fileName = "NeedManager", menuName = "Needs/NeedManager", order = 1)]
    public class NeedManager : ScriptableObject
    {
        [SerializeField] private List<Need> needs;

        public List<Need> GetRandomNeeds(int count)
        {
            var list = new List<Need>();
            for (int i = 0; i < count; i++)
            {
                list.Add(needs.GetRandom());
            }

            return list;
        }
    }
}
