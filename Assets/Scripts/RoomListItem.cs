using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;
public class RoomListItem : MonoBehaviour
{
    [SerializeField] TMP_Text roomName;

    public RoomInfo info { get; set; }

    public void SetUp(RoomInfo _info)
    {
        info = _info;
        roomName.text = _info.Name;
    }

    public void OnClick()
    {
        GameManager.Instance.JoinRoom(info);
    }
}
