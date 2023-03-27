using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAuto : Platform
{

    /// <summary>
    /// �÷����� ���� ������ ���θ� �����ϴ� ���� true�� ���� false�ȿ���
    /// </summary>
    bool isMoving = true;

   
    private void FixedUpdate()
    {
        if (isMoving) //isMoving�� ture�� ���� ������
        {
            Move();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))  //�÷��̾ �ٴ� Ʈ���ſ� ������ ������
        {
            isMoving = true;
        }
    }

    /// <summary>
    /// ���������� �����ϸ� ����Ǵ� �Լ� 
    /// </summary>
    protected override void OnArrived()
    {
        base.OnArrived();
        isMoving = false;
    }


}
