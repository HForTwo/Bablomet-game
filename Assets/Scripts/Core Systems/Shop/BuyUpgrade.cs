using UnityEngine;

public class BuyUpgrade : MonoBehaviour
{
    [SerializeField] private GameDataEvent _loadData;
    [SerializeField] private UpgradersStorage _upgradersStorage;
    [SerializeField] private BoolDoubleEvent _buyUpgrateEvent;

    public void BuyNewUpgrade(int upgraderIndex)
    {
        if (DebitMoney(_upgradersStorage.GetNextLevelPrice(upgraderIndex)))
        {
            _upgradersStorage.SetUpgrader(upgraderIndex);
            AudioManager.Instance.PlayBuyItem();
        }
    }

    public void DownloadCurrentLevelOfUpgrades(GameData data)
    {
        for (int i = 0; i < data.UpgradersLevels.Count; i++)
            _upgradersStorage.LoadUpgrader(data.UpgradersLevels[i], i);
    }

    private bool DebitMoney(double price)
    {
        return _buyUpgrateEvent.Raise(price);
    }

    private void OnEnable()
    {
        _loadData.RegisterListener(DownloadCurrentLevelOfUpgrades);
    }

    private void OnDisable()
    {
        _loadData.UnregisterListener(DownloadCurrentLevelOfUpgrades);
    }
}
