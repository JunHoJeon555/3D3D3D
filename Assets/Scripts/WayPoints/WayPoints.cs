using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    /// <summary>
    /// 이 웨이포인트에서 사용하는 웨이포인트 지점들
    /// </summary>
    Transform[] wayPoints;

    /// <summary>
    /// 현재 가고있는 웨이포인트의 인덱스(번호)
    /// </summary>
    int index = 0;          //현재 가고 있는 웨이포인트의 인덱스(번호)

    /// <summary>
    /// 현재 향하고 있는 웨이포인트의 트랜스폼 확인용 프로퍼티
    /// </summary>
    public Transform CurrentWayPoint => wayPoints[index];

    private void Awake()
    {
        //wayPoints = GetComponentsInChildren<Transform>(); 자기 작신도 포함해서 x
        wayPoints = new Transform[transform.childCount];
        for(int i = 0; i< wayPoints.Length; i++) 
        {
            wayPoints[i] = transform.GetChild(i);   
        }
    }

    /// <summary>
    /// 다음에 이동해야 할 웨이포인트를 알려주는 함수
    /// </summary>
    /// <returns>다음에 이동할 웨이포인트의 트랜스폼</returns>
    public Transform GetNextWayPoint()
    {
        index++;                   //index증가
        index %= wayPoints.Length; //index 0~(Waypoints.Length-1)까지만 되기.

        return wayPoints[index];   //해당 트랜스폼 리턴

    }
}
