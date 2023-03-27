using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



//기본적으로 public이다 ..
//인터페이스는 클래스에 여러개의 인터페이스를  상속 받을 수 있다.
//인터페이스에는 구현이 포함되어있지 않아야 한다
//인터페이스를 상속받은 클래스는 반드시 멤버 함수들을 구형해야한다.
//인터페이스에는 변수가 들어갈 수 없다. (프로퍼티는 특수한 형태의 함수)

public interface IUserbleObject
{
    bool IsDirectUse 
    {
        get;
    }

    void Used();

}
