using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace BloodPeaksStudios {
    public class PlayerManager : MonoBehaviour
    {
        public PhotonView PV;
        public static PlayerManager Instance;

        public List<CardData> playerDeck = new List<CardData>();

        public bool myTurn;

        public float lifePoints;
        public int energyPoints;
        //On Player Field
        public List<int> playerCards = new List<int>();

        public SetCardData attackingCard;
        public SetCardData defendingCard;


        public int playerID; 

        public void Awake()
        {
            PV = GetComponent<PhotonView>();

            lifePoints = 500f;
            
            Instance = this;


            playerDeck = SaveManager.Instance.LoadDeck();
            transform.tag = "Player";

            

            GameManager.Instance.playerObjects.Add(this.gameObject);
        }




        public void TakeDamage(float dmg)
        {
            if (PV.IsMine == true)
                lifePoints -= dmg;
        }
        
    }
}
