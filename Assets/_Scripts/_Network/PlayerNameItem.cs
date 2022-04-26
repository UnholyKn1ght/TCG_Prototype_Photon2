using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class PlayerNameItem : MonoBehaviourPunCallbacks
{
    [SerializeField] TextMeshProUGUI text;
    Player playerInfo;

    public void SetUp(Player player)
    {
        playerInfo = player;
        text.text = player.NickName;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (playerInfo == otherPlayer)
        {
            Destroy(gameObject);
        }
    }

    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }
}
