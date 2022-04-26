using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using TMPro;

namespace BloodPeaksStudios
{
    public class RoomListItem : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI roomText;
        public RoomInfo roomInfo;


        public void SetUp(RoomInfo info)
        {
            roomInfo = info;
            roomText.text = info.Name + " " + info.PlayerCount + "/" + " 2";
        }


        public void OnClick()
        {
            Launcher.Instance.JoinGame(roomInfo);
        }
    }
}
