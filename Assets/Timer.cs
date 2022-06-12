using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class Timer : MonoBehaviour
{
    bool startTimer = false;
    double timerIncrementValue;
    double startTime;
    [SerializeField] double timer = 10;
    ExitGames.Client.Photon.Hashtable CustomeValue;

    public static bool IsSessionEnd;

    [SerializeField] GameObject scoreboard;
    [SerializeField] TMP_Text timer_text;

    void Start()
    {
        IsSessionEnd = false;
        CustomeValue = new ExitGames.Client.Photon.Hashtable();

        Debug.Log(PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("StartTime"));

        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("StartTime"))
        {
            startTime = (double)PhotonNetwork.CurrentRoom.CustomProperties["StartTime"];
        }
        else 
        {
            startTime = PhotonNetwork.Time;
        }

        startTimer = true;
        CustomeValue.Add("StartTime", startTime);
        PhotonNetwork.CurrentRoom.SetCustomProperties(CustomeValue);

        /*if (PhotonNetwork.player.IsMasterClient)
        {
            CustomeValue = new ExitGames.Client.Photon.Hashtable();
            startTime = PhotonNetwork.time;
            startTimer = true;
            CustomeValue.Add("StartTime", startTime);
            PhotonNetwork.room.SetCustomProperties(CustomeValue);
        }
        else
        {
            startTime = double.Parse(PhotonNetwork.room.CustomProperties["StartTime"].ToString());
            startTimer = true;
        }*/
    }

    void Update()
    {
        if (!startTimer) 
            return;

        timerIncrementValue = PhotonNetwork.Time - startTime;

        timer_text.text = ((float) Mathf.Round((float) timerIncrementValue)).ToString();

        if (timerIncrementValue >= timer)
        {
            IsSessionEnd = true;
            scoreboard.SetActive(true);
            startTimer = false; 
        }
    }
}
