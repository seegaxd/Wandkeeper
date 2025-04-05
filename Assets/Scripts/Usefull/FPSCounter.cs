using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    public Text fpsText; // UI Text для отображения FPS

    private int frameCount = 0;
    private float elapsedTime = 0f;
    private float updateInterval = 0.5f; // Интервал обновления (0.5 секунды)

    void Start()
    {
        QualitySettings.vSyncCount = 0; // Отключаем вертикальную синхронизацию
        Application.targetFrameRate = -1; // Убираем лимит FPS
        Debug.Log("убрано");
    }
    void Update()
    {
        frameCount++;
        elapsedTime += Time.unscaledDeltaTime;

        if (elapsedTime >= updateInterval)
        {
            float fps = frameCount / elapsedTime;
            fpsText.text = $"FPS: {fps:F1}";
            frameCount = 0;
            elapsedTime = 0f;
        }
    }
}
