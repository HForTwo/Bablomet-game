using TMPro;
using UnityEngine;

public class BalanceUI : MonoBehaviour
{
    [SerializeField] private DoubleEvent _moneyEarnedEvent;
    [SerializeField] private UpgraderEvent _upgraderEvent;
    [SerializeField] private TextMeshProUGUI _balanceText;
    [SerializeField] private TextMeshProUGUI _incomeText;

    public void GetUpgrade(Upgrader newUpgreder, int level)
    {
        if (newUpgreder is PassiveIncome passiveIncome)
            _incomeText.text = $"Доход: {NumberFormatter.Format(passiveIncome.GetMoneyPerSecond(level))} C/c";
    }

    private void UpdateBalanceDisplay(double newBalance)
    {
        _balanceText.text = NumberFormatter.Format(newBalance);
    }

    private void OnValidate()
    {
        if (_balanceText == null)
            _balanceText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _moneyEarnedEvent.RegisterListener(UpdateBalanceDisplay);
        _upgraderEvent.RegisterListener(GetUpgrade);
    }

    private void OnDisable()
    {
        _moneyEarnedEvent.UnregisterListener(UpdateBalanceDisplay);
        _upgraderEvent.UnregisterListener(GetUpgrade);
    }
}
