using UnityEngine;

[CreateAssetMenu(menuName = "Data/Upgraders/Click power lvl")]
public class ClickPower : Upgrader
{
    [SerializeField] private Sprite[] _levelSprites;

    [Header("Динамические данные из JSON")]
    [SerializeField] private double[] _moneyMultipliers;
    [SerializeField] private int _maxLevel;

    public override void InitializeFromJSON(JSONUpgradeContainer container)
    {
        base.InitializeFromJSON(container);

        _maxLevel = container.upgrades.Count;
        _moneyMultipliers = new double[_maxLevel];
        for (int i = 0; i < _maxLevel; i++)
            _moneyMultipliers[i] = container.upgrades[i].moneyMultiplier;
    }

    public double GetMoneyMultiplier(int level) => _moneyMultipliers[level];
    public Sprite GetMoneySprite(int level) => _levelSprites[level];
    public override int GetMaxLevel() => _maxLevel;
}
