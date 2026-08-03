using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using YG;

public class LoadData : MonoBehaviour
{
    public static LoadData Instance { get; private set; }

    [SerializeField] private bool _isDebugging;
    [SerializeField] private GameDataEvent _onLoadData;
    [SerializeField] private GameEvent _onEnterTheGame;
    [SerializeField, Range(0, 10)] private int _upgradesCount;
    private WaitForEndOfFrame _waitForEndOfFrame = new();
    private bool _getSdk = false;
    private bool _getJsons = false;

    public void GetJsonData() => _getJsons = true;
    private void GetSdkData() => _getSdk = true;

    private void InitGame()
    {
        if (YG2.saves.UpgradesLevels == null || YG2.saves.UpgradesLevels.Length < _upgradesCount)
            YG2.saves.UpgradesLevels = new int[_upgradesCount];

        if (YG2.saves.IsFirstSession || _isDebugging)
        {
            int[] startUpgradesLevels = new int[_upgradesCount];
            GameData data = new(0, startUpgradesLevels);
            _onLoadData.Raise(data);
            YG2.saves.IsFirstSession = false;
            YG2.SaveProgress();
            _onEnterTheGame.Raise();
        }
        else
        {
            GameData data = new(YG2.saves.Balance, YG2.saves.UpgradesLevels);
            _onLoadData.Raise(data);
            _onEnterTheGame.Raise();
        }
    }

    private IEnumerator Start()
    {
        do
        {
            if (_getSdk)
                if (YG2.isSDKEnabled)
                    InitGame();

            yield return _waitForEndOfFrame;
        }
        while (!YG2.isSDKEnabled || !_getSdk || !_getJsons);
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        YG2.onGetSDKData += GetSdkData;
    }

    private void OnDisable()
    {
        YG2.onGetSDKData -= GetSdkData;
    }
}

public class GameData
{
    public double Balance;
    public List<int> UpgradersLevels = new();

    public GameData(double balance, int[] upgradesLevels)
    {
        Balance = balance;
        for (int i = 0; i < upgradesLevels.Length; i++)
            UpgradersLevels.Add(upgradesLevels[i]);
    }
}