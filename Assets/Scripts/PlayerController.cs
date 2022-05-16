using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerController : MonoBehaviourPunCallbacks, IDamagable
{
    private float inputVertical;
    private float inputHorizontal;

    private float inputVerticalRot;
    private float inputHorizontalRot;

    [SerializeField] private PlayerBodyMovement bodyObj;
    [SerializeField] private PlayerLookMovement lookObj;
    [SerializeField] private PlayerWeapon gunObj;


    PhotonView PV;
    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if (PV.IsMine)
        {
            gunObj.Equip(0);
        }
        else
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(GetComponentInChildren<PlayerGravityController>().gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            Move();
            Look();
            Jump();
            GunFunctionality();

            /*if (Input.GetButtonDown("Fire1"))
            {
                gunObj.ShotBullet();
            }*/
        }
    }

    void Look()
    {
        inputVerticalRot = Input.GetAxis("Mouse Y");
        inputHorizontalRot = Input.GetAxis("Mouse X");

        lookObj.MoveLookByMouse(inputVerticalRot, inputHorizontalRot);
    }

    void Move()
    {
        inputVertical = Input.GetAxis("Vertical");
        inputHorizontal = Input.GetAxis("Horizontal");

        bodyObj.MoveBody(inputVertical, inputHorizontal);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log(1);
            bodyObj.Jump();
        }
    }

    void GunFunctionality()
    {
        //Equip gun by numbers
        int massOfGunsLength = gunObj.transform.childCount;

        for (int i = 0; i < massOfGunsLength; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                gunObj.Equip(i);
            }
        }

        //Switch Gun by Mouse ScrollWheel
        int index = gunObj.itemIndex;

        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            if (index >= massOfGunsLength - 1)
            {
                gunObj.Equip(0);
            }
            else
                gunObj.Equip(index + 1);

        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            if (index <= 0)
            {
                gunObj.Equip(massOfGunsLength - 1);
            }
            else
                gunObj.Equip(index - 1);
        }

        //Synchronize switching weapons
        if (PV.IsMine)
        {
            Hashtable hash = new Hashtable();
            hash.Add("itemIndex", index);
            PhotonNetwork.SetPlayerCustomProperties(hash);
        }

        if (Input.GetMouseButtonDown(0))
        {
            gunObj.Attack(index);
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (!PV.IsMine && targetPlayer == PV.Owner)
        {
            gunObj.Equip((int)changedProps["itemIndex"]);
        }
    }

    public void TakeDamage(float damage)
    {
        PV.RPC("RPC_TakeDamage", RpcTarget.All, damage);
    }

    [PunRPC]
    void RPC_TakeDamage(float damage)
    {
        if (PV.IsMine)
        {
            Debug.Log("took damage: " + damage);
        }
    }
}
