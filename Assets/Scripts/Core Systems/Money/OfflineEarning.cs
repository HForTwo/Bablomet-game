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

    public void CalculateOfflineEarning()
    {
        string lastExitStr = YG2.saves.lastExitTime;

        if (string.IsNullOrEmpty(lastExitStr))
            return;

        DateTime lastExitTime = DateTime.Parse(lastExitStr, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
        DateTime now = DateTime.UtcNow;

        float offlineSeconds = (float)(now - lastExitTime).TotalSeconds;
        float maxSeconds = _minutesCollect * 60f;

        if (offlineSeconds > maxSeconds)
            offlineSeconds = maxSeconds;

        if (offlineSeconds < 29f)
            return;
        double earned = Math.Truncate(offlineSeconds * _earningPerSecond);
        if (earned > 0)
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
        YG2.saves.lastExitTime = DateTime.UtcNow.ToString("o");
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
