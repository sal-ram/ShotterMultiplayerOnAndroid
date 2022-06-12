using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        //MenuManager.Instance.OpenMenu("loading");
        Debug.Log("Connecting to MasterServer");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to MasterServer");
        PhotonNetwork.LoadLevel(1);
    }
}
