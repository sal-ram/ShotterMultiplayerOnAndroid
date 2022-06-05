using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;

    GameObject controller;

    SpawnManager spawnManager;

    public int player_kills { get; set; } = 0;
    public int player_deaths { get; set; } = 0;

    // Start is called before the first frame update

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    void Start()
    {
        if (PV.IsMine)
        {
            CreateController();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateController()
    {
        Vector3 position = SpawnManager.Instance.GetSpawnPoint().position;
        Quaternion rotation = SpawnManager.Instance.GetSpawnPoint().rotation;

        controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), position, rotation, 0, new object[] { PV.ViewID });
    }

    public void Die()
    {
        AddDeath();
        PhotonNetwork.Destroy(controller.gameObject);
        CreateController();
    }

    public void AddKill()
    {
        player_kills += 1;
    }

    public void AddDeath()
    {
        player_deaths += 1;
    }
}
