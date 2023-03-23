using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAutoLock : DoorAuto
{
    //bool isUnlock = false;

    
    BoxCollider tile;

    protected override void Awake()
    {
        base.Awake();
        tile = GetComponent<BoxCollider>();
        tile.enabled = false;
        
    }

    public void Unlock()
    {
        tile.enabled = true;
    }



    //private void OnTriggerEnter(Collider other)
    //{
    //    if(isUnlock)
    //    {
    //       Open();
    //    }
    //}


}
