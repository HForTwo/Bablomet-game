using UnityEngine;

public class MoneyCounter : MonoBehaviour
{
    [SerializeField] private IntEvent _getMultiplierEvent;
    [SerializeField] private UpgraderEvent _upgradeEvent;
    [SerializeField] private DoubleEvent _getCash;
    [SerializeField, Range(0, 1000)] private double _costOfOneBill = 1;
    private double _billMultiplier = 1;

    public void BanknoteSwipe(int multiplier)
    {
        _getCash.Raise(_costOfOneBill * _billMultiplier * multiplier);
    }

    public void UpgradeBillMultiplier(Upgrader currentUpgrader, int level)
    {
        if (currentUpgrader is ClickPower clickPower)
            _billMultiplier = clickPower.GetMoneyMultiplier(level);
    }

    private void OnEnable()
    {
        _getMultiplierEvent.RegisterListener(BanknoteSwipe);
        _upgradeEvent.RegisterListener(UpgradeBillMultiplier);
    }

    private void OnDisable()
    {
        _getMultiplierEvent.UnregisterListener(BanknoteSwipe);
        _upgradeEvent.UnregisterListener(UpgradeBillMultiplier);
    }
}
