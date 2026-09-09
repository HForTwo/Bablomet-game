using System;
using System.Globalization;
using UnityEngine;
using YG;

public class OfflineEarning : MonoBehaviour
{
    [SerializeField] private GameEvent _saveData;
    [SerializeField] private GameEvent _onEnterTheGame;
    [SerializeField] private UpgraderEvent _upgradeEvent;
    [SerializeField] private DoubleDoubleEvent _onOfflineIncome;
    private int _minutesCollect;
    private double _earningPerSecond;
    private bool _offlineRewardCalculated;

    public void CalculateOfflineEarning()
    {
        if (_offlineRewardCalculated)
            return;

        _offlineRewardCalculated = true;

        string lastExitStr = YG2.saves.lastExitTime;

        if (string.IsNullOrEmpty(lastExitStr))
            return;

        if (!DateTime.TryParse(lastExitStr, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out DateTime lastExitTime))
            return;

        DateTime now = DateTime.UtcNow;

        double offlineSeconds = (now - lastExitTime).TotalSeconds;
        if (offlineSeconds <= 0)
            return;

        double maxSeconds = _minutesCollect * 60d;
        if (maxSeconds <= 0)
            return;

        offlineSeconds = Math.Min(offlineSeconds, maxSeconds);
        if (offlineSeconds < 30d)
            return;

        double earned = Math.Truncate(offlineSeconds * _earningPerSecond);
        if (earned <= 0)
            return;

        _onOfflineIncome.Raise(earned, offlineSeconds);
    }

    public void GetUpgrade(Upgrader newUpgreder, int level)
    {
        if (newUpgreder is OfflineIncome offlineIncome)
            _minutesCollect = offlineIncome.GetMinutes(level);
        else if (newUpgreder is PassiveIncome passiveIncome)
            _earningPerSecond = passiveIncome.GetMoneyPerSecond(level);
    }

    public void SaveTime()
    {
        YG2.saves.lastExitTime = DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture);
    }

    private void OnEnable()
    {
        _upgradeEvent.RegisterListener(GetUpgrade);
        _onEnterTheGame.RegisterListener(CalculateOfflineEarning);
        _saveData.RegisterListener(SaveTime);
    }

    private void OnDisable()
    {
        _upgradeEvent.UnregisterListener(GetUpgrade);
        _onEnterTheGame.UnregisterListener(CalculateOfflineEarning);
        _saveData.UnregisterListener(SaveTime);
    }
}
