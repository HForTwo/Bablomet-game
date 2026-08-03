using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PurchaseOfUpgrade : MonoBehaviour
{
    [SerializeField] private Upgrader.UpgradeType _upgradeType;
    [SerializeField] private UpgraderEvent _buyUpgrade;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _button;

    public void SetUpdate(Upgrader currentUpgrader, int level)
    {
        if (currentUpgrader.Type == _upgradeType)
        {
            if (currentUpgrader.GetMaxLevel() > level + 1)
            {
                _name.text = currentUpgrader.GetName(level + 1);
                _description.text = currentUpgrader.GetDescription(level + 1);
                _price.text = currentUpgrader.GetPrice(level + 1).ToString();
            }
            else
            {
                _price.text = "Макс. уровень";
                _button.interactable = false;
            }
        }
    }

    private void OnEnable()
    {
        _buyUpgrade.RegisterListener(SetUpdate);
    }

    private void OnDisable()
    {
        _buyUpgrade.UnregisterListener(SetUpdate);
    }
}
