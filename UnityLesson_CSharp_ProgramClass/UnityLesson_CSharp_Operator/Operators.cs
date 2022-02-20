using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityLesson_CSharp_Operator
{
    static public class OperatorMethods
    {
        //산술연산
        
        static public int 더하기 (int a, int b) 
        {
            return a + b;
        }
        static public int 빼기 (int a, int b)
        {
            return a - b;
        }

        static public int 곱하기 (int a, int b)
        {
            return a * b;
        }
        // 정수형 실수형 계산의 경우
        static public int 나누기(int a, int b)  //실수형나누기
        {
            return a / b;
        }
        // 오버로드 (overload)
        // 인자의 종류만 다를 경우 함수이름을 하나로 쓸 수 있다(visual studio 기능)
        static public float 나누기(float a, float b) // 정수형 나누기
        {
            return a / b;
        }
     
       



        static public int 나머지(int a, int b)
        {
            return a % b;
        }

        static public int 증가(int a)
        {
            //    return a++; 증가 연산자는 문장이 종료되고 난 후 증가 연산을 하기때문에 return시 초기값 4를 내놓고 증가시키고 함수가 종료되며 소멸
            //                무용지물
            a++;
            return a;
 
        }

        static public int 감소(int a)
        {
            a--;
            return a;
        }

        //관계연산
        static public bool 같음(int a, int b)
        {
            return a == b;
        }
        static public bool 다름(int a, int b)
       {
            return a != b;
        }
        static public bool 초과(int a, int b)
        {
            return a > b;
        }
        static public bool 이상(int a, int b)
        {
            return a >= b;
        }
        static public bool 미만(int a, int b)
        {
            return a < b;
        }
        static public bool 이하(int a, int b)
        {
            return a <= b;
        }

        //논리연산
        
        static public bool Or(bool A, bool B)
        {
            return A | B;
        }
        static public bool Xor(bool A, bool B)
        {
            return A ^ B;
        }
        static public bool And(bool A, bool B)
        {
            return A & B;
        }
        static public bool Not(bool A, bool B)
        {
            return !A;
        }

        static public bool ConditionalOr(bool A, bool B)
        {
            return A || B;
        }
        static public bool ConditionalAnd(bool A, bool B)
        {
            return A && B;
        }

        //비트연산
        //

        static public int 비트or(int a, int b) 
        {
            return a | b;
        }

        static public int 비트and(int a, int b)
        {
            return a & b;
        }

        static public int 비트not(int a)
        {
            return ~a;
        }
        static public int 비트xor(int a, int b)
        {
            return a ^ b;
        }

        static public int 비트ShiftLeft(int a, int howManyShift)
        {
            return a << howManyShift;
        }
        static public int 비트shiftright(int a, int howmanyshift)
        {
            return a >> howmanyshift;
        }
    }
}
