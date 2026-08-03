using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(OpenCloseWindow))]
public class OflineEarningsWindow : MonoBehaviour
{
    [SerializeField] private DoubleDoubleEvent _onOfflineIncome;
    [SerializeField] private DoubleEvent _getCash;
    [SerializeField] private OpenCloseWindow _openCloseWindow;
    [SerializeField] private TextMeshProUGUI _totalMoneyEarned;
    [SerializeField] private TextMeshProUGUI _totalMoneyButtonText;
    [SerializeField] private TextMeshProUGUI _doubleMoneyButtonText;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private Button _doubleMoneyButton;
    private double _totalMoney;

    private string _correctMoney(double value) => NumberFormatter.Format(value);

    public void ShowTheWindow(double money, double seconds)
    {
        _totalMoney = money;

        _doubleMoneyButton.interactable = true;
        _totalMoneyEarned.text = _correctMoney(money);
        _totalMoneyButtonText.text = _correctMoney(money);
        _doubleMoneyButtonText.text = _correctMoney(money * 2);
        _timeText.text = GetCorrectTime((int)seconds);

        _openCloseWindow.OpenWindow();
    }

    public void GetMoney()
    {
        _getCash.Raise(_totalMoney);
        _openCloseWindow.CloseWindow();
    }

    public void GetDoubleMoney()
    {
        Ads.ShowReward("offline_x2", () =>
        {
            _doubleMoneyButton.interactable = false;
            _getCash.Raise(_totalMoney * 2);
            _openCloseWindow.CloseWindow();
        });
    }

    private string GetCorrectTime(int totalSeconds)
    {
        int hours = totalSeconds / 3600;
        int minutes = (totalSeconds % 3600) / 60;
        int seconds = totalSeconds % 60;

        return $"{hours}ч {minutes}м {seconds}с";
    }

    private void OnValidate()
    {
        if (_openCloseWindow == null)
            _openCloseWindow = GetComponent<OpenCloseWindow>();
    }

    private void OnEnable()
    {
        _onOfflineIncome.RegisterListener(ShowTheWindow);
    }

    private void OnDisable()
    {
        _onOfflineIncome.UnregisterListener(ShowTheWindow);
    }
}
