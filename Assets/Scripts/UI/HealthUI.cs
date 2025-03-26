using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HealthUI : MonoBehaviour, IObserver
{
    [Tooltip("0 - 0/2, 1 - 1/2, 2 - 2/2")]
    public Sprite[] healthSprites; // Хранит спрайты сердечек
    public GameObject heartPrefab; // Префаб сердца
    public Transform placeForHearts;

    private List<Image> heartsPool = new(); // Пул объектов

    void Start()
    {
        ObserverManager.Instance.AddListener(ObserverType.TakingDamage, this);
        ObserverManager.Instance.AddListener(ObserverType.RecoverHealth, this);
        InitializePool();
        UpdateHealthUI();
    }

    private void InitializePool()
    {
        int maxHearts = PlayerStats.Instance.maxHealth / 2;
        for (int i = 0; i < maxHearts; i++)
        {
            GameObject heartObj = Instantiate(heartPrefab, placeForHearts);
            heartObj.SetActive(false);
            heartsPool.Add(heartObj.GetComponent<Image>());
        }
    }

    public void OnNotify(ObserverType type, float inF, int inI)
    {
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        int currentHealth = PlayerStats.Instance.health;
        int maxHearts = PlayerStats.Instance.maxHealth / 2;

        for (int i = 0; i < maxHearts; i++)
        {
            if (i < heartsPool.Count)
            {
                heartsPool[i].gameObject.SetActive(true);

                if (currentHealth > (i * 2) + 1)
                    heartsPool[i].sprite = healthSprites[2]; // Полное сердце
                else if (currentHealth > i * 2)
                    heartsPool[i].sprite = healthSprites[1]; // Половина сердца
                else
                    heartsPool[i].sprite = healthSprites[0]; // Пустое сердце
            }
        }
    }
}
