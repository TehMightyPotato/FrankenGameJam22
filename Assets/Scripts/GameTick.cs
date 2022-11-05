using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Ticker", menuName = "Ticker", order = 4)]
public class GameTick : ScriptableObject
{
    public UnityEvent<float> gameTick;

    public void Init()
    {
        gameTick.RemoveAllListeners();
    }
    
    public void Tick(float t)
    {
        gameTick?.Invoke(t);
    }
}
