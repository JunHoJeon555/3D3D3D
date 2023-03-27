using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
    /// �鳯 ȸ���ӵ�
    /// </summary>
    public float spinSpeed = 720f;
    
    /// <summary>
    /// ������ ���� ����Ʈ
    /// </summary>
    Transform target;


    private void Start()
    {
        SetTarget(targetWaypoints.CurrentWayPoint); //ù��° ���� ����Ʈ
    }

    private void Update()
    {
        Move();
    }




    protected void Move()
    {
        
        transform.Translate(Time.deltaTime *moveSpeed * transform.forward, Space.World); //�̵�


        // (�Ÿ� < 0.1), (�Ÿ��� ���� < 0.1�� ����) ���� ����� ����.
        if ( (target.position - transform.position).sqrMagnitude < 0.01f) // �Ÿ��� 0.01���� ���� ��
        {
            //����
            SetTarget(targetWaypoints.GetNextWayPoint());   //���������� ������������Ʈ ������ �����ͼ� �����ϱ�
        }
    }



    /// <summary>
    /// ���� ���� ����Ʈ �����ϴ� �Լ�
    /// </summary>
    /// <param name="target"></param>
    private void SetTarget(Transform target)
    { 
        this.target = target;           //������ ����
        transform.LookAt(this.target);  //������ �ٶ󺸱�
    
    }
        
    

}