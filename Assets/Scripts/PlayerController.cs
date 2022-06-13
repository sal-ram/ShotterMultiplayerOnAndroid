using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerController : MonoBehaviourPunCallbacks, IDamageable
{
    private MultiplayerShooter playerInput;

    private float inputVertical;
    private float inputHorizontal;

    private float inputVerticalRot;
    private float inputHorizontalRot;

    [SerializeField] private PlayerBodyMovement bodyObj;
    [SerializeField] private PlayerLookMovement lookObj;
    [SerializeField] private PlayerWeapon gunObj;

    PhotonView PV;

    PlayerManager playerManager;

    PlayerStatisticSystem playerStatisticSystem;

    Hashtable hash;

    public int playerId { get; private set; }

    const float maxHealth = 150f;
    public float currentHealth { get; private set; } = maxHealth;
    private void Awake()
    {
        playerInput = new MultiplayerShooter();
        PV = GetComponent<PhotonView>();

        playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();

        playerId = PV.ViewID;

        playerStatisticSystem = FindObjectOfType<PlayerStatisticSystem>();

        hash = new Hashtable();

        //hash.Add("playerManager", (int)PV.InstantiationData[0]);

        Debug.Log("Я первее!");
    }

    public override void OnEnable()
    {
        base.OnEnable();
        playerInput.Enable();
    }

    public override void OnDisable()
    {
        base.OnDisable();
        playerInput.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;

        if (PV.IsMine)
        {
            gunObj.Equip(0);
            
        }
        else
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(GetComponentInChildren<PlayerGravityController>().gameObject);
            //Destroy(GetComponentInChildren<Canvas>().gameObject);
        }

       /* playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();
        
        playerStatisticSystem = FindObjectOfType<PlayerStatisticSystem>();

        hash = new Hashtable();

        hash.Add("playerManager", (int)PV.InstantiationData[0]);*/

        //hash = new Hashtable();
        //hash.Add("playerManager", (int)PV.InstantiationData[0]);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Timer.IsSessionEnd)
        {
            if (PV.IsMine)
            {
                Move();
                Look();
                Jump();
                GunFunctionality();

                if (gameObject.transform.position.y < -13)
                {
                    Die();
                }
            }
        }
    }

    void Look()
    {
        /*inputVerticalRot = Input.GetAxis("Mouse Y");
        inputHorizontalRot = Input.GetAxis("Mouse X");*/
        Vector2 a = playerInput.Player.Look.ReadValue<Vector2>();

        //Debug.Log(a);

        lookObj.MoveLookByMouse(a.y, a.x);
    }

    void Move()
    {
        // Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();
        Vector2 input = playerInput.Player.Move.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);

        /* inputVertical = Input.GetAxis("Vertical");
         inputHorizontal = Input.GetAxis("Horizontal");*/
        //Debug.Log(input);
        bodyObj.MoveBody(move.x, move.z);
    }

    void Jump()
    {
        if (playerInput.Player.Jump.triggered)
        {
            Debug.Log("Jump");
            bodyObj.Jump();
        }
    }

    void GunFunctionality()
    {
        //Equip gun by numbers
        int massOfGunsLength = gunObj.transform.childCount;

        /*for (int i = 0; i < massOfGunsLength; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                gunObj.Equip(i);
            }
        }*/

        //Switch Gun by Mouse ScrollWheel
        int index = gunObj.itemIndex;

        if (playerInput.Player.ChangeWeapon.triggered)
        {
            if (index >= massOfGunsLength - 1)
            {
                gunObj.Equip(0);
            }
            else
            {
                gunObj.Equip(index + 1);

            }
        }
       /* else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            if (index <= 0)
            {
                gunObj.Equip(massOfGunsLength - 1);
            }
            else
                gunObj.Equip(index - 1);
        }*/

        //Synchronize switching weapons
        if (PV.IsMine)
        {
            hash.Remove("itemIndex");
            hash.Add("itemIndex", index);
            PhotonNetwork.SetPlayerCustomProperties(hash);
        }

        //Fire
        if (playerInput.Player.Fire.triggered)
        {
            //Debug.Log("Fire");
            gunObj.Attack(index);
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
       // Debug.Log(targetPlayer.CustomProperties);
        if (!PV.IsMine && targetPlayer == PV.Owner)
        {
            if (changedProps.ContainsKey("itemIndex"))
            {
                gunObj.Equip((int)changedProps["itemIndex"]);
            }
        }
    }

    public void TakeDamage(float damage, int playerId)
    {
        PV.RPC("RPC_TakeDamage", RpcTarget.All, new object[] { damage, playerId });
    }

    [PunRPC]
    void RPC_TakeDamage(float damage, int playerId)
    {
        if (!PV.IsMine)
            return;
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Debug.Log(playerId);
            var killer = PhotonView.Find(playerId).GetComponent<PlayerController>();
            killer.AddKill();
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("В меня попали");
    }

    void Die()
    {
        int deaths = (int) PhotonNetwork.LocalPlayer.CustomProperties["deaths"];
        deaths += 1;
        PhotonNetwork.LocalPlayer.CustomProperties["deaths"] = deaths;

        var hash = new Hashtable();

        hash.Add("deaths", deaths);

        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);

        playerManager.Die();
    }

    public void AddKill()
    {
        int kills = (int)PhotonNetwork.LocalPlayer.CustomProperties["kills"];
        kills += 1;
        PhotonNetwork.LocalPlayer.CustomProperties["kills"] = kills;

        var hash = new Hashtable();

        hash.Add("kills", kills);

        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }
}
