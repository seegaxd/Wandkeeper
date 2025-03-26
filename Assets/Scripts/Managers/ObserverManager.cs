using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ObserverType
{
    MoneyLA,
    KeyLA,
    BombLA,
    StopGameplay,
    TakingDamage,
    RecoverHealth
}

public interface IObserver
{
    void OnNotify(ObserverType messageType, float messageFloat = 0, int messageInt = 0);
}

public interface IOallObserver
{
    void OnNotify(ObserverType messageType, float messageFloat = 1);
}

public class ObserverManager : MonoBehaviour
{
    private Dictionary<ObserverType, List<IObserver>> listenersDict = new Dictionary<ObserverType, List<IObserver>>();
    private List<IOallObserver> allListeners = new List<IOallObserver>();

    public static ObserverManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        listenersDict.Clear();
        allListeners.Clear();
    }

    public void NotifyAll(ObserverType eventType, float messageFloat = 0, int messageInt = 0)
    {
        if (listenersDict.TryGetValue(eventType, out var observers))
        {
            foreach (var listener in observers)
            {
                listener.OnNotify(eventType, messageFloat, messageInt);
            }
        }

        foreach (var listener in allListeners)
        {
            listener.OnNotify(eventType, messageFloat);
        }
    }

    public void AddListener(ObserverType eventType, IObserver listener)
    {
        if (!listenersDict.ContainsKey(eventType))
        {
            listenersDict[eventType] = new List<IObserver>();
        }

        if (!listenersDict[eventType].Contains(listener))
        {
            listenersDict[eventType].Add(listener);
        }
    }

    public void AddAllListener(IOallObserver listener)
    {
        if (!allListeners.Contains(listener))
        {
            allListeners.Add(listener);
        }
    }

    public void DeleteAllListener(IOallObserver listener)
    {
        allListeners.Remove(listener);
    }

    public void DeleteListener(ObserverType eventType, IObserver listener)
    {
        if (listenersDict.TryGetValue(eventType, out var observers))
        {
            observers.Remove(listener);
        }
    }
}
