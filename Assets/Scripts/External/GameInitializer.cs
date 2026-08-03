using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class GameInitializer : MonoBehaviour
{
    [Header("UI Элементы")]
    [SerializeField] private LoadingScreen _loadingScreen;

    [Header("Список ВСЕХ JSON файлов в StreamingAssets (включая .json)")]
    [SerializeField] private List<string> _jsonFilesToDownload;

    private IEnumerator InitializeGameDataRoutine()
    {
        if (_loadingScreen != null)
            _loadingScreen.ShowLoading();

        Dictionary<string, string> downloadedJsons = new();

        foreach (string fileName in _jsonFilesToDownload)
        {
            string path = Path.Combine(Application.streamingAssetsPath, fileName);
            string jsonContent = string.Empty;

            if (path.Contains("://") || path.StartsWith("http"))
            {
                using (UnityWebRequest webRequest = UnityWebRequest.Get(path))
                {
                    yield return webRequest.SendWebRequest();

                    if (webRequest.result == UnityWebRequest.Result.Success)
                        jsonContent = webRequest.downloadHandler.text;
                    else
                    {
                        Debug.LogError($"Ошибка загрузки {fileName}: {webRequest.error}");
                        yield break;
                    }
                }
            }
            else
            {
                if (File.Exists(path))
                    jsonContent = File.ReadAllText(path);
                else
                    Debug.LogError($"Файл не найден локально: {path}");
            }

            if (!string.IsNullOrEmpty(jsonContent))
                downloadedJsons[fileName] = jsonContent;
        }

        if (UpgradesGlobalLoader.Instance != null)
            UpgradesGlobalLoader.Instance.InitializeAllUpgraders(downloadedJsons);
        else
            Debug.LogError("UpgradesGlobalLoader не найден на сцене!");

        if (LoadData.Instance != null)
            LoadData.Instance.GetJsonData();
        else
            Debug.LogError("[GameInitializer] LoadData синглтон не найден на сцене!");

        if (_loadingScreen != null)
            _loadingScreen.HideLoading();

    }

    private void Start()
    {
        StartCoroutine(InitializeGameDataRoutine());
    }
}