using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DoorKeyUnlock : DoorKey
{
    
    Action onConsume;

    private void Start()
    {
        ResetTarget();
    }

    private void OnValidate()
    {
        //스코프
        ResetTarget();
    }

    void ResetTarget()
    {
        if (target != null)
        {
            DoorAutoLock lockDoor = target as DoorAutoLock;
            if (lockDoor != null)
            {
                onConsume += lockDoor.Unlock;
            }
            else
            {
                target = null;
            }
        }
    }

    protected override void OnConsume()
    {
        onConsume?.Invoke();
        Destroy(gameObject);
        Debug.Log("열렸습니다.");
    }

}
