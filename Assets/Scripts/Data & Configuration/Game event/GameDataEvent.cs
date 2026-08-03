using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Events/new game data event")]
public class GameDataEvent : ScriptableObject
{
    private readonly List<Action<GameData>> _listeners = new List<Action<GameData>>();

    public void Raise(GameData value)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
            _listeners[i].Invoke(value);
    }

    public void RegisterListener(Action<GameData> listener) => _listeners.Add(listener);
    public void UnregisterListener(Action<GameData> listener) => _listeners.Remove(listener);
}
