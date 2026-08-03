using System.Collections;
using UnityEngine;

public class EarningsPerSecond : MonoBehaviour
{
    [SerializeField] private UpgraderEvent _upgradeEvent;
    [SerializeField] private DoubleEvent _balanceUpdeter;
    private double _currentMoneyPerSecond;
    private const float _frequencyOfEarning = 5f;
    private WaitForSeconds _secondsWait = new(_frequencyOfEarning);
    private Coroutine _incomeCoroutine;

    public void GetUpgrade(Upgrader newUpgreder, int level)
    {
        if (newUpgreder is PassiveIncome passiveIncome)
        {
            _currentMoneyPerSecond = passiveIncome.GetMoneyPerSecond(level);
            _incomeCoroutine ??= StartCoroutine(GetMoneyPerSecond());
        }
    }

    private IEnumerator GetMoneyPerSecond()
    {
        while (true)
        {
            yield return _secondsWait;
            _balanceUpdeter.Raise(_currentMoneyPerSecond * _frequencyOfEarning);
        }
    }

    private void OnEnable()
    {
        _upgradeEvent.RegisterListener(GetUpgrade);
    }

    private void OnDisable()
    {
        _upgradeEvent.UnregisterListener(GetUpgrade);

        if (_incomeCoroutine != null)
        {
            StopCoroutine(_incomeCoroutine);
            _incomeCoroutine = null;
        }
    } 
}
