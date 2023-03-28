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



    //LateUpdate = ��� Update �Լ��� ȣ��� ��, ���������� ȣ��˴ϴ� 
    //�������� Fixed������Ʈ�� fixedUpdate�� �ٲ��־���.
    private void FixedUpdate()
    {
        //transform.position = target.position + offset;

        //�÷��̾� forward�� ī�޶��� forward�� ��ġ���Ѿ��Ѵ�.(Y�ุ ���)
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
