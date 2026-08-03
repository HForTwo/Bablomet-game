using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Events/new int event")]
public class IntEvent : ScriptableObject
{
    private readonly List<Action<int>> _listeners = new();

    public void Raise(int value)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
            _listeners[i].Invoke(value);

    }

    public void RegisterListener(Action<int> listener) => _listeners.Add(listener);
    public void UnregisterListener(Action<int> listener) => _listeners.Remove(listener);
}
