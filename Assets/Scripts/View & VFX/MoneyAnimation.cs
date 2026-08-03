using DG.Tweening;
using UnityEngine.UI;
using UnityEngine;

public class MoneyAnimation : MonoBehaviour
{
    [SerializeField] private float _animDuration = 0.5f;
    [SerializeField] private float _flyDistance = 200f;
    private RectTransform _rectTransform;
    private Image _image;
    private Vector3 _originalScale;

    public void PlaySuccessAnimation(System.Action onComplete)
    {
        _rectTransform.DOKill();
        _image.DOKill();

        Sequence sequence = DOTween.Sequence();

        float randomX = Random.Range(-80f, 80f);
        sequence.Append(_rectTransform.DOAnchorMax(new Vector2(0.5f, 1.5f), _animDuration).SetEase(Ease.OutQuad));

        sequence.Join(_rectTransform.DOJumpAnchorPos(_rectTransform.anchoredPosition + new Vector2(randomX, _flyDistance), 30f, 1, _animDuration));

        sequence.Join(_rectTransform.DOScaleX(-1f, _animDuration / 4).SetLoops(4, LoopType.Yoyo));

        sequence.Join(_image.DOFade(0f, _animDuration));

        sequence.OnComplete(() => onComplete?.Invoke());
    }

    public void PlayReturnAnimation(Vector2 targetPos)
    {
        Debug.Log("g");
        _rectTransform.DOKill();
        _rectTransform.DOMove(targetPos, 0.25f).SetEase(Ease.OutBack);
    }

    public void ResetToPool()
    {
        _rectTransform.DOKill();
        _image.DOKill();

        _rectTransform.localScale = _originalScale;
        _image.color = Color.white;
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
        _originalScale = _rectTransform.localScale;
    }
}
