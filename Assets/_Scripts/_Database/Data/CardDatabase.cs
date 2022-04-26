using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public static CardDatabase Instance;

    public List<CardData> cardDatabase = new List<CardData>();


    public void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
        SetIndex();
    }

    public void SetIndex()
    {
        for (int i = 0; i < cardDatabase.Count; i++)
        {
            cardDatabase[i].cardDataIndex = i;
        }
    }


}
