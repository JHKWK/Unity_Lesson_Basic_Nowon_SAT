using System;

namespace UnityLesson_CSharp_Variable
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
    class Person
    {
        // 변수 선언 형태 : 타입 (자료형) 변수이름(대소문자 구분 함)
        //
        // Person 클래스의 멤버 변수 (필드)
        int age; // int->정수형 나이                int   : 4byte의 정수 -2147483648 ~ 2147483647
        float height; // float -> 실수형 키         float : 4byte의 실수 
        bool isResting; // 논리형 쉬고있는가?       bool  : 1 byte 논리
        char genderChar; // 문자형 성별문자         char  : 2 byte 문자
        string name; //문자열형 이름                string: 문자갯수 * 2 byte 문자열
        short num; //숫자
        // bit(비트) = 한자리 이진수 ( 0과 1, 정보처리의 최소 단위)
        // byte(바이트) = 8bit (CPU의 데이터 처리 최소 단위)
        // 4 byte = 4*8 bit = 32 bit
        // 4 byte 가 표현할 수 있는 수의 범위 2^(bit수)
    }

}