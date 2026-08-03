using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Events/new game event")]
public class GameEvent : ScriptableObject
{
    private readonly List<Action> _listeners = new List<Action>();

    public void Raise()
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
            _listeners[i].Invoke();
    }

    public void RegisterListener(Action listener) => _listeners.Add(listener);
    public void UnregisterListener(Action listener) => _listeners.Remove(listener);
}
