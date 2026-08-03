using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Events/new gameobject event")]
public class GameObjectEvent : ScriptableObject
{
    private readonly List<Action<GameObject>> _listeners = new List<Action<GameObject>>();

    public void Raise(GameObject value)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
            _listeners[i].Invoke(value);

    }

    public void RegisterListener(Action<GameObject> listener) => _listeners.Add(listener);
    public void UnregisterListener(Action<GameObject> listener) => _listeners.Remove(listener);
}
