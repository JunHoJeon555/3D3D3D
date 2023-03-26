using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Base : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            TrapActivate(other.gameObject);
        }
    }

    private void TrapActivate(GameObject target)
    {
        OnTrapActivate(target);
    }

    /// <summary>
    /// TrapBase�� ��� ���� �������� �������̵��� �Լ�
    /// </summary>
    /// <param name="target">������ ���� ���</param>
    protected virtual void OnTrapActivate(GameObject target) 
    { 
    }
}