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

        controller =  PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), position, rotation, 0, new object[] { PV.ViewID });
    }

    public void Die()
    {
        PhotonNetwork.Destroy(controller.gameObject);
        CreateController();
    }

}
