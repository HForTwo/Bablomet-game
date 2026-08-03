using UnityEngine;
using DG.Tweening;

public class OpenCloseWindow : MonoBehaviour
{
    [SerializeField] private float _openPosition;
    [SerializeField] private float _closePosition;
    [SerializeField, Range(0f, 3f)] private float _animationDuration;
    
    public void OpenWindow()
    {
        gameObject.SetActive(true);
        transform
            .DOMoveY(_openPosition, _animationDuration)
            .From(_closePosition)
            .SetEase(Ease.OutBack);
    }

    public void CloseWindow()
    {
        transform
            .DOMoveY(_closePosition, _animationDuration)
            .From(_openPosition)
            .SetEase(Ease.InBack)
            .OnComplete(() => gameObject.SetActive(false));
    }
}
