using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance;

    [SerializeField] TMP_InputField create_room_inputField;
    [SerializeField] TMP_Text error_text;
    [SerializeField] TMP_Text nameRoom_text;
    [SerializeField] Transform roomListContainer;
    [SerializeField] GameObject roomItemPrefab;
    [SerializeField] Transform playerListContainer;
    [SerializeField] GameObject playerItemPrefab;
    [SerializeField] GameObject startGameButton;

    [HideInInspector] public string playerName;
    // Start is called before the first frame update
    void Start()
    {
        /*MenuManager.Instance.OpenMenu("loading");
        Debug.Log("Connecting to MasterServer");
        PhotonNetwork.ConnectUsingSettings();*/

        MenuManager.Instance.OpenMenu("mainMenu");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = "Player" + Random.Range(0, 1000);

        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("deaths") && PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("kills"))
        {
            PhotonNetwork.LocalPlayer.CustomProperties["deaths"] = 0;
            PhotonNetwork.LocalPlayer.CustomProperties["kills"] = 0;
        }
        else
        {
            PhotonNetwork.LocalPlayer.CustomProperties.Add("deaths", 0);
            PhotonNetwork.LocalPlayer.CustomProperties.Add("kills", 0);
        }
    }

    private void Awake()
    {
        Instance = this;
    }

   /* public override void OnConnectedToMaster()
    {   
        Debug.Log("Connected to MasterServer");
        MenuManager.Instance.OpenMenu("mainMenu");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = "Player" + Random.Range(0, 1000);

        PhotonNetwork.LocalPlayer.CustomProperties.Add("deaths", 0);
        PhotonNetwork.LocalPlayer.CustomProperties.Add("kills", 0);
    }*/

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
    }

    public void CreateRoom()
    {
        RoomOptions options = new RoomOptions();
        options.BroadcastPropsChangeToAll = true;
        if (string.IsNullOrEmpty(create_room_inputField.text))
        {
            return;
        }

        PhotonNetwork.CreateRoom(create_room_inputField.text);
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu("room");
        nameRoom_text.text = PhotonNetwork.CurrentRoom.Name;
        Player[] players = PhotonNetwork.PlayerList;

        foreach (Transform child in playerListContainer)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < players.Length; i++)
        {
            Instantiate(playerItemPrefab, playerListContainer).GetComponent<PlayerListItem>().SetUp(players[i]);
        }

        startGameButton.SetActive(PhotonNetwork.IsMasterClient);

    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(2);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        error_text.text = "Creation Room is failed: " + message;
        MenuManager.Instance.OpenMenu("error");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("mainMenu");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform obj in roomListContainer)
        {
            Destroy(obj.gameObject);
        }

        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;
            Instantiate(roomItemPrefab, roomListContainer).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerItemPrefab, playerListContainer).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu("loading");
    }

    public void Quit()
    {
        Application.Quit();
    }

}
