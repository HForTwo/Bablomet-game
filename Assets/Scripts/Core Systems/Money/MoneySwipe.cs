using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(MoneyAnimation), typeof(MoneyUIChange))]
public class MoneySwipe : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private MoneyAnimation _moneyAnimation;
    [SerializeField] private GameObjectEvent _getBanknoteEvent;
    [SerializeField] private IntEvent _getMultiplierEvent;
    [SerializeField] private float _swipeThreshold = 400f;
    [SerializeField] private MoneyUIChange _moneyUI;
    private Vector2 _startPosition;
    private bool _isSwaped = false;
    private float _offsetY;
    private int _multiplier = 1;
    private bool _isGold = false;

    public MoneyUIChange MoneyUI => _moneyUI;

    public void SetGoldBill() => _isGold = true;

    public void OnPointerDown(PointerEventData eventData)
    {
        _offsetY = transform.position.y - eventData.position.y;
        _startPosition = transform.position;
        _isSwaped = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_isSwaped == false)
            return;

        float targetY = eventData.position.y + _offsetY;
        transform.position = new Vector2(transform.position.x, targetY);
        float deltaY = transform.position.y - _startPosition.y;

        if (deltaY > _swipeThreshold)
        {
            _isSwaped = false;
            FireEvent();
            if (!_isGold)
                AudioManager.Instance.PlayMoneyDraw();
            else
            {
                AudioManager.Instance.PlayGoldMoneyDraw();
                _isGold = false;
                _multiplier = 1;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_isSwaped)
        {
            Clear();
            _isSwaped = false;
        }
    }

    public void Clear()
    {
        transform.localPosition = new Vector2(0, 480);
    }

    public void ChangeMultiplier(int multiplier)
    {
        if (multiplier >= 0)
            _multiplier = multiplier;
    }

    private void FireEvent()
    {
        _getMultiplierEvent.Raise(_multiplier);
        _getBanknoteEvent.Raise(gameObject);
    }



    private void OnValidate()
    {
        if (_moneyAnimation == null)
            _moneyAnimation = GetComponent<MoneyAnimation>();
        if (_moneyUI == null)
            _moneyUI = GetComponent<MoneyUIChange>();
    }
}
