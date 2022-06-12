using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SingleShotGun : Gun
{
    [SerializeField] Camera cam;
    PhotonView PV;
    PlayerController player;
    AudioSource audioController;
    public ParticleSystem shootEmission;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        player = transform.root.gameObject.GetComponent<PlayerController>();
        audioController = GetComponent<AudioSource>();
    }

    public override void Use()
    {
        shootEmission.Play();
        Shoot();
    }

    void Shoot()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        ray.origin = cam.transform.position;
        PV.RPC("RPC_Shoot", RpcTarget.All);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
           /*if (hit.collider.gameObject.GetComponent<PlayerController>()?.currentHealth <= ((GunInfo)itemInfo).damage)
           {
                if (PV.IsMine)
                {
                   transform.root.gameObject.GetComponent<PlayerController>().AddKill();
                }
           }*/

            hit.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(((GunInfo)itemInfo).damage, player.playerId);
            PV.RPC("RPC_HIT", RpcTarget.All, hit.point, hit.normal);
        }
    }

    [PunRPC]
    void RPC_HIT(Vector3 hitPosition, Vector3 hitNormal)
    {
        Collider[] colliders = Physics.OverlapSphere(hitPosition, 0.2f);

        if (colliders.Length != 0)
        {
            GameObject bulletImp =  Instantiate(bulletImpact, hitPosition, Quaternion.LookRotation(hitNormal));
            bulletImp.transform.SetParent(colliders[0].transform);
        }
    }


    [PunRPC]
    void RPC_Shoot()
    {
        audioController.PlayOneShot(((GunInfo)itemInfo).soundShot);
    }


}
