using System;

namespace UnityLesson_CSharp_ClassincludingVariablesFuntions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
        // Camel case 대소문자 구분규칙 WriteLine -> 단어가 바뀔때 첫글자를 대문자로 표현해서 가독성을 높인다
        // 왜 클래스와 함수는 대문자로 시작한다, 변수는 소문자로 시작한다
        // class method namspace 등은 대문자로 시작한다
        // 띄어쓰기가 꼭 필요한 경우 '_'를 사용

        // 코드 작성시 유의사항
        // 줄임말은 되도록 사용하지 않는다 ( 누가 봐도 알수 있도록, 직관적으로 )
        // 이름을 보았을 때 어떤 기능이나 목적이 있는지 알수 있도록 선정한다.
        // 애매하거나 복잡한 내용이 있으면 주석을 달아준다.

        //class 정의 형태 
        //class 클래스이름
        //{ 
        //     멤버 변수 -> 수치화 할수 있는것들
        //     멤버 함수 -> 
        //}
       class Person
        {
            int age;
            float height;
            bool isResting;
            char genderChar;
            string name;

            void SayAllInformation()
            {
                SayAge();
                SayHeight();
                SayisResting();
                SayGenderChar();
                SayName();
            }
            void SayAge()
            {
                Console.WriteLine(age);
            }
            void SayHeight()
            {
                Console.Write(height);
            }
            void SayisResting()
            {
                Console.WriteLine(isResting);
            }
            void SayGenderChar()
            {
                Console.WriteLine(genderChar);
            }
            void SayName()
            {
                Console.WriteLine(name);
            }

        }
    }


}
