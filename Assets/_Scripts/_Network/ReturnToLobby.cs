using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

namespace BloodPeaksStudios
{
    public class ReturnToLobby : MonoBehaviour
    {
        public void GoToLobby()
        {
            Destroy(GameObject.Find("PlayerDataManager"));
            Destroy(GameObject.Find("RoomManager"));
            
            PhotonNetwork.LoadLevel(1);
           
        }
    }
}
