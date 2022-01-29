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

        static public int 나누기(int a, int b)
        {
            return a / b;
        }

        static public int 나머지(int a, int b)
        {
            return a % b;
        }

        static public int 증가(int a)
        {
            return a++;
        }

        static public int 증감(int a)
        {
            return a--;
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
        static public bool Or(bool a, bool b)
        {
            return a | b;
        }
        static public bool Xor(bool a, bool b)
        {
            return a ^ b;
        }
        static public bool And(bool a, bool b)
        {
            return a & b;
        }
        static public bool Not(bool a, bool b)
        {
            return !a;
        }

        static public bool ConditionalOr(bool a, bool b)
        {
            return a || b;
        }
        static public bool ConditionalAnd(bool a, bool b)
        {
            return a && b;
        }
    }
}
