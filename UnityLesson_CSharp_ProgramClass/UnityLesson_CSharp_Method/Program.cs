using System;

namespace UnityLesson_CSharp_Method
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PrintHelloWord();
            PrintSomething("aaasefefsefsdfe"); // 소괄호 안 내용은 매개변수(parameter)라고 함
            bool tmpIsFinished = false;
            tmpIsFinished = PrintSomethingAndReturnIsFinished("123332123"); //
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
        static bool PrintSomethingAndReturnIsFinished(String something)
        {
            bool isFinished = false; //지역변수 (이 함수 안에서만 연산을 위해 사용)
            Console.WriteLine(something);
            isFinished = true;
            return isFinished;

        }
        /*반환형 함수이름(인자자료형 인자이름, 인자자료형 인자이름) 
        {
            함수 연산 내용
            return 반환할 내용

        }*/
    }
}
