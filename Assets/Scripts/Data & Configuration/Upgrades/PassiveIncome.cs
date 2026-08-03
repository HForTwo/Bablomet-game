using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Upgraders/Passive incove")]
public class PassiveIncome : Upgrader
{
    [SerializeField] private double[] _moneyPerSecond;
    [SerializeField] private int _maxLevel;

    public override void InitializeFromJSON(JSONUpgradeContainer container)
    {
        base.InitializeFromJSON(container);

        _maxLevel = container.upgrades.Count;
        _moneyPerSecond = new double[_maxLevel];
        for (int i = 0; i < _maxLevel; i++)
            _moneyPerSecond[i] = container.upgrades[i].passiveIncome;
    }

    public override int GetMaxLevel() => _maxLevel;
    public double GetMoneyPerSecond(int level) => _moneyPerSecond[level];
}
