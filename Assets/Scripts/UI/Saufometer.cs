using System;
using Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Saufometer : MonoBehaviour
    {
        [SerializeField] private RectTransform gague;
        [SerializeField] private ScoreHandler scoreHandler;
        [SerializeField] private float minScoreAngle;
        [SerializeField] private float maxScoreAngle;
        private void Start()
        {
            scoreHandler.onScoreChanged.AddListener(OnScoreChanged);
            OnScoreChanged(scoreHandler.score);
        }

        private void OnScoreChanged(int score)
        {
            var x = new Vector3(0, 0,
                ((float)score).Map(scoreHandler.scoreLowerLimit, scoreHandler.scoreUpperLimit, minScoreAngle,
                    maxScoreAngle));
            if (x.z > 0 && x.z > minScoreAngle)
            {
                x.z = minScoreAngle;
            }

            if (x.z < 0 && x.z < maxScoreAngle)
            {
                x.z = maxScoreAngle;
            }

            gague.eulerAngles = x;
        }
    }
}
