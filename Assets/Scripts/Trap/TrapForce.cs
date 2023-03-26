using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapForce : Trap_Base
{

    public float forcePower = 5f;

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    protected override void OnTrapActivate(GameObject target)
    {
        base.OnTrapActivate(target);
        anim.SetTrigger("Activate");
        Player player = target.GetComponent<Player>();
        if (player != null)
        {
            Vector3 dir = (transform.forward + transform.up).normalized;
            player.Rigid.AddForce(dir * forcePower, ForceMode.Impulse);
            player.SetForceJumpMode();
        }
    }




}
