using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class MoneySpawner : MonoBehaviour
{
    [SerializeField] private GameObjectEvent _getBanknoteEvent;
    [SerializeField] private UpgraderEvent _upgradeEvent;
    [SerializeField] private Queue<MoneySwipe> _moneyPool = new();
    [SerializeField] private MoneySwipe _billPrefab;
    [SerializeField, Range(0, 30)] private int _startBillSpawnCount;
    [SerializeField] private Sprite _currentMoneySprite;
    [SerializeField] private Image _bundleOfMoney;
    private float _goldSpawnChange;
    private int _goldMultiplier;
    private Sprite _goldBillSprite;
    private MoneySwipe _currentBill;
    private bool _nextGoldBill = false;

    public void ReturnToPool(MoneySwipe bill)
    {
        bill.gameObject.SetActive(false);
        _moneyPool.Enqueue(bill);
        _currentBill = null;

        if (_nextGoldBill)
        {
            bill.MoneyUI.ChangeSprite(_currentMoneySprite);
            _nextGoldBill = false;
        }
    }

    public void GetUpgrader(Upgrader currentUpgrader, int level)
    {
        if (currentUpgrader is ClickPower clickPower)
        {
            _currentMoneySprite = clickPower.GetMoneySprite(level);
            _bundleOfMoney.sprite = _currentMoneySprite;
            foreach (var bill in _moneyPool)
                bill.MoneyUI.ChangeSprite(_currentMoneySprite);

            if (_currentBill != null)
                _currentBill.MoneyUI.ChangeSprite(_currentMoneySprite);
        }
        else if (currentUpgrader is GoldBill goldBillData)
        {
            _goldSpawnChange = goldBillData.GetSpawnChange(level);
            _goldMultiplier = goldBillData.GetMultiplier(level);
            _goldBillSprite = goldBillData.GetBillSprite(level);
        }
    }

    private MoneySwipe GetBill()
    {
        bool spawnGold = Random.value < _goldSpawnChange;
        _currentBill = _moneyPool.Count > 0 ? _moneyPool.Dequeue() : SpawnMoney();

        if (spawnGold)
        {
            _currentBill.MoneyUI.ChangeSprite(_goldBillSprite);
            _nextGoldBill = true;
            _currentBill.ChangeMultiplier(_goldMultiplier);
            _currentBill.SetGoldBill();
        }

        _currentBill.gameObject.SetActive(true);
        _currentBill.transform.SetAsLastSibling();
        return _currentBill;
    }

    private void ReturnBill(GameObject newBill)
    {
        if (newBill.transform.TryGetComponent(out MoneySwipe bill))
        {
            bill.Clear();
            ReturnToPool(bill);
            GetBill();
        }
    }

    private MoneySwipe SpawnMoney()
    {
        MoneySwipe bill = Instantiate(_billPrefab, transform);
        bill.MoneyUI.ChangeSprite(_currentMoneySprite);
        return bill;
    }

    private void OnValidate()
    {
        if (_bundleOfMoney == null)
            _bundleOfMoney = GetComponent<Image>();
    }

    private void Start()
    {
        for (int i = 0; i < _startBillSpawnCount; i++)
            ReturnToPool(SpawnMoney());

        GetBill();
    }

    private void OnEnable()
    {
        _getBanknoteEvent.RegisterListener(ReturnBill);
        _upgradeEvent.RegisterListener(GetUpgrader);
    }

    private void OnDisable()
    {
        _getBanknoteEvent.UnregisterListener(ReturnBill);
        _upgradeEvent.UnregisterListener(GetUpgrader);
    }
}
