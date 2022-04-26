using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodPeaksStudios {
    public class SaveManager : MonoBehaviour
    {

        public static SaveManager Instance;

        public List<int> playerDeckIndex = new List<int>();
        public List<int> playerCollectionIndex = new List<int>();

        

        List<int> emptyList = new List<int>();

        public void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }


        public void SaveString(string keyName, string saveString)
        {

            PlayerPrefs.SetString(keyName, saveString);

        }

        public string LoadString(string keyName)
        {
            string loadedString = PlayerPrefs.GetString(keyName);
            return loadedString;

        }


        public void SavePlayerDeck()
        {
            List<CardData> playerDeck = new List<CardData>();
            List<CardData> cardDatabase = new List<CardData>();
  
            playerDeckIndex.Clear();
            playerDeck = PlayerDataManager.Instance.playerDeck;
            cardDatabase = CardDatabase.Instance.cardDatabase;

            

            for (int i = 0; i < playerDeck.Count; i++)
            {
                for (int a = 0; a < cardDatabase.Count; a++)
                {
                    if (playerDeck[i].cardName == cardDatabase[a].cardName)
                    {
                        playerDeckIndex.Add(a);
                    }
                }
            }


            
            PlayerPrefsX.SetIntArray("PlayerDeck", playerDeckIndex.ToArray());
           
           


        }

        

        public List<CardData> LoadDeck()
        {

            PlayerDataManager playerDeck;
            playerDeck = PlayerDataManager.Instance;
            playerDeck.playerDeck.Clear();

            List<CardData> loadPlayerDeck = new List<CardData>();

            int[] indexs = PlayerPrefsX.GetIntArray("PlayerDeck");
          


            for (int i = 0; i < indexs.Length; i++)
            {
                loadPlayerDeck.Add(CardDatabase.Instance.cardDatabase[indexs[i]]);

            }

            

            return loadPlayerDeck;
        }


        public void SavePlayerCardCollection()
        {
            List<CardData> playerCollection = PlayerDataManager.Instance.playerCardColleciton;
            List<CardData> cardDatabase = CardDatabase.Instance.cardDatabase;
            List<int> collectionCardAmount = PlayerDataManager.Instance.amountOfCards;

            playerCollectionIndex.Clear();


            for (int i = 0; i < playerCollection.Count; i++)
            {
                for (int a = 0; a < cardDatabase.Count; a++)
                {
                    if (playerCollection[i].cardName == cardDatabase[a].cardName)
                    {
                       playerCollectionIndex.Add(a);
                    }
                }
            }

            PlayerPrefsX.SetIntArray("PlayerCardCollection", playerCollectionIndex.ToArray());
            PlayerPrefsX.SetIntArray("CardCollectionAmount", collectionCardAmount.ToArray());
            
        }

        public int[] LoadPlayerCollectionInt()
        {
            int[] playerDeck = PlayerPrefsX.GetIntArray("PlayerCardCollection");
            return playerDeck;
        }


        public List<CardData> LoadPlayerCardCollection()
        {
            
            PlayerDataManager playerCollection;
            playerCollection = PlayerDataManager.Instance;
            playerCollection.playerCardColleciton.Clear();

            List<CardData> loadPlayerCollection = new List<CardData>();

            int[] indexs = LoadPlayerCollectionInt();


            for (int i = 0; i < indexs.Length; i++)
            {
                loadPlayerCollection.Add(CardDatabase.Instance.cardDatabase[indexs[i]]);

            }

            return loadPlayerCollection;
        }

        public List<int> LoadPlayerCollectionCardAmount()
        {
            List<int> cardAmount = new List<int>();
            int[] cardAmountLoaded = PlayerPrefsX.GetIntArray("CardCollectionAmount");


            for (int i = 0; i < cardAmountLoaded.Length; i++)
            {
                cardAmount.Add(cardAmountLoaded[i]);
            }


            return cardAmount;
        }



       

    }

}
