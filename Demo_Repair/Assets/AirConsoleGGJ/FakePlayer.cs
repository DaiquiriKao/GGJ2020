using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePlayer : AbsPlayer
{
    protected override void MoveForward()
    {
        Debug.Log($"玩家{data.deviceID}: 前進");
    }

    protected override void MoveBack()
    {
        Debug.Log($"玩家{data.deviceID}: 後退");
    }

    protected override void TurnLeft()
    {
        Debug.Log($"玩家{data.deviceID}: 左轉");
    }

    protected override void TurnRight()
    {
        Debug.Log($"玩家{data.deviceID}: 右轉");
    }

    protected override void Dodge()
    {
        Debug.Log($"玩家{data.deviceID}: 閃避");
    }

    protected override void Interact()
    {
        Debug.Log($"玩家{data.deviceID}: 修理/破壞");
    }
}

public abstract class AbsPlayer : MonoBehaviour
{
    protected PlayerData data;

    public void SetData(PlayerData playerData)
    {
        data = playerData;
        data.SetDodgeDelegate(Dodge);
        data.SetFixAndDestroyDelegate(Interact);
    }

    protected virtual void Update()
    {
        Move();
    }

    protected void Move()
    {
        if (data != null)
        {
            if (data.moveForward)
            {
                MoveForward();
            }
            else if (data.moveBack)
            {
                MoveBack();
            }
            if (data.turnLeft)
            {
                TurnLeft();
            }
            else if (data.turnRight)
            {
                TurnRight();
            }
        }
    }

    protected abstract void MoveForward();
    protected abstract void MoveBack();
    protected abstract void TurnLeft();
    protected abstract void TurnRight();
    protected abstract void Dodge();
    protected abstract void Interact();

}
