using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerStatisticSystem : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform transformParent;
    [SerializeField] PlayerStatisticsItem playerStatisticPrefab;

    Dictionary<Player, PlayerStatisticsItem> dictForPlayerAndStatistics = new Dictionary<Player, PlayerStatisticsItem>();

    bool activeness;

    /* private void Start()
     {
         foreach (Player player in PhotonNetwork.PlayerList)
         {
             Debug.Log("Я первее!");
             AddPlayerStatistic(player);
         }
     }

     public override void OnPlayerEnteredRoom(Player newPlayer)
     {
         AddPlayerStatistic(newPlayer);
     }

     public override void OnPlayerLeftRoom(Player otherPlayer)
     {
         RemovePlayerStatistic(otherPlayer);
     }*/

    public override void OnEnable()
    {
        base.OnEnable();

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            Debug.Log("Я первее!");
            AddPlayerStatistic(player);
        }
    }

    private void AddPlayerStatistic(Player player)
    {
        Debug.Log(player.NickName + " " + " " +  (int)player.CustomProperties["deaths"]);
        Debug.Log(player.NickName + " " + " " + (int)player.CustomProperties["kills"]);
        //var playerManager = PhotonView.Find((int)player.CustomProperties["playerManager"]).GetComponent<PlayerManager>();
        var item = Instantiate(playerStatisticPrefab, transformParent);

        //item.playerManager = playerManager;

        item.SetName(player.NickName);
        item.SetDeaths((int) player.CustomProperties["deaths"]);
        item.SetKills((int)player.CustomProperties["kills"]);

        dictForPlayerAndStatistics.Add(player, item);
    }

    private void RemovePlayerStatistic(Player player)
    {
        Destroy(dictForPlayerAndStatistics[player].gameObject);
        dictForPlayerAndStatistics.Remove(player);
    }

   /* public void OnOrOff()
    {
        gameObject.SetActive(!activeness);
    }

    private void OnEnable()
    {
        activeness = true;
    }

    private void OnDisable()
    {
        activeness = false;
    }*/
}
