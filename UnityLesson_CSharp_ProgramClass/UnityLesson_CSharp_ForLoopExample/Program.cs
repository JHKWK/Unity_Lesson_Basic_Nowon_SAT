using System;

// Orc 객체를 10마리 만들고 
// Orc들의 인스턴스는 Orc타입 배열에 넣어준다
// 각 오크의 이름은 "오크0","오크1",..."오크9"
// 각 오크에게 isResting 멤버 변수값은 랜덤으로 넣어준다.
// 각 오크가 쉬고있는지 확인해서 쉬고 있다면 점프 하도록 한다.

namespace UnityLesson_CSharp_ForLoopExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Orc[] arr_Orc = new Orc[10];
            int length = arr_Orc.Length;

            for (int i = 0; i < length; i++)
            {
                arr_Orc[i] = new Orc();
                arr_Orc[i].name = $"오크{i}"; 
                // '.'클래스안에 있는 변수에 접근한다  
                // "오크"+i
                
                //생성된 오크 이름 출력
                Console.WriteLine(arr_Orc[i].name);

                // isResting을 랜덤하게 지정한다
                arr_Orc[i].isResting = GetRandomBool();

                // isResting 상태에 따른 동작 
                if (arr_Orc[i].isResting)
                    arr_Orc[i].Jump();                
                else                
                    arr_Orc[i].Swing();                
            }

            /*
            for (int i = 0; i < 10; i++)
            {
                string orcname = $"orc{i}";
                Orc orcname = new Orc();     // 객체 생성시 객체의 이름은 생성 시 최초 선언 되어야 한다
                                             // 배열을 먼저 만든후 각각의 공간에 별도로 생성한다
                Console.WriteLine(orcname);
            }*/    
        }
        static private bool GetRandomBool()
        {
            Random random = new Random();
            int randomInt = random.Next(0, 2); // 최소값 ~ 최대값-1  0부터 시작하는 2개의 숫자
            bool randomBool = Convert.ToBoolean(randomInt); // 0은 false , 0이 아니면 true
            return randomBool;
        }
    }
    public class Orc
    {
        public int age;
        public string name;
        public char genderChar;
        public float weight;
        public float tall;
        public bool isResting;
        public void action()
        {
            if (isResting)
            {
                Console.WriteLine($"{name}이(가) 바쁘다");
            }

            else
            {
                Jump();
                Swing();
            }
        }
        public void Jump()
        {
            Console.WriteLine($"{name}이(가) 점프했다");
        }
        public void Swing()
        {
            Console.WriteLine($"{name}이(가) 휘둘렀다");
        }
    }
}