using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PotionRezept
{
    public int id;
    public Sprite potionImage;
    public ElementType[] elementType = new ElementType[3];
    public int finalId;
}

public class PotionManager : MonoBehaviour
{
    public static PotionManager Instance {get; private set;}
    public List<ElementType> grassesIn;
    public List<PotionRezept> avaibleRezepts;
    public bool isCloseToStations;
    void Awake()
    {
        if(Instance == null)
        { 
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        avaibleRezepts = ContentManager.Instance.unlockedRezepts;
    }
    public void AddGrass(ElementType type)
    {
        if(grassesIn.Count >= 3) return;
        grassesIn.Add(type);
        if(grassesIn.Count >= 3) 
        {
            if(isCloseToStations)
            CheckForRezept();
            else CreateStandartPotion();
        }
    }
    
    public void CreateStandartPotion()
    {
        foreach(ElementType type in grassesIn)
        {
            switch(type)
            {
                case ElementType.Fire : // dmg up, 1 grass = 20%
                    
                    break;
                case ElementType.Wind : // speed up, 1 grass = 20%
                    break;
                case ElementType.Earth : // +hp
                    break;
                case ElementType.Water : // +manaRegen
                    break;
                case ElementType.UmElementary : // cdr +20%
                    break;
                default : break;
            }
        }
    }
    public void CheckForRezept()
    {
        // Создаем копию и сортируем введенные травы
        List<ElementType> sortedInput = new List<ElementType>(grassesIn);
        sortedInput.Sort();

        foreach (var rezept in avaibleRezepts)
        {
            List<ElementType> sortedRezept = new List<ElementType>(rezept.elementType);
            sortedRezept.Sort();

            bool match = true;
            for (int i = 0; i < 3; i++)
            {
                if (sortedInput[i] != sortedRezept[i])
                {
                    match = false;
                    break;
                }
            }

            if (match)
            {
                Debug.Log("Найден рецепт: " + rezept.id);
                // Тут можешь выдать игроку зелье с id = rezept.finalId
                // Например:
                // PlayerInventory.Instance.AddPotion(rezept.finalId);

                grassesIn.Clear();
                return;
            }
        }

        Debug.Log("Рецепт не найден");
        grassesIn.Clear(); // Очистим даже если не найден, или по желанию
    }

}
