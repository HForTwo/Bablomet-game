using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Events/new double double event")]
public class DoubleDoubleEvent : ScriptableObject
{
    private readonly List<Action<double, double>> _listeners = new List<Action<double, double>>();

    public void Raise(double value, double secondValue)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
            _listeners[i].Invoke(value, secondValue);
    }

    public void RegisterListener(Action<double, double> listener) => _listeners.Add(listener);
    public void UnregisterListener(Action<double, double> listener) => _listeners.Remove(listener);
}
