using System;
using UnityEngine;

public abstract class Upgrader : ScriptableObject
{
    [Header("JSON Configuration")]
    [Tooltip("Имя JSON файла в папке StreamingAssets (без .json)")]
    [SerializeField] private string _jsonFileName;
    public string JsonFileName => _jsonFileName;

    [Header("UI")]
    [SerializeField] protected string[] _levelNames;
    [SerializeField] protected string[] _levelDescriptions;
    [SerializeField] private Sprite _upgraderIcons;
    public Sprite UpgraderIcons => _upgraderIcons;

    [Header("Settings")]
    [SerializeField] protected double[] _prices;
    [SerializeField] private UpgradersStorage _upgradesStorage;
    [SerializeField] private UpgradeType upgradeType;

    public enum UpgradeType
    {
        ClickPower = 0,
        GoldBill = 1,
        PassiveIncome = 2,
        OfflineIncome = 3,
        AutoClick = 4
    }

    public UpgradeType Type => upgradeType;
    public double GetPrice(int  level) => _prices[level];

    public abstract int GetMaxLevel();

    public string GetName(int level)
    {
        return _levelNames[level];
    }

    public string GetDescription(int level)
    {
        return _levelDescriptions[level];
    }

    public virtual void InitializeFromJSON(JSONUpgradeContainer container)
    {
        int totalLevels = container.upgrades.Count;
        _levelNames = new string[totalLevels];
        _levelDescriptions = new string[totalLevels];
        _prices = new double[totalLevels];

        for (int i = 0; i < totalLevels; i++)
        {
            _levelNames[i] = container.upgrades[i].name;
            _levelDescriptions[i] = container.upgrades[i].description;
            _prices[i] = container.upgrades[i].price;
        }
    }
}
