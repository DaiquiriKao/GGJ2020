using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class AirConsoleProxy : MonoBehaviour
{
    [SerializeField] private PlayerControlCenter playerCenter;

    private void Awake()
    {
        AirConsole.instance.onMessage += OnMessage;
        AirConsole.instance.onConnect += OnConnect;
    }

    private void OnConnect(int device)
    {
        playerCenter.AddNewPlayer(device);
    }

    private void OnMessage(int from, JToken data)
    {
        Debug.Log("message: " + data);

        if (data["action"] != null)
        {
            playerCenter.OnReceiveMessage(from, data["action"].ToString());
        }
    }

    private void OnDestroy()
    {
        if(AirConsole.instance != null)
        {
            AirConsole.instance.onMessage -= OnMessage;
        }
    }
}