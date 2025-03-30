using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMechanic : MonoBehaviour, IObserver
{
    public static PlayerMechanic Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start() {
        ObserverManager.Instance.AddListener(ObserverType.StopGameplay, this);
        Debug.Log("сделай редкости в контент менеджере, дальше добавь монеты/ключи/бомбы, точки интереса, в геймменеджере разные локи, в чит меню спавн выбранных предметов");
    }
    public void OnNotify(ObserverType type, float inF, int inI)
    {
        if(inF == 1) isCanActivate = false;
        else isCanActivate = true;
    }
    public Sphere primarySphere;
    public Sphere secondarySphere;
    public Ability qAbility;
    public Ability eAbility;
    public Ability rAbility;
    public Potion zPotion;
    public Potion xPotion;
    public ActiveItem AItem;
    private bool isCanActivate = true;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        transform.position = Vector3.zero;
    }
    void Update()
    {
        if(isCanActivate)
        {
            if(Input.GetKeyDown(KeyCode.Q)) 
        {
            if(qAbility != null)
            qAbility.ActivateAbility();
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(eAbility != null)
            eAbility.ActivateAbility();
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(rAbility != null)
            rAbility.ActivateAbility();
        }
        if(Input.GetKeyDown(KeyCode.Z))
        {
            if(zPotion != null)
            {
                zPotion.ActivatePotion();
                zPotion = null;
            }
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            if(xPotion != null)
            {
                xPotion.ActivatePotion();
                xPotion = null;
            }
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(AItem != null) AItem.ActivateItem();
        }
        if(Input.GetMouseButtonDown(0))
        {
            primarySphere?.ActiveEffect();
        }
        }
    }
    /*public void EquipSphere(Sphere newSphere, bool asPrimary)
    {
        if (asPrimary)
        {
            if (primarySphere != null) Destroy(primarySphere.gameObject);
            primarySphere = Instantiate(newSphere, transform); 
            primarySphere.Initialize(this, true);
        }
        else
        {
            if (secondarySphere != null) Destroy(secondarySphere.gameObject);
            secondarySphere = Instantiate(newSphere, transform);
            secondarySphere.Initialize(this, false);
        }
    }

    public void EquipAbility(Ability newAbility, AbilityType type)
    {
        switch (type)
        {
            case AbilityType.Qbtn :
                if(qAbility == null)
                {
                    qAbility = Instantiate(newAbility, transform);
                }
                break;
            case AbilityType.Ebtn :
                if(eAbility == null)
                {
                    eAbility = Instantiate(newAbility, transform);
                }
                break;
            case AbilityType.Rbtn :
                if(rAbility == null)
                {
                    rAbility = Instantiate(newAbility, transform);
                }
                break;
            default :
                break;
        }
    }*/
}
