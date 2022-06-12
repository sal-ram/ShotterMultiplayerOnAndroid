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
       /* playerManager.OnKillsUpdate += UpdateKills;
        playerManager.OnDeathsUpdate += UpdateDeaths;*/
    }

    public void SetName(string name)
    {
        nickname_text.text = name;
    }

    public void SetKills(int kills_new)
    {
        kills.text = kills_new.ToString();
    }

    public void SetDeaths(int deaths_new)
    {
        deaths.text = deaths_new.ToString();
    }
}
