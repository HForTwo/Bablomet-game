using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Events/new bool double event")]
public class BoolDoubleEvent : ScriptableObject
{
    private Predicate<double> _decisionMaker;
    private readonly List<Action<double>> _voidListeners = new List<Action<double>>();

    public bool Raise(double value)
    {
        for (int i = _voidListeners.Count - 1; i >= 0; i--)
            _voidListeners[i]?.Invoke(value);

        if (_decisionMaker != null)
            return _decisionMaker.Invoke(value);

        return false;
    }

    public void RegisterDecisionMaker(Predicate<double> listener)
    {
        if (_decisionMaker != null && _decisionMaker != listener)
            Debug.LogWarning($"Decision Maker уже занят! Перезаписываем на новый.");

        _decisionMaker = listener;
    }

    public void UnregisterDecisionMaker(Predicate<double> listener)
    {
        if (_decisionMaker == listener) _decisionMaker = null;
    }

    public void RegisterListener(Action<double> listener) => _voidListeners.Add(listener);
    public void UnregisterListener(Action<double> listener) => _voidListeners.Remove(listener);
}