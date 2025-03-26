using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class CheatMenu : MonoBehaviour
{
    public static CheatMenu Instance { get; private set; }
    public GameObject menu;
    private CheatsUI temp;
    private bool isActive;
    private static List<string> debugLogs = new List<string>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Application.logMessageReceived += LogCallback; // Подписываемся на сбор логов
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (!isActive)
            {
                temp = Instantiate(menu).GetComponent<CheatsUI>();
                isActive = true;
                PrintAllLogs();
            }
            else
            {
                Destroy(temp.gameObject);
                isActive = false;
            }
        }
        else if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("U pressed");
        }
    }

    private void LogCallback(string condition, string stackTrace, LogType type)
    {
        debugLogs.Add($"[{type}] {condition}");
        if(isActive)PrintAllLogs();
    }

    
    
    public void PrintAllLogs()
    {
        if (temp != null && temp.console != null)
        {
            temp.console.text = "=== LOGS ===\n" + string.Join("\n", debugLogs);
        }
        else
        {
            Debug.Log("=== ALL TAKEN LOGS ===");
            foreach (string log in debugLogs)
            {
                Debug.Log(log);
            }
        }
    }

    private void OnDestroy()
    {
        Application.logMessageReceived -= LogCallback; // Отписываемся при удалении объекта
    }
}
