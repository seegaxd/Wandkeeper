using System.Collections.Generic;
using UnityEngine;

public class ArtefactsManager : MonoBehaviour
{
    public float damageMulty;
    public PlayerStats PS;
    public List<Artefact> equipedArtefacts = new List<Artefact>();
    public static ArtefactsManager Instance { get; private set; }
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
        }
    }
    
}
