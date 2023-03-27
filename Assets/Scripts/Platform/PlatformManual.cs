using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class PlatformManual : Platform ,IUserbleObject
{ 

    bool isMovaing = false;


    /// <summary>
    /// private ������Ƽ. ���ο��� isMoving�� ����� �� ����� �Լ� �߰� ����
    /// </summary>
    bool IsMoving
    {
        get => isMovaing;
        set
        {
            isMovaing= value;
            if (isMovaing)      //isMoving�� true�� ���� ����
            {
                ActivatePlatform();
            }
            else
            {
                DeactivatePlatform();
            }
            
        }
    }

    /// <summary>
    /// ���� ��� ����
    /// </summary>
    public bool IsDirectUse => false;

    private void FixedUpdate()
    {
        if (isMovaing)
        {
            Move();
        }
    }

    /// <summary>
    /// �����̱� ������ �� �����ϴ� �Լ�
    /// </summary>
    void ActivatePlatform()
    {
    }


    /// <summary>
    /// ���� ��ų �� ����� �Լ�
    /// </summary>
    void DeactivatePlatform()
    {
    }

    /// <summary>
    /// �������� ����ϸ� ����Ǵ� �Լ�
    /// </summary>
    public void Used()
    {
        isMovaing = !isMovaing; //isMoving�� �ݴ�� ����
    }
}
