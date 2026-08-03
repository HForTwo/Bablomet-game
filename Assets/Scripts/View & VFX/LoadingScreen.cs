using UnityEngine;
using DG.Tweening;

public class LoadingScreen : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private RectTransform _loadingIcon;

    [Header("Animation Settings")]
    [SerializeField] private float _rotateDuration = 1.0f;
    [SerializeField] private float _scaleMultiplier = 1.3f;
    [SerializeField] private float _scaleDuration = 0.3f;
    [SerializeField] private float _delayBetweenCycles = 0.5f;

    private Sequence _loadingSequence;
    private Vector3 _originalScale;

    public void ShowLoading()
    {
        gameObject.SetActive(true);

        if (_loadingIcon != null)
        {
            _loadingIcon.localScale = _originalScale;
            _loadingIcon.localRotation = Quaternion.identity;
        }

        _loadingSequence?.Restart();
    }

    public void HideLoading()
    {
        gameObject.SetActive(false);
        _loadingSequence?.Pause();
    }

    private void BuildAnimationSequence()
    {
        if (_loadingIcon == null) return;

        _loadingSequence = DOTween.Sequence();
        _loadingSequence.Append(_loadingIcon.DOScale(_originalScale * _scaleMultiplier, _scaleDuration).SetEase(Ease.OutQuad));
        _loadingSequence.Join(_loadingIcon.DOLocalRotate(new Vector3(0, 0, -360f), _rotateDuration, RotateMode.FastBeyond360).SetEase(Ease.InOutQuad));
        _loadingSequence.Append(_loadingIcon.DOScale(_originalScale, _scaleDuration).SetEase(Ease.InQuad));
        _loadingSequence.AppendInterval(_delayBetweenCycles);
        _loadingSequence.SetLoops(-1, LoopType.Restart);
    }

    private void Awake()
    {
        if (_loadingIcon != null)
            _originalScale = _loadingIcon.localScale;
    }

    private void Start()
    {
        BuildAnimationSequence();
    }

    private void OnDestroy()
    {
        _loadingSequence?.Kill();
    }
}