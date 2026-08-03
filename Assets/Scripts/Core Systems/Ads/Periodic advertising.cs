using System.Collections;
using UnityEngine;

public class Periodicadvertising : MonoBehaviour
{
    private WaitForSeconds _wait = new(420f);
    private Coroutine _adsCoroutine;
    private bool _adReady = false;

    public void OpenShop()
    {
        if (_adReady)
        {
            Ads.ShowReward("periodic", () =>
            {
                _adReady = false;
                _adsCoroutine = StartCoroutine(AdTimer());
            });
        }
    }

    private IEnumerator AdTimer()
    {
        while (true)
        {
            yield return _wait;
            _adReady = true;
            StopCoroutine(_adsCoroutine);
            _adsCoroutine = null;
        }
    }

    private void Start()
    {
        _adsCoroutine = StartCoroutine(AdTimer());
    }
}
