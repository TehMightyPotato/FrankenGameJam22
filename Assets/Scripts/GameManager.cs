using System;
using System.Collections;
using Extensions;
using MyBox;
using Shooting;
using UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [ReadOnly] public float currentLevelTime;
    public float maxLevelTime;
    public Difficulty.Difficulty difficulty;

    [SerializeField] private GameTick ticker;
    [SerializeField] private ScoreHandler scoreHandler;
    [SerializeField] private ProjectilesHandler projectilesHandler;
    private bool _stop;

    private void Awake()
    {
        ticker.Init();
        scoreHandler.Init();
        projectilesHandler.Init();
    }

    [ButtonMethod]
    public void StartGame()
    {
        StartCoroutine(TrackTimeRoutine());
    }

    public void WinGame()
    {
        _stop = true;
    }

    public void LooseGame()
    {
        _stop = true;
    }

    private IEnumerator TrackTimeRoutine()
    {
        while (!_stop)
        {
            if (currentLevelTime > maxLevelTime)
            {
                WinGame();
            }
            currentLevelTime += Time.deltaTime;
            var t = currentLevelTime.Map(0, maxLevelTime, 0, 1);
            ticker.Tick(t);
            yield return null;
        }
    }
}
