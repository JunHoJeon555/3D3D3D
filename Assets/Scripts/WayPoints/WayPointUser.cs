using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class WayPointUser : MonoBehaviour
{
    /// <summary>
    /// 이 오브젝트가 움직일 웨이포인트
    /// </summary>
    public WayPoints targetWaypoints;
    
    /// <summary>
    /// 이동속도
    /// </summary>
    public float moveSpeed = 5f;

    /// <summary>
    /// 이동량을 알려주는 델리게이트
    /// </summary>
    public Action <Vector3> OnMove;

    /// <summary>
    /// 이번 프레임에 이동한 정도
    /// </summary>
    protected Vector3 moveDelta = Vector3.zero;

    /// <summary>
    /// 톱날 회전속도
    /// </summary>
    public float spinSpeed = 720f;
    
    /// <summary>
    /// 목적지 웨이 포인트
    /// </summary>
    Transform target;

    /// <summary>
    /// 이 오브젝트이 이동방향
    /// </summary>
    Vector3 moveDir;

    
 
    private void Start()
    {
        SetTarget(targetWaypoints.CurrentWayPoint); //첫번째 웨이 포인트
        
    }

    /// <summary>
    /// 이동처리용 함수, Update에서 호출할 것
    /// </summary>
    private void FixedUpdate()
    {
        Move();
    }




    protected void Move()
    {
        moveDelta = Time.fixedDeltaTime * moveSpeed * moveDir;  //이동방향대로 움직인다/.
        transform.Translate(moveDelta, Space.World); //이동


        // (거리 < 0.1), (거리의 제곱 < 0.1의 제곱) 둘의 결과는 같다.
        if ( (target.position - transform.position).sqrMagnitude < 0.01f) // 거리가 0.01보다 작을 때
        {
            //도착
            SetTarget(targetWaypoints.GetNextWayPoint());   //도착했으면 다음웨이포인트 지접ㅁ 가져와서 설정하기
            moveDelta = Vector3.zero;
        }
        OnMove?.Invoke(moveDelta);
    }




    /// <summary>
    /// 다음 웨이 포인트 지정하는 함수
    /// </summary>
    /// <param name="target"></param>
    protected virtual void SetTarget(Transform target)
    {
        this.target = target;           //목적지 설정
        moveDir = (this.target.position - transform.position).normalized;   //이동방향 기록해놓기
    }
        
    

}
