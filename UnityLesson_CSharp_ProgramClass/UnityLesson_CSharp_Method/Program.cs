using System;

namespace UnityLesson_CSharp_Method
{
    internal class Program
    {
        static void Main(string[] args) //viod 의미 : 반환할것이 없다
        {
            PrintHelloWord();
            PrintSomething("aaasefefsefsdfe"); // 소괄호 안 내용은 매개변수(parameter)라고 함
            bool tmpIsFinished = false;
            tmpIsFinished = PrintSomethingAndReturnIsFinished("12345"); //
           Console.WriteLine(tmpIsFinished);
        }

        // 인자 X, 반환 X
        static void PrintHelloWord()
        {

            Console.WriteLine("Hello World!");
        }

        // 인자 0, 반환 X
        static void PrintSomething(String something)
        {
            Console.WriteLine(something);
        }

        // 인자 0, 반환 0
        static bool PrintSomethingAndReturnIsFinished(String something) // 반환 할 것이 있기 때문에 viod를 쓰지 않았음
                                                                        // bool -> 반환하려는 정보가 true/false
        {
            bool isFinished = false; //지역변수 (이 함수 안에서만 연산을 위해 사용)
                                     //   Console.WriteLine(something);
                                     //   isFinished = true;
                                     //   return isFinished;

            if (something == "1234") 
            {
                isFinished = true;
                Console.WriteLine("정답");
            }
            else
            {
                isFinished = false;
                Console.WriteLine("오답");                    
            }

            return isFinished;

        }
        /*반환형(자료형과 비슷 반환할) 함수이름(인자자료형 인자이름, 인자자료형 인자이름)  인자:argument
        {
            함수 연산 내용
            return 반환할 내용

        }*/
    }
}
