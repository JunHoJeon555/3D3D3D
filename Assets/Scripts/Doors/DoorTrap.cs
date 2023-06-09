using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Switch;

public class DoorTrap : DoorManual
{
    ParticleSystem ps;
    Player player;

    protected override void Awake()
    {
        base.Awake();
        Transform child = transform.GetChild(2);
        ps = child.GetComponent<ParticleSystem>();
        
    }

    protected override void OnOpen()
    {     
        ps.Play();
        if(player != null)
        {
            player.Die();
        }

    }

    protected override void OnClose()
    {
      ps.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        player = other.GetComponent<Player>();   
    }
    private void OnTriggerExit(Collider other)
    {
        player = null;
    }

}
