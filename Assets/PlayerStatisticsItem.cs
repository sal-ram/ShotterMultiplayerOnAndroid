using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatisticsItem : MonoBehaviour
{
    [SerializeField] private TMP_Text nickname_text;
    [SerializeField] private TMP_Text kills;
    [SerializeField] private TMP_Text deaths;
    // Start is called before the first frame update
    public 
    private void Start()
    {
        
    }

    public void SetName(string name)
    {
        nickname_text.text = name;
    }

    public void UpdateKills(int newkill)
    {
        kills.text = newkill.ToString();
    }

    public void UpdateDeaths(int newdeath)
    {
        deaths.text = newdeath.ToString();
    }
}
