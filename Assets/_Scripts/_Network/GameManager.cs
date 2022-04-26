using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace BloodPeaksStudios {
    /// <summary>
    /// Handles Game States And Any Action Made By The Players
    /// </summary>
    public class GameManager : MonoBehaviour
    {

        //TODO Turnbase System, Attacking Opponent Cards, Drawing New Cards. 

        public PhotonView PV;
        public static GameManager Instance;
        public List<GameObject> playerObjects = new List<GameObject>();

        #region Client Variables

        //Phase Control
        #region Phases
        public bool attacking;
        public bool defending;

        
        [Header("Turns")]
        [Space]
        
        public bool playerTurn;
        public Image playerTurnInc;
        public Image opponentTurnInc;
        public PlayerHand playerHand;


        #endregion

        //Player UI Info
        #region Player UI Variables
        [Header("PlayerUI")]
        //Player UI 
        public Image playerHPBar;
        public TextMeshProUGUI playerHPText;
        public Image playerEnergyBar;
        public TextMeshProUGUI playerEnergyText;
        public TextMeshProUGUI playerDeckCountText;
        public Button endTurnButton;
        public Button skipBlockButton;
        public Button confirmBlockButton;
        public Button confirmAttackButton;
        public Button setAttackPhaseButton;
        [Space(10f)]
        public SetCardData attackingCard;
        public SetCardData defendingCard;

        [Space]
        #endregion

        //Opponent Stats
        #region OpponentStats Varibles
        [Header("OpponentStats")]
        //Opponent Stats
        public float opponentHealth;
        public int opponentEnergy;
        public Image opponentEnergyBar;
        public TextMeshProUGUI opponentEnergyText;
        [Space]
        #endregion
        
        //Opponent UI Info
        #region Opponent UI Variables
        [Header("OpponentUI")]
        public GameObject opponentFieldContent;
        public Image opponentHPBar;
        public TextMeshProUGUI opponentHPText;
        #endregion

        //Field Data
        #region Field Data
        public List<int> opponentCards = new List<int>();
        #endregion

        #endregion





        private void Awake()
        {
            //Checks For Only 1 Instance Of This GameObject
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);

            }
            else
            {
                Instance = this;
                PV = GetComponent<PhotonView>();
            }
            //Set Player Data At Start Of Game.
            for (int i = 0; i < playerObjects.Count; i++)
            {
                playerObjects[i].GetComponent<PlayerManager>().playerID = i;
            }

            attacking = false;
        }

        public void Start()
        { 
            PV.RPC("PickTurn", RpcTarget.MasterClient);
        }


        //Update Methods
        public void Update()
        {
            UpdateHealthUI();
            UpdateEnergyUI();
            UpdatePlayerDeckCount();

            if (opponentEnergy > 10)
            {
                opponentEnergy = 10;
            }

            if (playerObjects[0].GetComponent<PlayerManager>().myTurn == false)
            {
                endTurnButton.interactable = false;
                setAttackPhaseButton.gameObject.SetActive(false);
            }
            else
            {
                endTurnButton.interactable = true;
                setAttackPhaseButton.gameObject.SetActive(true);
                
            }
            
            
        }

        #region Turn Management
        
        public void SendSwitchTurn()
        {
            PV.RPC("SwitchTurns", RpcTarget.Others);

            playerObjects[0].GetComponent<PlayerManager>().myTurn = !playerObjects[0].GetComponent<PlayerManager>().myTurn;
            playerTurn = playerObjects[0].GetComponent<PlayerManager>().myTurn;

            

            if (playerObjects[0].GetComponent<PlayerManager>().myTurn == true)
            {
                playerTurnInc.color = Color.green;
                opponentTurnInc.color = Color.black;

                if (playerObjects[0].GetComponent<PlayerManager>().energyPoints < 10)
                    playerObjects[0].GetComponent<PlayerManager>().energyPoints += 2;

                playerHand.DrawCard();
            }
            if (playerObjects[0].GetComponent<PlayerManager>().myTurn == false)
            {
                playerTurnInc.color = Color.black;
                opponentTurnInc.color = Color.green;
                opponentEnergy += 2;
            }
        }

        [PunRPC]
        public void SwitchTurns()
        {
            playerObjects[0].GetComponent<PlayerManager>().myTurn = !playerObjects[0].GetComponent<PlayerManager>().myTurn;
            playerTurn = playerObjects[0].GetComponent<PlayerManager>().myTurn;


            if (playerObjects[0].GetComponent<PlayerManager>().myTurn == true)
            {
                playerTurnInc.color = Color.green;
                opponentTurnInc.color = Color.black;

                if (playerObjects[0].GetComponent<PlayerManager>().energyPoints < 10)
                    playerObjects[0].GetComponent<PlayerManager>().energyPoints += 2;

                playerHand.DrawCard();
            }
            if (playerObjects[0].GetComponent<PlayerManager>().myTurn == false)
            {
                playerTurnInc.color = Color.black;
                opponentTurnInc.color = Color.green;
                opponentEnergy += 2;
            }
        }


        [PunRPC]
        public void PickTurn()
        {
            int pickNumber = Random.Range(0, 1);

            //MasterClient Goes First
            if (pickNumber == 0)
            {
                playerObjects[0].GetComponent<PlayerManager>().myTurn = true;
                PV.RPC("MasterClientGoesFirst", RpcTarget.Others);
                playerTurnInc.color = Color.green;
                opponentTurnInc.color = Color.black;
            }

            //The Opponent Client Goes First
            if (pickNumber == 1)
            {
                playerObjects[0].GetComponent<PlayerManager>().myTurn = false;
                PV.RPC("OpponentGoesFirst", RpcTarget.Others);
                playerTurnInc.color = Color.black;
                opponentTurnInc.color = Color.green;
            }

            playerObjects[0].GetComponent<PlayerManager>().energyPoints = 2;
            opponentEnergy = 2;
            PlayerHand.Instance.FirstTurn();
        }

        [PunRPC]
        public void MasterClientGoesFirst()
        {
            playerObjects[0].GetComponent<PlayerManager>().myTurn = false;
            playerTurnInc.color = Color.black;
            opponentTurnInc.color = Color.green;
            playerObjects[0].GetComponent<PlayerManager>().energyPoints = 2;
            PlayerHand.Instance.FirstTurn();

        }

        [PunRPC]
        public void OppoentGoesFirst()
        {
            playerObjects[0].GetComponent<PlayerManager>().myTurn = true;
            playerTurnInc.color = Color.green;
            opponentTurnInc.color = Color.black;
            playerObjects[0].GetComponent<PlayerManager>().energyPoints = 2;
            PlayerHand.Instance.FirstTurn();

        }
     



        #endregion


        #region Card Management
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID">What Card To Send</param>
        /// <param name="playerIndex">0_Player/ 1_Opponent</param>
        public void SendCardData(int ID, int playerIndex)
        {
            if (playerIndex == 0)
                PlayPlayerCard(ID);

            if (playerIndex == 1)
                PlayOpponentCard(ID);

        }


        //Player Cards
     
        public void PlayPlayerCard(int ID)
        {
            playerObjects[0].GetComponent<PlayerManager>().playerCards.Add(ID);
            PV.RPC("SendPlayerCard", RpcTarget.Others, ID);

        }

        [PunRPC]
        public void SendPlayerCard(int ID)
        {
            opponentCards.Add(ID);
            UpdatePlayingFields(ID);
        }


        //Opponent Cards
       
        public void PlayOpponentCard(int ID)
        {
            playerObjects[1].GetComponent<PlayerManager>().playerCards.Add(ID);
            opponentCards.Add(ID);
            PV.RPC("SendOpponentHealthUpdate", RpcTarget.Others, ID);
        }

        [PunRPC]
        public void SendOpponentCard(int ID)
        {
            playerObjects[0].GetComponent<PlayerManager>().playerCards.Add(ID);
            UpdatePlayingFields(ID);
        }


        #endregion


        //Send UI Updates To Clients In Game.
        #region UI Info


       
        public void UpdatePlayerDeckCount()
        {
            playerDeckCountText.text = PlayerHand.Instance.deckCount.ToString();
        }



        [PunRPC]
        public void UpdatePlayingFields(int ID)
        {
            GameObject newCard = Instantiate(Resources.Load("CardObject"), opponentFieldContent.transform.position, Quaternion.identity) as GameObject;
            newCard.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            newCard.GetComponent<SetCardData>().cardData = CardDatabase.Instance.cardDatabase[ID];
            newCard.transform.SetParent(opponentFieldContent.transform);
            newCard.GetComponent<PlayerHandDragHandler>().canDrag = false;
            

        }

        [PunRPC]
        public void UpdateFieldsOnDestory()
        {
            //Called When An Card On The Field Is Destoryed
            List<CardData> playerCardData = new List<CardData>();
            List<CardData> opponentCardData = new List<CardData>();

            GameObject playerZone = GameObject.Find("PlayerPlayZone");
            GameObject opponentZone = GameObject.Find("OpponentPlayZone");
         
            //Collects Card Data From Card Is Destroyed
            for (int i = 0; i < playerZone.transform.childCount ; i++)
            {
                playerCardData.Add(playerZone.transform.GetChild(i).GetComponent<SetCardData>().cardData);
            }

            for (int a = 0; a < opponentZone.transform.childCount; a++)
            {
                opponentCardData.Add(opponentZone.transform.GetChild(a).GetComponent<SetCardData>().cardData);
            }

            //Replace Cards On Field
            for (int b = 0; b < playerCardData.Count; b++)
            {
                GameObject newCard = Instantiate(Resources.Load("CardObject"), playerZone.transform.position, Quaternion.identity) as GameObject;
                newCard.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                newCard.GetComponent<SetCardData>().cardData = playerCardData[b];
                newCard.transform.SetParent(playerZone.transform);
                newCard.GetComponent<PlayerHandDragHandler>().canDrag = false;

            }

            for (int c = 0; c < opponentCardData.Count; c++)
            {
                GameObject newCard = Instantiate(Resources.Load("CardObject"), opponentFieldContent.transform.position, Quaternion.identity) as GameObject;
                newCard.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                newCard.GetComponent<SetCardData>().cardData = opponentCardData[c];
                newCard.transform.SetParent(opponentFieldContent.transform);
                newCard.GetComponent<PlayerHandDragHandler>().canDrag = false;
            }

        }


        //Update Field HUDs For Lifepoints
        public void UpdateHealthUI()
        {
            playerHPBar.fillAmount = playerObjects[0].GetComponent<PlayerManager>().lifePoints / 500f;
            playerHPText.text = playerObjects[0].GetComponent<PlayerManager>().lifePoints.ToString();
            opponentHPBar.fillAmount = opponentHealth / 500f;
            opponentHPText.text = opponentHealth.ToString();
        }

        //Update Players Energy Fields
        public void UpdateEnergyUI()
        {
            playerEnergyBar.fillAmount = playerObjects[0].GetComponent<PlayerManager>().energyPoints / 10f;
            playerEnergyText.text = playerObjects[0].GetComponent<PlayerManager>().energyPoints.ToString();
            opponentEnergyBar.fillAmount = opponentEnergy / 10f;
            opponentEnergyText.text = opponentEnergy.ToString();
        }








        #endregion

        //Used For Checking To See If The Game Is Over
        #region Win Conduction
        public void CheckGameStatus()
        {
            PV.RPC("CheckForWin", RpcTarget.All);
        }


        
        [PunRPC]
        public void CheckForWin()
        {
            if (playerObjects[0].GetComponent<PlayerManager>().lifePoints <= 0)
            {
                //If Players Loses
                PhotonNetwork.LeaveRoom();
                PhotonNetwork.LoadLevel(4);
                
            }

            if (opponentHealth <= 0)
            {
                //If Player Wins
                PhotonNetwork.LeaveRoom();
                PhotonNetwork.LoadLevel(3);
            }
        }

        #endregion

        //Methods Used To Send Energy Usage To Each Player
        #region Player Energy Usage

        /// <summary>
        /// Send Energy Data Through To Local And Other Client
        /// </summary>
        /// <param name="amount">How Much Should Be Used</param>
        /// <param name="playerIndex"> 0_Player / 1_Opponent </param>
        [PunRPC]
        public void SendEnergyUsage(int amount, int playerIndex)
        {

            if (playerIndex == 0)
                UsePlayerEnergy(amount);

            if (playerIndex == 1)
                UseOpponentEnergy(amount);

        }

        //Spend Opponent Energy
        public void UseOpponentEnergy(int amount)
        {
            playerObjects[1].GetComponent<PlayerManager>().energyPoints -= amount;
            opponentEnergy -= amount;
            PV.RPC("SendOpponentEnergyUpdate", RpcTarget.Others, amount);
        }

        [PunRPC]
        public void SendOpponentEnergyUpdate(int amount)
        {
            playerObjects[0].GetComponent<PlayerManager>().energyPoints -= amount;
            

        }

        //Direct Damage To Player
        public void UsePlayerEnergy(int amount)
        {
            playerObjects[0].GetComponent<PlayerManager>().energyPoints -= amount;
            PV.RPC("SendPlayerEnergyUpdate", RpcTarget.Others, amount);
        }

        [PunRPC]
        public void SendPlayerEnergyUpdate(int amount)
        {
            opponentEnergy -= amount;
           

        }

        


        #endregion

        //Methods Used To Send Damage To Each Player Or Self.
        #region Player Damage Calucations

        /// <summary>
        /// 
        /// </summary>
        /// <param name="damage">How Much Damage Should Be Dealt</param>
        /// <param name="playerIndex"> 0_Player / 1_Opponent </param>
        [PunRPC]
        public void SendDamage(float damage, int playerIndex)
        {
            if (playerIndex == 1)
                DamageOpponent(damage);

            if (playerIndex == 0)
                DamagePlayer(damage);

            
        }

        //Direct Damage To Opponent
        public void DamageOpponent(float damage)
        {
            playerObjects[1].GetComponent<PlayerManager>().TakeDamage(damage);
            opponentHealth -= damage;
            PV.RPC("SendOpponentHealthUpdate", RpcTarget.Others, damage);
        }

        [PunRPC]
        public void SendOpponentHealthUpdate(float damage)
        {
            playerObjects[0].GetComponent<PlayerManager>().TakeDamage(damage);
            CheckGameStatus();
            
        }

        //Direct Damage To Player
        public void DamagePlayer(float damage)
        {
            playerObjects[0].GetComponent<PlayerManager>().TakeDamage(damage);
            PV.RPC("SendPlayerHealthUpdate", RpcTarget.Others, damage);
        }

        [PunRPC]
        public void SendPlayerHealthUpdate(float damage)
        {  
            opponentHealth -= damage;
            CheckGameStatus();

        }

        #endregion


        #region Attack Management

        public void SetAttackPhase()
        {
            if (playerObjects[0].GetComponent<PlayerManager>().myTurn == true)
            {
                if (attacking == false)
                {
                    attacking = true;
                    endTurnButton.gameObject.SetActive(false);
                    skipBlockButton.gameObject.SetActive(false);
                    confirmBlockButton.gameObject.SetActive(false);
                    confirmAttackButton.gameObject.SetActive(true);
                    defending = false;
                    PV.RPC("SetDefenders", RpcTarget.Others);
                }
            }

        }

        [PunRPC]
        public void SetDefenders()
        {
            if (playerObjects[0].GetComponent<PlayerManager>().myTurn == false)
            {
                attacking = false;
                defending = true;
                endTurnButton.gameObject.SetActive(false);
                skipBlockButton.gameObject.SetActive(true);
                confirmBlockButton.gameObject.SetActive(true);
                confirmAttackButton.gameObject.SetActive(false);
            }
        }

        
        //Grab Info From PlayerObject[1]
        [PunRPC]
        public void SendDefendCard()
        {
           
        }

        [PunRPC]
        public void SendAttackCard()
        {

        }
        


        #endregion

    }
}
