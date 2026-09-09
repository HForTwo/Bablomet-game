using UnityEngine;
using System.Collections;
using YG;

public class SaveData : MonoBehaviour
{
    [SerializeField] private GameEvent _saveGame;
    [SerializeField] private float _saveIntervalSeconds = 60f;
    private Coroutine _saveCoroutine;

    private void Save()
    {
        _saveGame.Raise();
        YG2.SaveProgress();
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            Save();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
            Save();
    }

    private IEnumerator AutoSaveRoutine()
    {
        WaitForSeconds wait = new(_saveIntervalSeconds);

        while (true)
        {
            yield return wait;
            Save();
        }
    }

    private void Start()
    {
        _saveCoroutine = StartCoroutine(AutoSaveRoutine());
    }

    private void OnDestroy()
    {
        if (_saveCoroutine != null)
            StopCoroutine(_saveCoroutine);
    }
}

namespace YG
{
    public partial class SavesYG
    {
        public bool IsFirstSession = true;
        public double Balance = 0;
        public int[] UpgradesLevels = new int[4];
        public string lastExitTime = "";
    }
}
