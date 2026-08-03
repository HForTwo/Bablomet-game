using UnityEngine;
using System.Collections.Generic;

public class FloatingTextSpawner : MonoBehaviour
{
    [SerializeField] private DoubleEvent _getCash;
    [SerializeField] private FloatingText _textPrefab;
    [SerializeField] private Transform _canvasTransform;
    [SerializeField] private RectTransform _spawnZone;

    [Header("Pool Settings")]
    [SerializeField] private int _initialPoolSize = 10;
    private Queue<FloatingText> _textPool = new Queue<FloatingText>();

    public void SpawnText(double amount)
    {
        if (_textPrefab == null || _canvasTransform == null)
            return;

        FloatingText textInstance = GetFromPool();
        Vector2 targetLocalPosition = GetRandomLocalPointInZone();

        textInstance.transform.localPosition = new Vector3(targetLocalPosition.x, targetLocalPosition.y, 0f);
        textInstance.transform.localScale = Vector3.one;

        textInstance.gameObject.SetActive(true);
        textInstance.Setup($"+{amount} C", ReturnToPool);
    }

    private Vector2 GetRandomLocalPointInZone()
    {
        if (_spawnZone == null)
        {
            Debug.LogWarning("FloatingTextSpawner: Не назначена _spawnZone!");
            return Vector2.zero;
        }

        Rect rect = _spawnZone.rect;

        float randomLocalX = Random.Range(rect.xMin, rect.xMax);
        float randomLocalY = Random.Range(rect.yMin, rect.yMax);
        Vector2 localPointInZone = new Vector2(randomLocalX, randomLocalY);

        Vector3 worldPoint = _spawnZone.TransformPoint(localPointInZone);
        Vector3 localPointInCanvas = _canvasTransform.InverseTransformPoint(worldPoint);

        return localPointInCanvas;
    }

    private FloatingText GetFromPool()
    {
        if (_textPool.Count > 0)
            return _textPool.Dequeue();
        else
            return Instantiate(_textPrefab, _canvasTransform);
    }

    private void ReturnToPool(FloatingText textObject)
    {
        textObject.gameObject.SetActive(false);
        _textPool.Enqueue(textObject);
    }

    private void Start()
    {
        if (_textPrefab == null || _canvasTransform == null)
            return;

        for (int i = 0; i < _initialPoolSize; i++)
        {
            FloatingText obj = Instantiate(_textPrefab, _spawnZone);
            obj.gameObject.SetActive(false);
            _textPool.Enqueue(obj);
        }
    }

    private void OnEnable()
    {
        _getCash.RegisterListener(SpawnText);
    }

    private void OnDisable()
    {
        _getCash.UnregisterListener(SpawnText);
    }
}
