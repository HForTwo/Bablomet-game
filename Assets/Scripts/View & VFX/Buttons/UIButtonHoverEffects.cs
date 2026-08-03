using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class UIButtonHoverEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Scale Settings")]
    [SerializeField] private float _hoveredScale = 1.1f;
    [SerializeField] private float _scaleDuration = 0.2f;

    [Header("Rotation Shake Settings")]
    [SerializeField] private float _shakeStrength = 10f;
    [SerializeField] private float _shakeDuration = 0.4f;

    private Vector3 _originalScale;
    private Tween _scaleTween;
    private Sequence _shakeSequence;
    
    private void Start()
    {
        _originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _scaleTween?.Kill();
        _scaleTween = transform.DOScale(_originalScale * _hoveredScale, _scaleDuration)
            .SetEase(Ease.OutQuad);

        _shakeSequence?.Kill();
        _shakeSequence = DOTween.Sequence();

        _shakeSequence.Append(transform.DOLocalRotate(new Vector3(0, 0, _shakeStrength), _shakeDuration * 0.25f).SetEase(Ease.OutQuad))
            .Append(transform.DOLocalRotate(new Vector3(0, 0, -_shakeStrength), _shakeDuration * 0.5f).SetEase(Ease.InOutQuad))
            .Append(transform.DOLocalRotate(Vector3.zero, _shakeDuration * 0.25f).SetEase(Ease.InQuad))
            .SetLoops(-1, LoopType.Restart);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _scaleTween?.Kill();
        _shakeSequence?.Kill();

        _scaleTween = transform.DOScale(_originalScale, _scaleDuration)
            .SetEase(Ease.OutQuad);

        transform.DOLocalRotate(Vector3.zero, _scaleDuration)
            .SetEase(Ease.OutQuad);
    }

    private void OnDestroy()
    {
        _scaleTween?.Kill();
        _shakeSequence?.Kill();
    }
}