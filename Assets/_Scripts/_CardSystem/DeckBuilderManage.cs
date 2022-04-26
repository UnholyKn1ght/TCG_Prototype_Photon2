using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodPeaksStudios
{
    public class DeckBuilderManage : MonoBehaviour
    {
        public static DeckBuilderManage Instance;


        [SerializeField] GameObject cardCollectionContent;
        [SerializeField] GameObject playerDeckContent;

        
       
        public int maxDeckLimit = 40;

        SaveManager saveManager;

        public int selectedCard;

        public void Start()
        {
            saveManager = SaveManager.Instance;
            PlayerDataManager.Instance.playerDeck = saveManager.LoadDeck();
            Instance = this;
        }

        public void DisplayCards()
        {
            foreach (Transform card in cardCollectionContent.transform)
            {
                Destroy(card.gameObject);
            }

            foreach (Transform deck in playerDeckContent.transform)
            {
                Destroy(deck.gameObject);
            }

            
            if (PlayerDataManager.Instance.playerCardColleciton.Count > 0)
            {
                for (int i = 0; i < PlayerDataManager.Instance.playerCardColleciton.Count; i++)
                {
                    GameObject newCard = Instantiate(Resources.Load("CardObjectBuilder"), cardCollectionContent.transform.position, Quaternion.identity) as GameObject;
                    newCard.transform.SetParent(cardCollectionContent.transform);
                    newCard.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    SetCardData setCardInfo = newCard.GetComponent<SetCardData>();
                    CardData copyCardInfo = Object.Instantiate(PlayerDataManager.Instance.playerCardColleciton[i]) as CardData;
                    setCardInfo.cardData = copyCardInfo;
                    setCardInfo.placeNumber = i;
                    setCardInfo.cardData.inDeck = false;
                    
                    
                    
                    

                }
            }

            if (PlayerDataManager.Instance.playerDeck.Count > 0)
            {
                for (int i = 0; i < PlayerDataManager.Instance.playerDeck.Count; i++)
                {
                    GameObject newCard = Instantiate(Resources.Load("CardObjectBuilder"), playerDeckContent.transform.position, Quaternion.identity) as GameObject;
                    newCard.transform.SetParent(playerDeckContent.transform);
                    newCard.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    SetCardData setCardInfo = newCard.GetComponent<SetCardData>();
                    CardData copyCardInfo = Object.Instantiate(PlayerDataManager.Instance.playerDeck[i]) as CardData;
                    
                    setCardInfo.cardData = copyCardInfo;
                    setCardInfo.placeNumber = i;
                    setCardInfo.cardData.inDeck = true;

                }
            }



        }

        //Goes On Add Card Button
        public void AddCardToDeck()
        {
            CardData cardInfo = PlayerDataManager.Instance.currentSelectedCard;


            if (cardInfo.inDeck == false && PlayerDataManager.Instance.playerDeck.Count < 40 && PlayerDataManager.Instance.currentSelectedCard != null)
            {
                PlayerDataManager.Instance.playerDeck.Add(cardInfo);
                PlayerDataManager.Instance.playerCardColleciton.Remove(PlayerDataManager.Instance.playerCardColleciton[selectedCard]); //Undo When More Cards Are Added & Data Network Transfer works.
                selectedCard = 0;
                PlayerDataManager.Instance.currentSelectedCard = null;
                
                DisplayCards();
            }
            else
            {
                Debug.Log("FULL DECK");
            }
        }

        //Goes On Remove Card Button
        public void AddCardToCollection() //Removes Card From Deck
        {

            CardData cardInfo = PlayerDataManager.Instance.currentSelectedCard;

            if (cardInfo.inDeck == true && PlayerDataManager.Instance.currentSelectedCard != null)
            {
                PlayerDataManager.Instance.playerCardColleciton.Add(PlayerDataManager.Instance.playerDeck[selectedCard]);
                PlayerDataManager.Instance.playerDeck.Remove(PlayerDataManager.Instance.playerDeck[selectedCard]);
                selectedCard = 0;
                PlayerDataManager.Instance.currentSelectedCard = null;
                
                DisplayCards();
                
            }
        }

      



        public void ClearWindows()
        {
            foreach (Transform cardCollection in cardCollectionContent.transform)
            {
                Destroy(cardCollection.gameObject);
            }

            foreach (Transform card in playerDeckContent.transform)
            {
                Destroy(card.gameObject);
            }

        }
    }
}
