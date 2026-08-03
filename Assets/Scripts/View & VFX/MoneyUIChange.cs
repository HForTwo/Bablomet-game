using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class MoneyUIChange : MonoBehaviour
{
    [SerializeField] private Image _image;

    public void ChangeSprite(Sprite sprite)
    {
        _image.sprite = sprite;
    }

    private void OnValidate()
    {
        if (_image == null)
            _image = GetComponent<Image>();
    }
}
