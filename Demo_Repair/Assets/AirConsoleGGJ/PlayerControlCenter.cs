using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlCenter : MonoBehaviour
{
    private List<PlayerData> playerDataList = new List<PlayerData>();
    public List<AbsPlayer> players;
    public GameObject playerPrefab;

    //選角呼叫
    public void AddNewPlayer(int deviceID)
    {
        if (!IsPlayerExist(deviceID))
        {
            Debug.Log($"追加角色: {deviceID}");
            PlayerData playerData = new PlayerData(deviceID);
            playerDataList.Add(playerData);
            AbsPlayer player = Instantiate(playerPrefab).GetComponent<AbsPlayer>();
            player.SetData(playerData);
            players.Add(player);
        }
    }

    public void OnReceiveMessage(int deviceID, string keyName)
    {
        if (IsPlayerExist(deviceID))
        {
            PadCommand key = (PadCommand)Enum.Parse(typeof(PadCommand), keyName);
            GetPlayer(deviceID).ButtonInput(key);
        }
    }

    private bool IsPlayerExist(int deviceId)
    {
        foreach(var player in playerDataList)
        {
            if(player.deviceID == deviceId)
            {
                return true;
            }
        }
        return false;
    }

    private PlayerData GetPlayer(int deviceID)
    {
        foreach(var player in playerDataList)
        {
            if(player.deviceID == deviceID)
            {
                return player;
            }
        }
        return null;
    }
}

[Serializable]
public class PlayerData
{
    public int deviceID;
    public bool moveForward;
    public bool moveBack;
    public bool turnRight;
    public bool turnLeft;

    Action dodge;
    Action interact;

    public PlayerData(int ID)
    {
        deviceID = ID;
    }

    public void SetDodgeDelegate(Action action)
    {
        dodge = action;
    }

    public void SetFixAndDestroyDelegate(Action action)
    {
        interact = action;
    }

    public void ButtonInput(PadCommand input)
    {
        switch (input)
        {
            //前進
            case PadCommand.moveForward:
                moveForward = true;
                break;
            case PadCommand.moveForwardStop:
                moveForward = false;
                break;
            //左轉
            case PadCommand.turnLeft:
                turnLeft = true;
                break;
            case PadCommand.turnLeftStop:
                turnLeft = false;
                break;
            //後退
            case PadCommand.moveBack:
                moveBack = true;
                break;
            case PadCommand.moveBackStop:
                moveBack = false;
                break;
            //右轉
            case PadCommand.turnRight:
                turnRight = true;
                break;
            case PadCommand.turnRightStop:
                turnRight = false;
                break;
            //躲避
            case PadCommand.dodge:
                dodge?.Invoke();
                break;
            //修理&破壞
            case PadCommand.interact:
                interact?.Invoke();
                break;
        }
    }
}

public enum PadCommand
{
    moveForward,
    moveForwardStop,
    turnLeft,
    turnLeftStop,
    moveBack,
    moveBackStop,
    turnRight,
    turnRightStop,
    dodge,
    interact
}

