using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Spike : Trap_Base
{

    Animator anim;


    private void Awaek()
    {
       anim = GetComponent<Animator>();
    }

    protected override void OnTrapActivate(GameObject target)
    {
        base.OnTrapActivate(target);
        anim.SetTrigger("Activate");
    }

}
