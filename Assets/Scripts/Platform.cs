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
        Vector3 lookPosition = target.position;     //�ٶ󺸴� ������ ����� �� y���� ������ ����
        lookPosition.y = transform.position.y;
        transform.LookAt(lookPosition);  //������ �ٶ󺸱�
    }
}
