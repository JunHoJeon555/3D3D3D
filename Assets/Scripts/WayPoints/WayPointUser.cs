using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class WayPointUser : MonoBehaviour
{
    /// <summary>
    /// �� ������Ʈ�� ������ ��������Ʈ
    /// </summary>
    public WayPoints targetWaypoints;
    
    /// <summary>
    /// �̵��ӵ�
    /// </summary>
    public float moveSpeed = 5f;

    /// <summary>
    /// �̵����� �˷��ִ� ��������Ʈ
    /// </summary>
    public Action <Vector3> OnMove;

    /// <summary>
    /// �̹� �����ӿ� �̵��� ����
    /// </summary>
    protected Vector3 moveDelta = Vector3.zero;

    /// <summary>
    /// �鳯 ȸ���ӵ�
    /// </summary>
    public float spinSpeed = 720f;
    
    /// <summary>
    /// ������ ���� ����Ʈ
    /// </summary>
    Transform target;

    /// <summary>
    /// �� ������Ʈ�� �̵�����
    /// </summary>
    Vector3 moveDir;

    
 
    private void Start()
    {
        SetTarget(targetWaypoints.CurrentWayPoint); //ù��° ���� ����Ʈ
        
    }

    /// <summary>
    /// �̵�ó���� �Լ�, Update���� ȣ���� ��
    /// </summary>
    private void FixedUpdate()
    {
        Move();
    }




    protected void Move()
    {
        moveDelta = Time.fixedDeltaTime * moveSpeed * moveDir;  //�̵������� �����δ�/.
        transform.Translate(moveDelta, Space.World); //�̵�


        // (�Ÿ� < 0.1), (�Ÿ��� ���� < 0.1�� ����) ���� ����� ����.
        if ( (target.position - transform.position).sqrMagnitude < 0.01f) // �Ÿ��� 0.01���� ���� ��
        {
            //����
            SetTarget(targetWaypoints.GetNextWayPoint());   //���������� ������������Ʈ ������ �����ͼ� �����ϱ�
            moveDelta = Vector3.zero;
        }
        OnMove?.Invoke(moveDelta);
    }




    /// <summary>
    /// ���� ���� ����Ʈ �����ϴ� �Լ�
    /// </summary>
    /// <param name="target"></param>
    protected virtual void SetTarget(Transform target)
    {
        this.target = target;           //������ ����
        moveDir = (this.target.position - transform.position).normalized;   //�̵����� ����س���
    }
        
    

}
