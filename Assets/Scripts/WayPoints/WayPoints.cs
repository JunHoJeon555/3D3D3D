using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    /// <summary>
    /// �� ��������Ʈ���� ����ϴ� ��������Ʈ ������
    /// </summary>
    Transform[] wayPoints;

    /// <summary>
    /// ���� �����ִ� ��������Ʈ�� �ε���(��ȣ)
    /// </summary>
    int index = 0;          //���� ���� �ִ� ��������Ʈ�� �ε���(��ȣ)

    /// <summary>
    /// ���� ���ϰ� �ִ� ��������Ʈ�� Ʈ������ Ȯ�ο� ������Ƽ
    /// </summary>
    public Transform CurrentWayPoint => wayPoints[index];

    private void Awake()
    {
        //wayPoints = GetComponentsInChildren<Transform>(); �ڱ� �۽ŵ� �����ؼ� x
        wayPoints = new Transform[transform.childCount];
        for(int i = 0; i< wayPoints.Length; i++) 
        {
            wayPoints[i] = transform.GetChild(i);   
        }
    }

    /// <summary>
    /// ������ �̵��ؾ� �� ��������Ʈ�� �˷��ִ� �Լ�
    /// </summary>
    /// <returns>������ �̵��� ��������Ʈ�� Ʈ������</returns>
    public Transform GetNextWayPoint()
    {
        index++;                   //index����
        index %= wayPoints.Length; //index 0~(Waypoints.Length-1)������ �Ǳ�.

        return wayPoints[index];   //�ش� Ʈ������ ����

    }
}
