using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Blade : WayPointUser
{

 
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
        
    }

    private void FixedUpdate()
    {
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

    protected override void SetTarget(Transform target)
    {
        base.SetTarget(target);
        transform.LookAt(this.target);  //������ �ٶ󺸱�
    }


}
