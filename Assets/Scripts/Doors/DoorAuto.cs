using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAuto : DoorBase
{
    //자동문 


    private void OnTriggerEnter(Collider other)
    {
        
        if(other.CompareTag("Player"))      //ItemUseChecker와 겹쳤을 때 실행되는 것 방지
        {
            Open();
        }

    }



    //레이어를 사용해서 플레이어인지 아닌지 확인 할 필요x
    private void OnTriggerExit(Collider other)
    {
        Close();
    }


}
