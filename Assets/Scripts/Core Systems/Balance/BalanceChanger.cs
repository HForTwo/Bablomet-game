using System;
using UnityEngine;
using YG;

public class BalanceChanger : MonoBehaviour
{
    [SerializeField] private GameEvent _saveData;
    [SerializeField] private GameDataEvent _getData;
    [SerializeField] private DoubleEvent _getCash;
    [SerializeField] private DoubleEvent _balanceUpdater;
    [SerializeField] private BoolDoubleEvent _buyItem;
    private double _balance = 0;
    public double Balance => _balance;

    public void GetMoney(double amount)
    {
        _balance += Math.Truncate(amount);
        UpdateBalance();
    }

    public bool TryDebitMoney(double amount)
    {
        if (_balance < amount)
            return false;
        else
        {
            _balance -= amount;
            UpdateBalance();
            return true;
        }
    } 

    public void LoadBalance(GameData data)
    {
        _balance = data.Balance;
        UpdateBalance();
    }

    public void SaveBalance()
    {
        YG2.saves.Balance = _balance;
    }

    private void UpdateBalance()
    {
        _balanceUpdater.Raise(_balance);
    }

    private void OnEnable()
    {
        _getCash.RegisterListener(GetMoney);
        _getData.RegisterListener(LoadBalance);
        _buyItem.RegisterDecisionMaker(TryDebitMoney);
        _saveData.RegisterListener(SaveBalance);
    }

    private void OnDisable()
    {
        _getCash.UnregisterListener(GetMoney);
        _getData.UnregisterListener(LoadBalance);
        _buyItem.UnregisterDecisionMaker(TryDebitMoney);
        _saveData.UnregisterListener(SaveBalance);
    }
}
