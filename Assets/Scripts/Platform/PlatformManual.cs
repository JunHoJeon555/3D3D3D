using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class PlatformManual : Platform ,IUserbleObject
{ 

    bool isMovaing = false;


    /// <summary>
    /// private 프로퍼티. 내부에서 isMoving이 변경될 때 실행될 함수 추가 실행
    /// </summary>
    bool IsMoving
    {
        get => isMovaing;
        set
        {
            isMovaing= value;
            if (isMovaing)      //isMoving이 true일 때만 실행
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
    /// 직접 사용 금지
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
    /// 움직이기 시작할 때 실행하는 함수
    /// </summary>
    void ActivatePlatform()
    {
    }


    /// <summary>
    /// 정지 시킬 때 실행될 함수
    /// </summary>
    void DeactivatePlatform()
    {
    }

    /// <summary>
    /// 아이템을 사용하면 실행되는 함수
    /// </summary>
    public void Used()
    {
        isMovaing = !isMovaing; //isMoving만 반대로 변경
    }
}
