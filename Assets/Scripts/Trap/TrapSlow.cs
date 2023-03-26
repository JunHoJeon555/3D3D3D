using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSlow : Trap_Base
{
    public float slowDuration = 5f;

    protected override void OnTrapActivate(GameObject target)
    {
        base.OnTrapActivate(target);

        Player player = target.GetComponent<Player>();
        player.SetHalfSpeed();
        if(player != null)
        {
            player.SetHalfSpeed();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if(player != null) 
            {
                player.ResetMoveSpeed();
                StartCoroutine(RestoreSpeed(player));
            }
        }
    }
    
   
    IEnumerator RestoreSpeed(Player player)
    {
        yield return new WaitForSeconds(slowDuration);
        player.ResetMoveSpeed();

    }


}
