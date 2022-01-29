using System;

namespace UnityLesson_CSharp_If
{
    internal class Program
    {    // static 함수에서는 static 변수/ static 함수만 사용 가능하다
        static bool condition1 = true;
        static bool condition2 = true;
        static bool condition3 = false;
        static void Main(string[] args)
        {
            if (condition1)// if (조건)
            {
                Console.WriteLine("조건 1이 참이다");// 조건이 참일때 실행 할 내용
            }
            else if (condition2) // 조건 1,2 둘다 참이다를 출력하고 싶으면 else if가 아니라 if를 사용한다
            {
                Console.WriteLine("조건 1이 거짓이고 조건 2가 참이다");
            }
            else if (condition3)
            {
                Console.WriteLine("조건 1,2가 거짓이고 조건 3이 참이다");
            }
            else
            {
                Console.WriteLine("조건 1,2,3이 모두 거짓이다");
            } 
        }
    }
}
