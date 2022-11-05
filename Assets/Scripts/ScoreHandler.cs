using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Score", menuName = "Score", order = 2)]
public class ScoreHandler : ScriptableObject
{
    public int planetHitCorrectScoreChange;
    public int planetHitWrongScoreChange;
    public int planetFinishedScoreChange;
    public int shotMissedScoreChange;
    
    public UnityEvent<int> onScoreChanged;
    public int score;


    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        score = 0;
        onScoreChanged.RemoveAllListeners();
    }
    
    
    public void PlanetHitCorrect()
    {
        score += planetHitCorrectScoreChange;
        onScoreChanged?.Invoke(score);
    }
    

    public void PlanetHitWrong()
    {
        score += planetHitWrongScoreChange;
        onScoreChanged?.Invoke(score);
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
}
