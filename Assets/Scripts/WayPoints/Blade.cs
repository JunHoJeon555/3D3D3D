using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Blade : WayPointUser
{

    
;
 
    /// <summary>
    /// Į��
    /// </summary>
    Transform bladeMesh;

    

    private void Awake()
    {
        
        bladeMesh = transform.GetChild(0);
    }


   

    private void Update()
    { 
        bladeMesh.Rotate(Time.deltaTime * spinSpeed * Vector3.right);   //�鳯 ȸ��
        Move();
    }


    private void OnCollisionEnter(Collision collision)
    {
        // �� ������Ʈ�� �÷��̾� �ϰ� �浹�� �ϹǷ� �浹�� �Ͼ�� ������ �÷��̾��
        Player player = collision.gameObject.GetComponent<Player>();
        if(player != null ) 
        { 
            player.Die();
        }
        
    }


    /// <summary>
    /// ���� ���� ����Ʈ �����ϴ� �Լ�
    /// </summary>
    /// <param name="target"></param>
    private void SetTarget(Transform target)
    { 
        this.target = target;           //������ ����
        transform.LookAt(this.target);  //������ �ٶ󺸱�
    
    }
        
    

}
