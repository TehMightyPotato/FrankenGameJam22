using System;
using System.Collections.Generic;
using Extensions;
using Planets.Needs;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Difficulty
{
    [CreateAssetMenu(fileName = "DifficultySO", menuName = "DifficultySO", order = 0)]
    public class Difficulty : ScriptableObject
    {
        public float time;

        public AnimationCurve planetSpawnInterval;

        public AnimationCurve planetNeedCountCurve;

        public GameTick tick;

        private bool bigtiddiegothgf;
        
        private void Init()
        {
            tick.gameTick.AddListener(Scale);
        }

        private void Scale(float t)
        {
            time = t;
        }

        public int CalcPlanetSpawnInterval()
        {
            return Mathf.RoundToInt(planetSpawnInterval.Evaluate(time));
        }

        public int CalcPlanetNeedsCount()
        {
            return Mathf.RoundToInt(Random.Range(1,planetNeedCountCurve.Evaluate(time)));
        }
    }
}
