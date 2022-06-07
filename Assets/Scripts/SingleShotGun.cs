using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SingleShotGun : Gun
{
    [SerializeField] Camera cam;
    PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    public override void Use()
    {
        Shoot();
    }

    void Shoot()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        ray.origin = cam.transform.position;
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
           if (hit.collider.gameObject.GetComponent<PlayerController>()?.currentHealth <= ((GunInfo)itemInfo).damage)
           {
                if (PV.IsMine)
                {
                   transform.root.gameObject.GetComponent<PlayerController>().AddKill();
                }
           }

            hit.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(((GunInfo)itemInfo).damage);
            PV.RPC("RPC_Shoot", RpcTarget.All, hit.point, hit.normal);
        }
    }

    [PunRPC]
    void RPC_Shoot(Vector3 hitPosition, Vector3 hitNormal)
    {
        Collider[] colliders = Physics.OverlapSphere(hitPosition, 0.2f);

        if (colliders.Length != 0)
        {
            GameObject bulletImp =  Instantiate(bulletImpact, hitPosition, Quaternion.LookRotation(hitNormal));
            bulletImp.transform.SetParent(colliders[0].transform);
        }
    }
}
