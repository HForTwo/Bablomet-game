using UnityEngine;

public class EarningsWithPrestige : MonoBehaviour
{
    [SerializeField] private PrestigeUpgradeData _prestigeData;
    [SerializeField] private BoolDoubleEvent _buyUpgrateEvent;
    private int _prestigeLevel = 0;

    public void BuyPrestige()
    {
        if (_buyUpgrateEvent && _prestigeLevel < _prestigeData.MaxLevel)
            _prestigeLevel++;
    }
}
