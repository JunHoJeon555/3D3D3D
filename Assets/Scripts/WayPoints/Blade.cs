using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Blade : MonoBehaviour
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
    /// 톱날 회전속도
    /// </summary>
    public float spinSpeed = 720f;
    
    /// <summary>
    /// 목적지 웨이 포인트
    /// </summary>
    Transform target;
 
    /// <summary>
    /// 칼날
    /// </summary>
    Transform bladeMesh;

    

    private void Awake()
    {
        
        bladeMesh = transform.GetChild(0);
    }


    private void Start()
    {
        SetTarget(targetWaypoints.CurrentWayPoint); //첫번째 웨이 포인트
    }

    private void Update()
    {
        bladeMesh.Rotate(Time.deltaTime * spinSpeed * Vector3.right);   //톱날 회전
        transform.Translate(Time.deltaTime *moveSpeed * transform.forward, Space.World); //이동


        // (거리 < 0.1), (거리의 제곱 < 0.1의 제곱) 둘의 결과는 같다.
        if ( (target.position - transform.position).sqrMagnitude < 0.01f) // 거리가 0.01보다 작을 때
        {
            //도착
            SetTarget(targetWaypoints.GetNextWayPoint());   //도착했으면 다음웨이포인트 지접ㅁ 가져와서 설정하기
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
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
