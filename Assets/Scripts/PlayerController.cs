using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerController : MonoBehaviourPunCallbacks, IDamageable
{
    private PlayerInput playerInput;

    private float inputVertical;
    private float inputHorizontal;

    private float inputVerticalRot;
    private float inputHorizontalRot;

    [SerializeField] private PlayerBodyMovement bodyObj;
    [SerializeField] private PlayerLookMovement lookObj;
    [SerializeField] private PlayerWeapon gunObj;

    PhotonView PV;

    PlayerManager playerManager;

    const float maxHealth = 150f;
    float currentHealth = maxHealth;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
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

        playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();
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
        /*inputVerticalRot = Input.GetAxis("Mouse Y");
        inputHorizontalRot = Input.GetAxis("Mouse X");*/
        Vector2 a = playerInput.actions["Look"].ReadValue<Vector2>();

        Debug.Log(a);

        lookObj.MoveLookByMouse(a.y, a.x);
    }

    void Move()
    {
        Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);

       /* inputVertical = Input.GetAxis("Vertical");
        inputHorizontal = Input.GetAxis("Horizontal");*/

        bodyObj.MoveBody(move.x, move.z);
    }

    void Jump()
    {
        if (playerInput.actions["Jump"].triggered)
        {
            Debug.Log(1);
            bodyObj.Jump();
        }
    }

    void GunFunctionality()
    {
        /*//Equip gun by numbers
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

        //Fire
        if (Input.GetMouseButtonDown(0))
        {
            gunObj.Attack(index);
        }*/
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
        if (!PV.IsMine)
            return;
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
        
    }

    void Die()
    {
        playerManager.Die();
    }
}
