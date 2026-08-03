using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Events/new double event")]
public class DoubleEvent : ScriptableObject
{
    private readonly List<Action<double>> _listeners = new List<Action<double>>();

    public void Raise(double value)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
            _listeners[i].Invoke(value);

    }

    public void RegisterListener(Action<double> listener) => _listeners.Add(listener);
    public void UnregisterListener(Action<double> listener) => _listeners.Remove(listener);
}
