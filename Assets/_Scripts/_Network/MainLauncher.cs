using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using TMPro;

namespace BloodPeaksStudios
{
    public class MainLauncher : MonoBehaviourPunCallbacks
    {
        // Start is called before the first frame update
        void Start()
        {

            ConnectToPhoton();

        }

        public void ConnectToPhoton()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {

            PhotonNetwork.JoinLobby();


        }

        public override void OnJoinedLobby()
        {
            
            PhotonNetwork.LoadLevel(1);

        }
    }
}
