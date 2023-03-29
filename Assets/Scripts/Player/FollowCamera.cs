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
            target = player.transform.GetChild(7);
        }

        offset = transform.position - target.position;
        lenght = offset.magnitude; //루트연산이여서 절대 피해랴하는것
    }



    //LateUpdate = 모든 Update 함수가 호출된 후, 마지막으로 호출됩니다 
    //움직임은 Fixed업데이트니 fixedUpdate로 바꿔주었다.
    private void FixedUpdate()
    {



       // transform.position = Vector3.Lerp(transform.position,
                             //target.position + offset, Time.fixedDeltaTime * speed);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(target.position - transform.position),
            Time.fixedDeltaTime * speed     ///ㅂ간
            );


        transform.LookAt(target);           //카메라가 목표지점 바라보기 


        Ray ray = new Bay(target, tramsformm.position - target.position);
        if (Physics.Raycast(ray, out RaycastHit, lengh))
        {

        }

    }





}
