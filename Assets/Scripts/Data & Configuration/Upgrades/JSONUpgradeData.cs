using System.Collections.Generic;

[System.Serializable]
public class JSONUpgradeData
{
    public int level;
    public string name;
    public string description;
    public double price;

    public double moneyMultiplier;
    public float spawnChanges;
    public int multipliers;
    public double passiveIncome;
    public int offlineMinutes;
}

[System.Serializable]
public class JSONUpgradeContainer
{
    public List<JSONUpgradeData> upgrades;
}