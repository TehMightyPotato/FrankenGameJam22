using System;
using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Score", menuName = "Score", order = 2)]
public class ScoreHandler : ScriptableObject
{
    [Separator("Score Value Changes")]
    public int planetHitCorrectScoreChange;
    public int planetHitWrongScoreChange;
    public int planetFinishedScoreChange;
    public int shotMissedScoreChange;
    [Separator("Score Value Limits")] 
    public int scoreLowerLimit;
    public int scoreUpperLimit;
    [Separator("Events")]
    public UnityEvent<int> onScoreChanged;
    public UnityEvent onPlanetHitCorrect;
    public UnityEvent onPlanetHitWrong;
    public UnityEvent onLowerScoreLimitReached;
    [Separator("Runtime Values")]
    public int score;

    public void Init()
    {
        score = 0;
        onScoreChanged.RemoveAllListeners();
        onScoreChanged.AddListener(x => CheckForLooseCondition());
    }

    public void PlanetHitCorrect()
    {
        score += planetHitCorrectScoreChange;
        onScoreChanged?.Invoke(score);
        onPlanetHitCorrect?.Invoke();
    }
    

    public void PlanetHitWrong()
    {
        score += planetHitWrongScoreChange;
        onScoreChanged?.Invoke(score);
        onPlanetHitWrong?.Invoke();
    }

    public void PlanetFinished()
    {
        score += planetFinishedScoreChange;
        onScoreChanged?.Invoke(score);
    }

    public void ShotMissed()
    {
        score += shotMissedScoreChange;
        onScoreChanged?.Invoke(score);
    }

    private void CheckForLooseCondition()
    {
        if (score <= scoreLowerLimit)
        {
            onLowerScoreLimitReached?.Invoke();
        }
    }
}
