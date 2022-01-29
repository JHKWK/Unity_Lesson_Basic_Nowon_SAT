using System;

namespace UnityLesson_CSharp_ClassObjectInstance_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //AA 객체생성
            //new 키워드 
            new AA();          // 생성자 : 클래스 이름과 똑같은 함수. 객체를 생성하고 '반환함'
            AA a1 = new AA();  // 생성자가 생성한 객체를 저장
            // 이 과정을 '인스턴스화'라고 한다.
            // = 메모리 공간에 객체를 할당시킨다.
            // 'new AA()'로 생성하여 반환한 'AA타입의 객체'가 'AA타입 변수 a1'에 할당 되었다
            // 이렇게 하면 aa 변수를 통해서 생성된 객체에 접근 할 수가 있게 된다.
            // 여기서 새로 '생성된 객체'가 할당된 'a1변수'를 '인스턴스' 라고 한다

        }
    }
    // AA클래스
    // 접근 제한자
    // public     : 다른 모든 클래스에서 접근 할 수 가 있음.
    // private    : 다른 클래스에서 접근할 수 없음.
    // protected  : 상속받은 클래스에서만 접근할 수 있음.
    // internal   : 같은 어셈블리(같은 프로젝트) 에서만 접근할 수 있음
    // 기본적으로 접근 제한자를 명시하지 않으면 private이 디폴트.
    public class AA
    {
    }

}
