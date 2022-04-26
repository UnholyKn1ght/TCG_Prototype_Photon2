using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using TMPro;

namespace BloodPeaksStudios
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        public static Launcher Instance;

        public GameObject mainMenuPanel;
        public GameObject joiningPanel;
        public GameObject loadPanel;
        public GameObject createRoomPanel;
        public GameObject findGamePanel;

        public GameObject roomMenu;
        public TextMeshProUGUI roomText;

        public GameObject errorPanel;
        public TextMeshProUGUI errorText;

        public GameObject createUserWindow;
        public TMP_InputField userNameField;

        public TextMeshProUGUI usernameText;

        [SerializeField] Transform gameListContent;
        [SerializeField] GameObject gameListPrefab;
        [SerializeField] GameObject playerListPrefab;
        [SerializeField] Transform playerListContent;
        [SerializeField] TMP_InputField roomNameInputField;
        [SerializeField] GameObject startGameButton;


        [SerializeField] TextMeshProUGUI testCount;


        // Start is called before the first frame update
        void Start()
        {
            SetPlayerData();
            Screen.SetResolution(1920, 1080, true);

        }


        public void Update()
        {
            if (PlayerPrefs.HasKey("PlayerName"))
            {
                usernameText.text = "User : " + PhotonNetwork.NickName;
                PhotonNetwork.NickName = SaveManager.Instance.LoadString("PlayerName");
            }
        }


        public void SetPlayerData()
        {
            if (PlayerPrefs.HasKey("PlayerName"))
            {


            }
            else
            {
                OpenCreateUserWindow();
            }


            if (PlayerPrefs.HasKey("PlayerCardCollection"))
            {
                PlayerDataManager.Instance.playerCardColleciton = SaveManager.Instance.LoadPlayerCardCollection();
            }
            
        }

        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;

            mainMenuPanel.SetActive(true);

            
            
            Instance = this;
        }


        public void OpenCreateRoomMenu()
        {
            joiningPanel.SetActive(false);
            loadPanel.SetActive(false);
            errorPanel.SetActive(false);
            createRoomPanel.SetActive(true);
            mainMenuPanel.SetActive(false);
        }

        public void CreateRoom()
        {
            
            if (string.IsNullOrEmpty(roomNameInputField.text))
            {
                return;
            }
            //Create The New Room Is InputField Is Not Null //Add Features Later
            RoomOptions roomOps = new RoomOptions() { MaxPlayers = 2 };
            PhotonNetwork.CreateRoom(roomNameInputField.text, roomOps);
            mainMenuPanel.SetActive(false);
            joiningPanel.SetActive(false);
            loadPanel.SetActive(true);
        }

        public override void OnJoinedRoom()
        {


            loadPanel.SetActive(false);
            createRoomPanel.SetActive(false);
            roomMenu.SetActive(true);
            roomText.text = PhotonNetwork.CurrentRoom.Name;
            Player[] players = PhotonNetwork.PlayerList;

            foreach (Transform child in playerListContent)
            {
                Destroy(child.gameObject);
            }


            for (int i = 0; i < players.Length; i++)
            {
                Instantiate(playerListPrefab, playerListContent).GetComponent<PlayerNameItem>().SetUp(players[i]);
            }

            startGameButton.SetActive(PhotonNetwork.IsMasterClient);

        }


        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            startGameButton.SetActive(PhotonNetwork.IsMasterClient);
        }

        public void BackToMainMenu()
        {
            joiningPanel.SetActive(false);
            loadPanel.SetActive(false);
            errorPanel.SetActive(false);
            createRoomPanel.SetActive(false);
            mainMenuPanel.SetActive(true);
            findGamePanel.SetActive(false);
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            errorText.text = "Room Creation Failed " + message;
            joiningPanel.SetActive(false);
            loadPanel.SetActive(false);
            errorPanel.SetActive(true);
            createRoomPanel.SetActive(false);
            mainMenuPanel.SetActive(false);

        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
            roomMenu.SetActive(false);
            loadPanel.SetActive(true);
            BackToMainMenu();
        }

        public void OpenFindGameWindow()
        {
            mainMenuPanel.SetActive(false);
            findGamePanel.SetActive(true);

        }


        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            foreach (Transform trans in gameListContent)
            {
                Destroy(trans.gameObject);
            }

            for (int i = 0; i < roomList.Count; i++)
            {
                if (roomList[i].RemovedFromList)
                    continue;

                Instantiate(gameListPrefab, gameListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
            }
        }

        public void JoinGame(RoomInfo info)
        {
            PhotonNetwork.JoinRoom(info.Name);
            findGamePanel.SetActive(false);
            loadPanel.SetActive(true);


        }


        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Instantiate(playerListPrefab, playerListContent).GetComponent<PlayerNameItem>().SetUp(newPlayer);
        }

        //Load Players Into The Main Game
        public void StartGame()
        {
            PhotonNetwork.LoadLevel(2);
        }

        public void OpenCreateUserWindow()
        {
            createUserWindow.SetActive(true);
        }

        public void SavePlayerUsername()
        {
            SaveManager.Instance.SaveString("PlayerName", userNameField.text);
            usernameText.text = "User : " + SaveManager.Instance.LoadString("PlayerName");
            PhotonNetwork.NickName = SaveManager.Instance.LoadString("PlayerName");
            PlayerDataManager.Instance.playerNickName = SaveManager.Instance.LoadString("PlayerName");
            createUserWindow.SetActive(false);
        }


        public void QuitGame()
        {
            Application.Quit();

        }
    }
}
