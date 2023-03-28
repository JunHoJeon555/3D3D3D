using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Rendering.UI;

public class VirtualStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    /// <summary>
    /// ����ƽ�� �Է��� �˸��� �������̤�Ʈ
    /// </summary>
    public Action<Vector2> onMoveInput;

    /// <summary>
    /// ��ü������ rect
    /// </summary>
    RectTransform containerRect;

    /// <summary>
    /// �ڵ��� rect
    /// </summary>
    RectTransform handleRect;

    /// <summary>
    /// �ڵ��� ������ �� �ִ� �ִ� �Ÿ�
    /// </summary>
    float stickRange;


    private void Awake()
    {
        containerRect = transform as RectTransform;
        Transform child = transform.GetChild(0);
        handleRect = child as RectTransform;
        //

        stickRange = (containerRect.rect.width - handleRect.rect.width) * 0.5f; //�ִ� �Ÿ� ���ϱ�
    }



    public void OnDrag(PointerEventData eventData)
    {
        //containerRect�� �Ǻ����� �󸶸�ŭ �̵��ߴ����� position�� ��
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            containerRect, eventData.position, eventData.pressEventCamera, out Vector2 position);
        handleRect.anchoredPosition = position;

        position = Vector2.ClampMagnitude(position, stickRange);    //�������� stickRange�� ���� ������ Ŭ����
       

        InputUpdate(position);  //position��ŭ �ڵ� �����̰� ��ȣ ������
    }

    /// <summary>
    /// ��ƽ�� �������� �Է����� �����ؼ� �ڵ��� �����̰� ��ȣ�� ������ �Լ�
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        InputUpdate(Vector2.zero);          //���콺�� ���� �� �ڵ��� �߸� ��ġ�� �ʱ�ȭ
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //�������;� ������  ���� ��. ������ ���� ����x    
    }

    /// <summary>
    /// ��ƽ�� �������� �̓����� ���椩�ؼ� �ڵ��� �����̰� ��ȣ�� ������ �Լ�
    /// </summary>
    /// <param name="pos"></param>
    private void InputUpdate(Vector2 pos)
    {
       handleRect.anchoredPosition = pos;       //��Ŀ���� 
       onMoveInput?.Invoke(pos/stickRange);     //������ -1~1��
    }
}



