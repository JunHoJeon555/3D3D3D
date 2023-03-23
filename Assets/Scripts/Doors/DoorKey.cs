using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKey : MonoBehaviour
{
    //DoorBase는 Auto와 Manulal을 부모로 상속 중
    public DoorBase target;

    public float rotateSpeed = 360.0f;

    Transform KeyModel;

    private void Awake()
    {
        KeyModel = transform.GetChild(0);
    }

    private void Update()
    {
        KeyModel.Rotate(Time.deltaTime * rotateSpeed*Vector3.up);
    }

    private void OnTriggerEnter(Collider other)
    {
        OnConsume();
        
    }

    //컴포넌트 수정할 때 호출
    private void OnValidate()
    {
        //target에 값을 넣었다.
        if (target != null)
        {
            //target이 자동문이어야 한다.
            //자동문이 아니면 target은 null이 되어야 한다.
            //결과값은 동일

            //target이 DoorAuto인지 아닌지 확인
            //맞으면 DoorAuto면 그대로
            //아니면 target은 null;

            target = target as DoorAuto;

            //target = target.GetComponent<DoorAuto>();

            //bool isDoorAuto = tatget is DoorAuto; 얘가 bool타입으로 해준다 true,false;
            //if(!isDoorAuto)
            //{
            //  target = null;
            //}
        }
    }

   
    protected virtual void OnConsume()
    {
        
        target.Open();
        Destroy(this.gameObject);
    }
     
   


 
}
