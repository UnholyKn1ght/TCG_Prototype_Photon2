using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodPeaksStudios {
    public class PlayerHand : MonoBehaviour
    {
        public static PlayerHand Instance;


        
        [SerializeField] int handLimit = 5;
        [SerializeField] int handCurrent = 0;
        public int deckCount;
        bool openHandPanel = false;
        GameObject handContenter;

        
        public void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            handContenter = this.gameObject;
           
        
        }


        public void Start()
        {
            deckCount = PlayerManager.Instance.playerDeck.Count;
        }



        public void FirstTurn()
        {

            for (int i = 0; i < PlayerManager.Instance.playerDeck.Count; i++)
            {
                int randomPick = Random.Range(0, PlayerManager.Instance.playerDeck.Count);
               
                if (handCurrent < handLimit)
                {
                    
                    GameObject newCard = Instantiate(Resources.Load("CardObject"), handContenter.transform.position, Quaternion.identity) as GameObject;
                    newCard.GetComponent<SetCardData>().cardData = PlayerManager.Instance.playerDeck[randomPick];
                    newCard.transform.SetParent(handContenter.transform);
                    newCard.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    newCard.GetComponent<PlayerHandDragHandler>().canvas = GameObject.Find("PlayCanvas").GetComponent<Canvas>();
                    PlayerManager.Instance.playerDeck.Remove(PlayerManager.Instance.playerDeck[randomPick]);
                    
                    handCurrent++;
                    deckCount--;
                 

                }
            }


        }

        public void DrawCard()
        {
            if (PlayerManager.Instance.playerDeck.Count == 0)
            {
                //Loses The Game
              
            }
            if (PlayerManager.Instance.playerDeck.Count != 0)
            {
                if (handCurrent < 5)
                {
                    int randomPick = Random.Range(0, PlayerManager.Instance.playerDeck.Count);
                    GameObject newCard = Instantiate(Resources.Load("CardObject"), handContenter.transform.position, Quaternion.identity) as GameObject;
                    newCard.GetComponent<SetCardData>().cardData = PlayerManager.Instance.playerDeck[randomPick];
                    newCard.transform.SetParent(handContenter.transform);
                    newCard.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    PlayerManager.Instance.playerDeck.RemoveAt(randomPick);
                    handCurrent++;
                    deckCount--;
                }
                else
                {
                    int randomPick = Random.Range(0, PlayerManager.Instance.playerDeck.Count);
                    GameObject newCard = Instantiate(Resources.Load("CardObject"), handContenter.transform.position, Quaternion.identity) as GameObject;
                    newCard.GetComponent<SetCardData>().cardData = PlayerManager.Instance.playerDeck[randomPick];
                    newCard.transform.SetParent(handContenter.transform);
                    newCard.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    PlayerManager.Instance.playerDeck.RemoveAt(randomPick);
                    Destroy(newCard);
                    deckCount--;
                }
            }
        }

        public void Update()
        {
            if (openHandPanel == false)
            {
                this.gameObject.SetActive(false);
            }
            if (openHandPanel == true)
            {
                this.gameObject.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                DrawCard();
            }
        }

        public void OpenHandPanel()
        {

            openHandPanel = !openHandPanel;

            if (openHandPanel == false)
            {
                this.gameObject.SetActive(false);
            }
            if (openHandPanel == true)
            {
                this.gameObject.SetActive(true);
            }
        }

    }
}