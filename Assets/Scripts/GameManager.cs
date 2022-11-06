using System;
using System.Collections;
using System.Linq;
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
    public MusicManager MusicManager;

    [SerializeField] private GameTick ticker;
    [SerializeField] private ScoreHandler scoreHandler;
    [SerializeField] private ProjectilesHandler projectilesHandler;
    [SerializeField] private PlanetHitHandler planetHitHandler;
    private bool _stop;

    public FMODUnity.EventReference ValidProjectileSoundEventName;
    public FMODUnity.EventReference InvalidProjectileSoundEventName;

    private float _lastMusicIntensity;

    private void Awake()
    {
        ticker.Init();
        scoreHandler.Init();
        projectilesHandler.Init();
        planetHitHandler.Init();
    }

    public void Start()
    {
        scoreHandler.onScoreChanged.AddListener(ScoreChangedHandler);
        planetHitHandler.OnProjectileEntered.AddListener(PlanetProjectileEntered);

    }

    private void PlanetProjectileEntered(ProjectileEnteredEventArgs args)
    {
        if (args.Planet.Needs.Count <= 0)
        {
            return;
        }

        if (args.Planet.Needs.All(x => x.needKind != args.Projectile.shotKind))
        {
            MusicManager.PlayEvent(InvalidProjectileSoundEventName);
        }
        else
        {
            MusicManager.PlayEvent(ValidProjectileSoundEventName);
        }
    }

    private void ScoreChangedHandler(int newScore)
    {
        _lastMusicIntensity = Math.Min(_lastMusicIntensity + 0.02f, 1f);
        MusicManager.SetIntensity(_lastMusicIntensity);
    }

    public void OnDestroy()
    {

    }

    [ButtonMethod]
    public void StartGame()
    {
        StartCoroutine(TrackTimeRoutine());
        MusicManager.SetMusicTheme(MusicTheme.Default);
        MusicManager.SetIntensity(0);
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
