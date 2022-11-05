using System;
using System.Collections.Generic;
using Planets.Needs;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Difficulty
{
    [CreateAssetMenu(fileName = "DifficultySO", menuName = "DifficultySO", order = 0)]
    public class Difficulty : ScriptableObject
    {
        public float currentDifficulty;

        public AnimationCurve planetSpawnInterval;

        public AnimationCurve planetNeedCountCurve;

        private bool bigtiddiegothgf;


        public int CalcPlanetSpawnInterval()
        {
            return Mathf.RoundToInt(planetSpawnInterval.Evaluate(currentDifficulty));
        }

        public int CalcPlanetNeedsCount()
        {
            return Mathf.RoundToInt(Random.Range(1,planetNeedCountCurve.Evaluate(currentDifficulty)));
        }
    }
}
