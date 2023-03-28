using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    Vector3 offset;

    private void Start()
    {
        if(target == null)
        {
            Player player =  FindObjectOfType<Player>();
            target = player.transform;
        }

        offset = transform.position - target.position;
    }



    //LateUpdate = 모든 Update 함수가 호출된 후, 마지막으로 호출됩니다 
    //움직임은 Fixed업데이트니 fixedUpdate로 바꿔주었다.
    private void FixedUpdate()
    {
        //transform.position = target.position + offset;

        //플레이어 forward와 카메라의 forward를 일치시켜야한다.(Y축만 고려)
        //Vector3 camDir = transform.forward;
        //camDir.y = 0f;

        //Quaternion camRotate = Quaternion.FromToRotation(camDir, target.forward);

        //transform.position = target.position + camRotate * offset;

        //transform.rotation *=Quaternion.FromToRotation(camDir, target.forward);
        ////Vector3.Angle(target.forward, transform.forward);
        
        
        



       // transform.position = Vector3.Lerp(transform.position,
                             //target.position + offset, Time.fixedDeltaTime * speed);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(target.position - transform.position),
            Time.fixedDeltaTime * speed
            );



    }





}
