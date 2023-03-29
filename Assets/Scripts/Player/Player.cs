using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    /// <summary>
    /// 이동속도
    /// </summary>
    public float moveSpeed = 5f;

    /// <summary>
    /// 현재 이동속도
    /// </summary>
    private float currentMoveSpeed = 5f;


    /// <summary>
    /// 회전속도
    /// </summary>
    public float rotateSpeed = 180f;

    /// <summary>
    /// 점프력
    /// </summary>
    public float jumpForce = 5f;

    /// <summary>
    /// 플레이어의 최대수명, 수명이 다되면 사망
    /// </summary>
    public float lifeTimeMax = 3f;

    public float zVector = 10f;

    /// <summary>
    /// 플레이어의 현재 수명, 수명이 0보다 작거나 같아지면 사망
    /// </summary>
    float lifeTime = 3f;

    /// <summary>
    /// 수명용 프로퍼티
    /// </summary>
    public float LifeTime
    {
        get => lifeTime;
        private set
        {
            lifeTime = value;                       //수명 변경
            onLifeTimeChange?.Invoke(lifeTime / lifeTimeMax); //수명 변경을 알림 (비율을 알려줌)

            if (lifeTime <= 0)                      //수명이 다 되면 사망
            {
                Die();
            }
        }
    }

    /// <summary>
    /// 생명여부 표시
    /// </summary>
    bool isAlive = true;


    /// <summary>
    /// 
    /// </summary>
    public Action<float> onLifeTimeChange;



    /// <summary>
    /// 현재이동방향
    /// </summary>
    float moveDir = 0f;   //-1 ~ 1 사이 (1:앞 -1:뒤)

    /// <summary>
    /// 회전방향
    /// </summary>
    float rotateDir = 0f; //-1 ~ 1 사이 (1:우 -1:좌)

    /// <summary>
    /// 현재 점프 여부 true면 점프, false면 점프 중 아님
    /// </summary>
    bool isJumping = false;

    /// <summary>
    /// 현재 점프 여부. true
    /// </summary>
    public float jumpCoolTime = 5f;
    public float jumpCoolTimeMax = 5f;

    /// <summary>
    /// 쿨타임 변경될 때마다 신호를 보내기 위한 프로퍼티
    /// </summary>
    private float JumpCoolTime
    {
        get => jumpCoolTime;
        set
        {
            jumpCoolTime = value;
            if (jumpCoolTime < 0)
            {
                jumpCoolTime = 0;
            }
            onJumpCoolTimeChange?.Invoke(jumpCoolTime / jumpCoolTimeMax);   // 쿨타임이 변경되면 비율을 알려줌
        }
    }

    /// <summary>
    /// 쿨타임 변경될 때 실행될 델리게이트
    /// </summary>
    Action<float> onJumpCoolTimeChange;

    /// <summary>
    /// true면 점프 쿨타임 완료 false는 쿨타임 중
    /// </summary>
    private bool IsJumpCoolEnd => jumpCoolTime <= 0;
    
    public Action onDie;

    /// <summary>
    /// rigid 읽기 전용 프로퍼티
    /// </summary>
    public Rigidbody Rigid => rigid;


    Rigidbody rigid;
    PlayerInputActions inputActions;
    Animator anim;
    
    
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        inputActions = new PlayerInputActions();
        anim= GetComponent<Animator>();

        //아이템 사용 알람이 울리면 실행될 함수 등록
        ItemUseAlarm alarm = GetComponentInChildren<ItemUseAlarm>();
        alarm.onUseableItemUsed += UseObject;

        
    }

  

    private void OnEnable()
    {   
        inputActions.Player.Enable();                      //플레이어 인풋 액션 맵 활성화
        inputActions.Player.Move.performed += OnMoveInput; // 액션들에게 함수 바인딩하기
        inputActions.Player.Move.canceled+= OnMoveInput;   //키를 안 누르고 있을 때
        inputActions.Player.Use.performed += OnUseInput;
        inputActions.Player.Jump.performed += OnJumpInput;

        isAlive = true;
        //lifeTime = lifeTimeMax; //변수값을 변경하는 것
        LifeTime = lifeTimeMax;                     //프로퍼티를 실행하는 것
        JumpCoolTime= 0f;             //변경될때마다 알려된다

        ResetMoveSpeed(); //처음 속도 지정
    }

    private void OnDisable()
    {
        
        inputActions.Player.Jump.performed -= OnJumpInput; //액션에 연결된 함수들 바인딩 해제
        inputActions.Player.Use.performed -= OnUseInput;    
        inputActions.Player.Move.canceled -= OnMoveInput;
        inputActions.Player.Move.performed -= OnMoveInput;
        inputActions.Player.Disable();                     //플레이어 액션맵 비활성화

    }
    private void Start()
    {
        //가상 스틱연결
        VirtualStick stick = FindObjectOfType<VirtualStick>();
        if(stick != null )
        {
        stick.onMoveInput += (input) => SetInput(input, input != Vector2.zero);  //가상 스틱의 입력이 있으면 이동처리

        }

        //가상 버튼 연결 
        VirtualButton button = FindObjectOfType<VirtualButton>();
        if(button != null)
        {

        button.onClick += Jump;                                  // 가상 버튼이 눌려지면 점프
        onJumpCoolTimeChange += button.RefreshCoolTime;         //점프 쿨타임이 변하면 버튼의 쿨타임 표시 변경
        }

    }
    private void Update()
    {
        LifeTime -= Time.deltaTime;
        jumpCoolTime -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Move();         //이동처리
        Rotate();       //회전처리   
    }

   

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) //Ground와 충돌했을 때만 
        {
            OnGround(); //찾기 함수 실행
        }

        else if(collision.gameObject.CompareTag("Platform"))
        {  
            OnGround();     //바닥에 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            Platform platform = other.gameObject.GetComponent<Platform>();
            platform.OnMove += OnRideMovingObject;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            Platform platform = other.gameObject.GetComponent<Platform>();
            platform.OnMove -= OnRideMovingObject;
        }
    }



    




    //private void LateUpdate() // 카메라 같은거 처리할 때 아니면 모든 업데이트를 사용할 때 마지막으로 계산을 해줘야 할 때



    private void OnMoveInput(InputAction.CallbackContext context)
    {
       Vector2 input = context.ReadValue<Vector2>(); //context에 포함되는것들이 꽤 있다.

        SetInput(input, !context.canceled);
        
    }

    /// <summary>
    /// 입력에 따라 이동처리용 변수에 값을 설정하는 함수
    /// </summary>
    /// <param name="input">이동 방향</param>
    /// <param name="isMove">이동 중인지 아닌지</param>
    private void SetInput(Vector2 input, bool isMove)
    {
        rotateDir = input.x; //좌우  (좌:-1 우:+1)
        moveDir = input.y;   //앞뒤  (앞:+1 뒤:-1)
                             //Debug.Log(input); 값 확인

        //context.performed : 액션에 연결된 키 중 하나라도 입력 중이면 true 아니면 false
        //context.canceled  : 액션에 연결된 키가 모두 입력 중이지 않으면 true 아니면 false

        anim.SetBool("IsMove", isMove);  //애니메이션 파라메터 변경(Idle, Move 중 선택)
    }


    private void OnUseInput(InputAction.CallbackContext context)
    {
        anim.SetTrigger("Use");
    }

    private void OnJumpInput(InputAction.CallbackContext context)
    {

        Jump(); //점프 처리 함수 실행
        
    }   

    /// <summary>
    /// 이동처리 함수
    /// </summary>
    void Move()
    {
        //moveDir 방향으로 이동 시키기 (앞 아니면 뒤)    
        rigid.MovePosition(rigid.position + Time.fixedDeltaTime * currentMoveSpeed * moveDir * transform.forward); // transform.forward z축방향   
        
    }

    /// <summary>
    /// 회전처리 함수
    /// </summary>
   void Rotate()
    {
        //rigid.AddTorque(); //회전력 추가
        //rigid.MoveRotation();// 특정회전으로 설정하기

        //Quaternion rotate = Quaternion.Euler(0,
        //    Time.fixedDeltaTime*rotateSpeed*rotateDir,
        //    0);

        //특정 축을 기준으로 회전 시키는 쿼터니언을 만드는 함수
        Quaternion rotate = Quaternion.AngleAxis(Time.fixedDeltaTime * 
                            rotateSpeed * rotateDir, transform.up); //transform.up = y축을 고정으로 회전//플레이어의 up방향을 기준이로 

        //위에서 만든 회전을 사용
        rigid.MoveRotation(rigid.rotation * rotate);

     
    }

    
    /// <summary>
    /// 점프 처리 함수
    /// </summary>
    void Jump()
    {
        if (!isJumping && IsJumpCoolEnd) //점프 중이 아니고 쿨타임이 다 되었을 때만 가능 
        {
            JumpCoolTime = jumpCoolTimeMax;
            rigid.AddForce(jumpForce * Vector3.up, ForceMode.Impulse); ; //월드의 Up방향으로 힘을 즉시 가하기
            isJumping = true;   //점프 중이라고 표시
        }
    }

    /// <summary>
    /// 착지했을 때 처리 함수(무한 점프 방지) 
    /// </summary>
    private void OnGround()
    {
        isJumping = false; //점프가 끝났다고 표시
    }

    //delegate 연결하기
    /// <summary>
    /// 아이템 사용한다는 알람이 오면 실행되는 함수
    /// </summary>
    /// <param name="obj">사용할 오브젝트</param>
    private void UseObject(IUserbleObject obj)
    {
        if (obj.IsDirectUse)
        {
            obj.Used(); // 사용
        }
    }


    /// <summary>
    /// 플레이어가 사망했을 때 실행되는 함수
    /// </summary>
    public void Die()
    {
        //뒤로 넘어지는 방향 구하기
        
        if(isAlive)
        {
            //Destroy(this.gameObject);
            anim.SetTrigger("Die");

            //pitch와 roll회전이 막혀있던 것을풀기
            //rigid.constraints = RigidbodyConstraints.None;
            rigid.freezeRotation = false;

            //액션맵 비활성화
            inputActions.Player.Disable();
               
            //머리 위치 설정 
            Transform head = transform.GetChild(0);
        
            //뒤로 가는 방향
            Vector3 dir = -(transform.forward).normalized;
        
            //머리위치에 -z값의 힘을 가한다.
            rigid.AddForceAtPosition(dir* zVector, head.position, ForceMode.Impulse);
        
            //플레이어의 up벡터를 축으로 1만큼 회전력 더하기
            rigid.AddTorque(transform.up, ForceMode.Impulse);

            //델리게이트 신호 보내기
            onDie?.Invoke();

            //죽음 표시
            isAlive = false;

        }
    }

    public void SetForceJumpMode()
    {
        isJumping = true;
    }

    public void SetHalfSpeed()
    {
        currentMoveSpeed= moveSpeed*0.5f;
    }

    public void ResetMoveSpeed()
    {
        currentMoveSpeed= moveSpeed;
    }

    /// <summary>
    /// 플레이어의 움직임이 플랫폼의 움직임을 더해준다/
    /// </summary>
    /// <param name="delta"></param>
    private void OnRideMovingObject(Vector3 delta)
    {
        rigid.MovePosition(rigid.position + delta);
    }

}
