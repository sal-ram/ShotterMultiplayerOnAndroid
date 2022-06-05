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

    private void Start()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
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
    }

    private void AddPlayerStatistic(Player player)
    {
        var item = Instantiate(playerStatisticPrefab, transformParent);
        item.SetName(player.NickName);
        dictForPlayerAndStatistics.Add(player, item);
    }

    private void RemovePlayerStatistic(Player player)
    {
        Destroy(dictForPlayerAndStatistics[player].gameObject);
        dictForPlayerAndStatistics.Remove(player);
    }
}