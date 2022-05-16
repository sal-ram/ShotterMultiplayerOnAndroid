using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerWeapon: MonoBehaviour
{
    //[SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Item[] items;
    public int itemIndex { get; private set; }
    int itemPreviousIndex = -1;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /*public void ShotBullet()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs","Bullet"), transform.position, transform.rotation);
    }*/

    public void Equip(int _index)
    {
        if (_index == itemPreviousIndex)
            return;

        itemIndex = _index;

        items[itemIndex].itemGameObject.SetActive(true);

        if (itemPreviousIndex != -1)
        {
            items[itemPreviousIndex].itemGameObject.SetActive(false);
        }

        itemPreviousIndex = itemIndex;
    }

    public void Attack(int index)
    {
        items[index].Use();
    }


}
