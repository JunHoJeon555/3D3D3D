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
        coolDown.fillAmount = 0;        //������Ʈ���ִ� filled�� ������
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke();
    }

    /// <summary>
    /// ��ٿ� �̹����� fill������ �����ϴ� �Լ� 
    /// </summary>
    /// <param name="ratio"></param>
    public void RefreshCoolTime(float ratio)
    {
        coolDown.fillAmount = ratio;
    }
}
