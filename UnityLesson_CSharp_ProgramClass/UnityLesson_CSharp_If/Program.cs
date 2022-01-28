using System;

namespace UnityLesson_CSharp_If
{
    internal class Program
    {
        static bool condition1 = true;
        static bool condition2 = true;
        static bool condition3;
        static void Main(string[] args)
        {
            if (condition1) // if (조건)
            {
                Console.WriteLine("조건 1이 참이다");// 조건이 참일때 실행 할 내용
            }
            else if (condition2)
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
