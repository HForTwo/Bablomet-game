using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Events/new upgrader event")]
public class UpgraderEvent : ScriptableObject
{
    private readonly List<Action<Upgrader, int>> _listeners = new List<Action<Upgrader, int>>();

    public void Raise(Upgrader currentUpgrade, int level)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
            _listeners[i].Invoke(currentUpgrade, level);

    }

    public void RegisterListener(Action<Upgrader, int> listener) => _listeners.Add(listener);
    public void UnregisterListener(Action<Upgrader, int> listener) => _listeners.Remove(listener);
}
