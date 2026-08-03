using UnityEngine;
using System.Collections;
using YG;

public class SaveData : MonoBehaviour
{
    [SerializeField] private GameEvent _saveGame;
    [SerializeField] private float _saveIntervalSeconds = 600f;
    private Coroutine _saveCoroutine;

    private void OnApplicationQuit()
    {
        _saveGame.Raise();
        YG2.SaveProgress();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            _saveGame.Raise();
            YG2.SaveProgress();
        }
    }

    private IEnumerator AutoSaveRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_saveIntervalSeconds);
            _saveGame.Raise();
            YG2.SaveProgress();
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
