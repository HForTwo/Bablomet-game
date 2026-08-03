using UnityEngine;

[CreateAssetMenu(menuName = "Data/Upgraders/Gold bill lvl")]
public class GoldBill : Upgrader
{
    [SerializeField, Range(1f, 25f)] private float[] _spawnChanges;
    [SerializeField] private int[] _multipliers;
    [SerializeField] private int _maxLevel;
    [SerializeField] private Sprite[] _billSprites;

    public override void InitializeFromJSON(JSONUpgradeContainer container)
    {
        base.InitializeFromJSON(container);

        _maxLevel = container.upgrades.Count;
        _spawnChanges = new float[_maxLevel];
        _multipliers = new int[_maxLevel];
        for (int i = 0; i < _maxLevel; i++)
        {
            _spawnChanges[i] = container.upgrades[i].spawnChanges;
            _multipliers[i] = container.upgrades[i].multipliers;
        }
    }

    public float GetSpawnChange(int level) => _spawnChanges[level] / 100f;
    public int GetMultiplier(int level) => _multipliers[level];
    public Sprite GetBillSprite(int level) => _billSprites[level];

    public override int GetMaxLevel() => _maxLevel;
}
