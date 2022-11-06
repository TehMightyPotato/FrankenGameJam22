using System.Collections;
using System.Collections.Generic;
using FMOD;
using FMOD.Studio;
using UnityEngine;

public enum MusicTheme
{
    Default,
    Dubstep,
    Hit,
    Trance
}

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance = null;

    public ScoreHandler ScoreHandler;

    public FMODUnity.EventReference EventName = new();

    private FMOD.Studio.EventInstance _musicInstance;

    private readonly List<FMOD.Studio.EventInstance> _currentDynamicEvents = new();

    public void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        _musicInstance = FMODUnity.RuntimeManager.CreateInstance(EventName);
        _musicInstance.start();
    }
    
    public void Update()
    {
        if (_currentDynamicEvents.Count > 0)
        {
            foreach (var eventInstance in _currentDynamicEvents)
            {
                var res = eventInstance.getPlaybackState(out var playbackState);
                if (res == RESULT.OK && playbackState != PLAYBACK_STATE.STOPPED)
                    continue;

                _currentDynamicEvents.Remove(eventInstance);
                eventInstance.clearHandle();
            }
        }
    }

    public void SetIntensity(float intensity)
    {
        _musicInstance.setParameterByName("Intensity", intensity);
    }

    public void SetMusicTheme(MusicTheme theme)
    {
        _musicInstance.setParameterByNameWithLabel("Theme", theme.ToString());
    }

    public void PlayEvent(FMODUnity.EventReference eventReference)
    {
        var eventInstance = FMODUnity.RuntimeManager.CreateInstance(eventReference);
        eventInstance.start();
        _currentDynamicEvents.Add(eventInstance);
    }
}
