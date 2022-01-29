using System;

namespace UnityLesson_CSharp_Operator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int a = 14;
            int b = 5;
            int c = 0;


            // ==========================================================================================================
            // 산술연산
            // 더하기, 빼기, 나누기, 곱하기, 나머지


            // 더하기
            c = a + b;
            // Console.WriteLine(c);

            // 빼기
            c = a - b;
            // Console.WriteLine(c);

            // 곱하기
            c = a * b;
            // Console.WriteLine(c);

            // 나누기
            // 실수로 계산시 나머지는 버린다.
            c = a / b;
            // Console.WriteLine(c);

            // 나머지
            // 변수가 실수여도 정수로 계산한다
            c = a % b;
            //Console.WriteLine(c);

            // ==========================================================================================================
            // 증강연산
            // 증강연산자, 감소 연산자


            // 증강연산자 c = c+1
            c++;
            // 감소연산자 c = c-1
            c--;


            // ==========================================================================================================
            // 관계 연산
            // 등호와 부등호
            // 같음, 다름, 크기비교 연산 수행
            // 관계연산 결과가 참이면 true / 거짓이면 false를 반환

            bool result;
            // 같음비교
            result = a==b;
            //Console.WriteLine(result);

            // 다름 비교
            result = a!=b;
            //Console.WriteLine(result);

            // 크다
            result = a > b;
            //Console.WriteLine(result);

            // 크거나 같다
            result = a >= b;
            //Console.WriteLine(result);

            // 작다
            result = a < b;
            //Console.WriteLine(result);

            // 작거나 같다
            result = a <= b;
            //Console.WriteLine(result);


            // ==========================================================================================================
            // 논리연산 ( 논리 자료형 연산 = bool형 끼리의 연산 ) 
            // 양변의 피연산자들을 비교해서 연산수행
            // or, and, xor, not

            bool A = true;
            bool B = false;


            // or
            // a와 b 둘 중 하나라도 true이면 true/ 나머지경우  false 반환
            result = A | B; // shift \ 키
            //Console.WriteLine(result);

            // and
            // A와 B둘다 true = true / 아닐경우 false
            result = A & B;
            //Console.WriteLine(result);

            // xor
            // A와 B둘 중 하나만 true일 때 true / 아닐경우 false
            result = A ^ B;
            //Console.WriteLine(result);

            // not
            // 반대 부호 반환 true->false  flase->true
            result = !A;
            //Console.WriteLine(result);


            // ==========================================================================================================
            // 조건부 논리연산
            // 왼쪽 피연산자 조건에 따라서 오른쪽 피연산자와 비교할지 말지 평가 후에 연산
            // conditional or, conditional and

            // conditional or
            // 만약 A가 true일 경우 연산할 필요없이 true 이므로 연산을 수행 하지 않고 A를 반환
            result = A || B;

            // conditional and
            // 만약 A가 false면 B의 값에 관계없이 결과값이 false 이므로 연산을 수행 하지 않고 false를 반환
            result = A && B;




            // ==========================================================================================================
            // 비트연산 ->
            // 정수형 연산이다 int변수를 사용한다 
            // 변수 를 2진법으로 변환해서 논리연산을 한 후, 그 결과를 다시 10진법 정수로 표현한다.

            // 비트연산을 하는 이유 : 데이터를 효율적으로 관리하기 위함
            // or, and, xor, not, shift-left, shift-right

            // or 연산
            c=a|b;
            Console.WriteLine(c);

            // and
            c = a & b;
            Console.WriteLine(c);

            // xor
            c = a ^ b;
            Console.WriteLine(c);
                        
            //not
            c = ~a;
            Console.WriteLine(c);

            //shift-left 비트전체를 왼쪽으로 한칸옮기고 마지막에 있던 비트는 버림
            // << 뒤에 들어가는 숫자만큼 민다
            c = a << 1;
            Console.WriteLine(c);

           //shift-right 비트 전체를 오른쪽으로 한칸옮기고 넘어간 비트는 버림
            c = a >> 1;
            Console.WriteLine(c);


        }
    }
    // FSM (Finite State Machine) 유한상태 머신
    // 클래스의 상태에 따라서 다른 동작ㄹ을 하기위해 사용
    // ex 플레이어가 IDLE 일 때 귀환 누르면 귀환 시작
    // 피격상태/공격상태일떄는 귀환 누르면 귀환 안되게 하는 조건을
    // 아래 플레이어 상태에 따라서 나눈다
    [Flags]
    public enum e_FSM
    {
        IDEL = 0
        피격당한 상태 = 1<<0,
        공격중인 상태 = 1<<1,
        STATE_3=1<<2,
        STATE_4=1<<3,
    }

}
