using System.Collections;
using System.Collections.Generic;
using FMOD;
using FMOD.Studio;
using UnityEngine;


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

    public void OnDestroy()
    {
        _musicInstance.release();
        _musicInstance.stop(STOP_MODE.IMMEDIATE);
        _musicInstance.clearHandle();
    }

    public void Update()
    {
        if (_currentDynamicEvents.Count > 0)
        {
            for (var i = _currentDynamicEvents.Count - 1; i >= 0; i--)
            {
                var res = _currentDynamicEvents[i].getPlaybackState(out var playbackState);
                if (res == RESULT.OK && playbackState != PLAYBACK_STATE.STOPPED)
                    continue;
                _currentDynamicEvents[i].clearHandle();
                _currentDynamicEvents.Remove(_currentDynamicEvents[i]);
            }
        }
    }

    public void SetMusicIntensity(float intensity)
    {
        _musicInstance.setParameterByName("Intensity", intensity);
    }
    
    public void PlayEvent(FMODUnity.EventReference eventReference)
    {
        var eventInstance = FMODUnity.RuntimeManager.CreateInstance(eventReference);
        eventInstance.start();
        _currentDynamicEvents.Add(eventInstance);
    }
}
