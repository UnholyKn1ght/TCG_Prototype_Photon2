using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace BloodPeaksStudios
{
    public class PlayerDataManager : MonoBehaviour
    {
        public static PlayerDataManager Instance;

        public string playerNickName;

        public CardData currentSelectedCard;

        public List<CardData> playerDeck = new List<CardData>();

        public List<CardData> playerCardColleciton = new List<CardData>();
        public List<int> amountOfCards = new List<int>();



        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);

            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }


        }


        public void Start()
        {
            if (PlayerPrefs.HasKey("PlayerName"))
            {
                playerNickName = SaveManager.Instance.LoadString("PlayerName");
                PhotonNetwork.NickName = playerNickName;
            }


        }


    }
}
