using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : WayPointUser
{
    private void FixedUpdate()
    {
        Move();
    }

    protected override void SetTarget(Transform target)
    {
        base.SetTarget(target);
        Vector3 lookPosition = target.position;     //바라보는 방향을 계산할 때 y축의 영향을 제거
        lookPosition.y = transform.position.y;
        transform.LookAt(lookPosition);  //목적지 바라보기
    }
}
