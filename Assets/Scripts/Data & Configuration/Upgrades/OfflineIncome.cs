using UnityEngine;

[CreateAssetMenu(menuName = "Data/Upgraders/Offline income")]
public class OfflineIncome : Upgrader
{
    [SerializeField] private int[] _minutesCollects;
    [SerializeField] private int _maxLevel;

    public override void InitializeFromJSON(JSONUpgradeContainer container)
    {
        base.InitializeFromJSON(container);

        _maxLevel = container.upgrades.Count;
        _minutesCollects = new int[_maxLevel];
        for (int i = 0; i < _maxLevel; i++)
            _minutesCollects[i] = container.upgrades[i].offlineMinutes;
    }

    public override int GetMaxLevel() => _maxLevel;

    public int GetMinutes(int level) => _minutesCollects[level];
}
