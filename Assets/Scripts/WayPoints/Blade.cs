using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Blade : WayPointUser
{

    
;
 
    /// <summary>
    /// 칼날
    /// </summary>
    Transform bladeMesh;

    

    private void Awake()
    {
        
        bladeMesh = transform.GetChild(0);
    }


   

    private void Update()
    { 
        bladeMesh.Rotate(Time.deltaTime * spinSpeed * Vector3.right);   //톱날 회전
        Move();
    }


    private void OnCollisionEnter(Collision collision)
    {
        // 이 오브젝트는 플레이어 하고만 충돌을 하므로 충돌이 일어나면 무조선 플레이어다
        Player player = collision.gameObject.GetComponent<Player>();
        if(player != null ) 
        { 
            player.Die();
        }
        
    }


    /// <summary>
    /// 다음 웨이 포인트 지정하는 함수
    /// </summary>
    /// <param name="target"></param>
    private void SetTarget(Transform target)
    { 
        this.target = target;           //목적지 설정
        transform.LookAt(this.target);  //목적지 바라보기
    
    }
        
    

}
