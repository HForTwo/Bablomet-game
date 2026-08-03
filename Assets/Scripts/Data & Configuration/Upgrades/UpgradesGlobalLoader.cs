using System.Collections.Generic;
using UnityEngine;

public class UpgradesGlobalLoader : MonoBehaviour
{
    public static UpgradesGlobalLoader Instance { get; private set; }

    [Header("Сюда скидывай ВСЕ свои ScriptableObjects апгрейдов")]
    [SerializeField] private List<Upgrader> _allUpgraders;

    public void InitializeAllUpgraders(Dictionary<string, string> downloadedJsons)
    {
        Dictionary<string, string> cleanJsons = new Dictionary<string, string>();

        foreach (var kvp in downloadedJsons)
        {
            string cleanKey = kvp.Key.Replace(".json", "", System.StringComparison.OrdinalIgnoreCase).Trim().ToLower();
            cleanJsons[cleanKey] = kvp.Value;
        }

        foreach (Upgrader upgrader in _allUpgraders)
        {
            if (upgrader == null || string.IsNullOrEmpty(upgrader.JsonFileName))
            {
                Debug.LogWarning($"Пропущен ScriptableObject в списке или не указано имя JSON файла!");
                continue;
            }

            string searchKey = upgrader.JsonFileName.Trim().ToLower();

            if (cleanJsons.ContainsKey(searchKey))
            {
                string jsonText = cleanJsons[searchKey];

                JSONUpgradeContainer container = JsonUtility.FromJson<JSONUpgradeContainer>(jsonText);
                upgrader.InitializeFromJSON(container);
            }
            else
                Debug.LogError($"[GlobalLoader] Ошибка: Не найдены данные для апгрейдера {upgrader.name}. " +
                               $"Он ищет файл с именем '{searchKey}', но в скачанных файлах такого нет!");
        }
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}