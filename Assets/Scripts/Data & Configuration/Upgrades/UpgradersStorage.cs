using UnityEngine;
using YG;

[CreateAssetMenu(menuName = "Data/Upgraders/new upgrades storage")]
public class UpgradersStorage : ScriptableObject
{
    [SerializeField] private UpgraderEvent _upgraderEvent;
    [SerializeField] private Upgrader[] _upgraders;
    private int[] _currentLevels = new int[4];

    public Upgrader GetUpgrader(int index) => _upgraders[index];
    public int GetLevel(int index) => _currentLevels[index];
    public double GetNextLevelPrice(int index) => _upgraders[index].GetPrice(_currentLevels[index] + 1);

    public void SetUpgrader(int upgrederIndex)
    {
        _currentLevels[upgrederIndex]++;
        _upgraderEvent.Raise(_upgraders[upgrederIndex], _currentLevels[upgrederIndex]);
        YG2.saves.UpgradesLevels[upgrederIndex] = _currentLevels[upgrederIndex];
    }

    public void LoadUpgrader(int newLevel, int upgrederIndex)
    {
        _currentLevels[upgrederIndex] = newLevel;
        _upgraderEvent.Raise(_upgraders[upgrederIndex], _currentLevels[upgrederIndex]);
    }
}
