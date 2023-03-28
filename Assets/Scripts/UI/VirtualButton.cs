using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualButton : MonoBehaviour , IPointerClickHandler
{
    public Action onClick;
    Image coolDown;

    void Awake()
    {
        coolDown = transform.GetChild(0).GetChild(1).GetComponent<Image>();
        coolDown.fillAmount = 0;        //컴포넌트에있는 filled의 설정값
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke();
    }

    /// <summary>
    /// 쿨다운 이미지의 fill정도를 갱신하는 함수 
    /// </summary>
    /// <param name="ratio"></param>
    public void RefreshCoolTime(float ratio)
    {
        coolDown.fillAmount = ratio;
    }
}
