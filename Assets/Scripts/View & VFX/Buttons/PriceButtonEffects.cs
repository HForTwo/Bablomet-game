using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

[RequireComponent(typeof(Image))]
public class PriceButtonEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Settings")]
    [SerializeField] private BoolDoubleEvent _buyUpgrateEvent;
    [SerializeField] private Image _image;
    [SerializeField] private BalanceChanger _balanceChanger;
    [SerializeField] private UpgradersStorage _upgradersStorage;
    [SerializeField, Range(0, 3)] private int _upgraderindex;

    [Header("Animation")]
    [SerializeField] private float _enterScale = 1.2f;
    [SerializeField] private float _scaleDuration = 0.2f;
    [SerializeField] private Color _canBuyColor;
    [SerializeField] private Color _cantBuyColor;

    private Vector3 _originalScale;
    private Color _originalColor;
    private Tween _scaleTween;
    private Tween _colorTween;
    private double _upgraderPrice;
    private bool _isHovered;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isHovered = true;
        PlayEffects();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _isHovered = false;
        StopEffects();
    }

    public void UpdateEffects(double price)
    {
        if (_isHovered)
            StopEffects(onComplete: PlayEffects);
    }

    private void PlayEffects()
    {
        UpgradePrice();
        _scaleTween?.Kill();
        _scaleTween = transform.DOScale(_originalScale * _enterScale, _scaleDuration)
            .SetEase(Ease.OutQuad);

        Color targetColor = _balanceChanger.Balance >= _upgraderPrice ? _canBuyColor : _cantBuyColor;

        _colorTween?.Kill();
        _colorTween = _image.DOColor(targetColor, _scaleDuration)
            .SetEase(Ease.OutQuad);

        if (!_isHovered)
            StopEffects();
    }

    private void StopEffects(Action onComplete = null)
    {
        _scaleTween?.Kill();
        _colorTween?.Kill();

        if (onComplete != null)
        {
            Sequence stopSequence = DOTween.Sequence();

            stopSequence.Join(transform.DOScale(_originalScale, _scaleDuration).SetEase(Ease.OutQuad));
            stopSequence.Join(_image.DOColor(_originalColor, _scaleDuration).SetEase(Ease.OutQuad));

            stopSequence.OnComplete(() => onComplete.Invoke());
        }
        else
        {
            _scaleTween = transform.DOScale(_originalScale, _scaleDuration).SetEase(Ease.OutQuad);
            _colorTween = _image.DOColor(_originalColor, _scaleDuration).SetEase(Ease.OutQuad);
        }
    }

    private void UpgradePrice()
    {
        _upgraderPrice = _upgradersStorage.GetNextLevelPrice(_upgraderindex);
    }

    private void OnValidate()
    {
        if (_image == null)
            _image = GetComponent<Image>();
    }

    private void Start()
    {
        _originalScale = transform.localScale;
        _originalColor = _image.color;
    }

    private void OnEnable()
    {
        _buyUpgrateEvent.RegisterListener(UpdateEffects);
    }

    private void OnDisable()
    {
        _buyUpgrateEvent.UnregisterListener(UpdateEffects);
    }

    private void OnDestroy()
    {
        _scaleTween?.Kill();
        _colorTween?.Kill();
    }
}
