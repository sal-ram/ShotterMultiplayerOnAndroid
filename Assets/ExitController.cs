using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.UI;

public class ExitController : MonoBehaviourPunCallbacks
{
    Button buttonExit;
    // Start is called before the first frame update

    private void Start()
    {
        buttonExit = GetComponent<Button>();
        buttonExit.onClick.AddListener(ExitToMenu);
    }
    public void ExitToMenu()
    {
        Debug.Log("here");
        PhotonNetwork.LeaveRoom();
        Destroy(RoomManager.Instance);
    }

    /*public override void OnLeftRoom()
    {
        base.OnLeftRoom();
    }*/

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.LoadLevel(1);
    }
}
