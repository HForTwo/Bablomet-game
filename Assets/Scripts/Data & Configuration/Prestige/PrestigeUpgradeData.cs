using UnityEngine;

[CreateAssetMenu(menuName = "Data/Prestige/new prestige")]
public class PrestigeUpgradeData : ScriptableObject
{
    [SerializeField] public string _upgradeName;
    [SerializeField] public int _maxLevel;
    [SerializeField] public double _baseCost;
    [SerializeField] public float _costMultiplier = 1.5f;
    public float _bonusPerLevel = 0.2f;

    public int MaxLevel => _maxLevel;
}
