using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Events/new vector2 event")]
public class Vector2Event : ScriptableObject
{
    private readonly List<Action<Vector2>> _listeners = new List<Action<Vector2>>();

    public void Raise(Vector2 value)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
            _listeners[i].Invoke(value);
    
    }

    public void RegisterListener(Action<Vector2> listener) => _listeners.Add(listener);
    public void UnregisterListener(Action<Vector2> listener) => _listeners.Remove(listener);
}
