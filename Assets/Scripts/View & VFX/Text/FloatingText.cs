using UnityEngine;
using TMPro;
using System;
using DG.Tweening; // Подключаем DOTween

[RequireComponent(typeof(CanvasGroup))]
public class FloatingText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textNode;
    [SerializeField] private CanvasGroup _canvasGroup;

    [Header("Animation Settings")]
    [SerializeField] private float _moveDuration = 1.0f;
    [SerializeField] private float _moveHeight = 100f;
    [SerializeField] private float _fadeOutDelay = 0.3f;

    private Action<FloatingText> _onAnimationComplete;
    private Sequence _animationSequence;

    public void Setup(string value, Action<FloatingText> returnToPoolAction)
    {
        if (_textNode == null) _textNode = GetComponent<TextMeshProUGUI>();
        if (_canvasGroup == null) _canvasGroup = GetComponent<CanvasGroup>();

        _textNode.text = value;
        _onAnimationComplete = returnToPoolAction;

        _canvasGroup.alpha = 1f;

        _animationSequence?.Kill();

        Animate();
    }

    private void Animate()
    {
        _animationSequence = DOTween.Sequence();

        Tween moveTween = transform.DOLocalMoveY(transform.localPosition.y + _moveHeight, _moveDuration)
            .SetEase(Ease.OutCubic);

        Tween fadeTween = _canvasGroup.DOFade(0f, _moveDuration - _fadeOutDelay)
            .SetDelay(_fadeOutDelay)
            .SetEase(Ease.InQuad);

        _animationSequence.Insert(0, moveTween);
        _animationSequence.Insert(0, fadeTween);

        _animationSequence.OnComplete(() =>
        {
            _onAnimationComplete?.Invoke(this);
        });
    }

    private void OnDestroy()
    {
        _animationSequence?.Kill();
    }
}