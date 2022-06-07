using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatisticsItem : MonoBehaviour
{
    [SerializeField] private TMP_Text nickname_text;
    [SerializeField] private TMP_Text kills;
    [SerializeField] private TMP_Text deaths;

    public PlayerManager playerManager { get; set; }

    // Start is called before the first frame update
    private void Start()
    {
        playerManager.OnKillsUpdate += UpdateKills;
        playerManager.OnDeathsUpdate += UpdateDeaths;
    }

    public void SetName(string name)
    {
        nickname_text.text = name;
    }

    public void UpdateKills(int new_kills)
    {
        kills.text = new_kills.ToString();
    }

    public void UpdateDeaths(int newdeath)
    {
        deaths.text = newdeath.ToString();
    }
}
