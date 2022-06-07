using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.InputSystem;

public class ScoreboardController : MonoBehaviour
{
    PhotonView PV;
    [SerializeField] GameObject crossboardList;
    PlayerInput playerInput;
    bool active = false;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        PV = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (playerInput.actions["Scoreboard"].triggered)
        {
            Debug.Log("hmmmm");
            crossboardList.SetActive(!active);
            active = !active;
        }
    }
}
