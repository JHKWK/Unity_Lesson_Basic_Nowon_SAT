using System;
enum e_PlayerState //enum + type의 형태로 선언 // 내용물은 ,를 붙여가며 열거
{
    Idle,   // ...00000000
    Attack, // ...00000001
    Jump,   // ...00000010
    Walk,   // ...00000011
    Run,    // ...00000100
    Dash,   // ...00000101
    Home,   // ...00000110
    Test,   // ...00000111

    // switch-case에 적합한 형태,
    // 각 요소들이 동시에 일어나는 경우가 없는 상황에 적합한 형태
    // 특히 FSM (Finite State Machine)유한 상태 머신.
    // 순서나 상태가 정해져있는 경우
}
[Flags] // ToString() 속성을 참조할 때 중복되는 모든 요소들에 대해 표현이 가능(문자열로 형 변환이 가능)
enum e_PlayerStateFlags
{ 
    Idel = 0,      // ...00000000
    Attack =1<<0,  // ...00000001
    Jump = 1<<1,   // ...00000010
    Walk = 1<<2,   // ...00000100
    Run = 1<<3,    // ...00001000
    Dash = 1<<4,   // ...00010000
    Home = 1<<5,   // ...00100000
    Test = 1<<6,   // ...01000000
    
    // DashAttack = Attack | Dash, // 00010001  여러가지 상태가 중첩될 수 있다 그러나 생성할 수 있는 경우의 수가 줄어든다
    // 각 요소들이 동시에 일어나는 경우가 있는 상황에 적합한 형태   
}

namespace UnityLesson_CSharp_EnumAndSwitchCase
{
    internal class Program
    {
        //Casting 캐스팅
        // 비트정보 그대로 들고와서 타입만 변경시킴
        // (e_PlayerState)1 의 형태를 취함
        static e_PlayerState createMotion = e_PlayerState.Test;  
        static void Main(string[] args)
        {
            //Enum-bit
            e_PlayerStateFlags flags = e_PlayerStateFlags.Jump | e_PlayerStateFlags.Attack;
            Console.WriteLine(flags);

            Warrior warrior1 = new Warrior();
            Console.WriteLine("생성할 전사의 이름을 입력하세요");
            warrior1.name= Console.ReadLine();
            Console.WriteLine($"{ warrior1.name}(이)가 생성 되었습니다");

            //if문을 이용한 분기
            if (createMotion == e_PlayerState.Idle)
            {
                // do nothing
            }
            else if (createMotion == e_PlayerState.Attack)
            {
                warrior1.Attack();
            }
            else if (createMotion == e_PlayerState.Jump)
            {
                warrior1.Jump();
            }
            else if (createMotion == e_PlayerState.Walk)
            {
                warrior1.Walk();
            }
            else if (createMotion == e_PlayerState.Run)
            {
                warrior1.Run();
            }
            else if (createMotion == e_PlayerState.Dash)
            {
                warrior1.Dash();
            }
            else if (createMotion == e_PlayerState.Home)
            {
                warrior1.Home();
            }
            else
            {
                Console.WriteLine("어 상태가 이상한데");
            }

            //전사에게 동작 명령하기
            Console.WriteLine($"{warrior1.name}에게 명령을 내려 주세요");
            string motionInput = Console.ReadLine();
            e_PlayerState motion; // 오브젝트 생성
            

            //true flase 검토
            bool isParased = Enum.TryParse(motionInput, out motion);
            if (isParased)//명령어가 참일경우
            {
                //e_Playerstate 타입의 motion 오브젝트에 입력값을 넣는다
                motion = (e_PlayerState)Enum.Parse(typeof(e_PlayerState), motionInput); 

                //case를 활용하여 입력값에 따른 분기문을 출력
                //switch -case 분기
                // switch (경우의수/매개변수)
                switch (motion)
                {
                    case e_PlayerState.Idle:
                        // do nothing
                        break;
                    case e_PlayerState.Attack:
                        warrior1.Attack();
                        break;
                    case e_PlayerState.Jump:
                        warrior1.Jump();
                        break;
                    case e_PlayerState.Walk:
                        warrior1.Walk();
                        break;
                    case e_PlayerState.Run:
                        warrior1.Run();
                        break;
                    case e_PlayerState.Dash:
                        warrior1.Dash();
                        break;
                    case e_PlayerState.Home:
                        warrior1.Home();
                        break;
                    default:
                        Console.WriteLine("전사는 그런거 할줄 몰라요");
                        break;
                }
            }
            else
                Console.WriteLine("잘못된 입력 입니다");


        }
    }
    public class Warrior
    {
        public string name;
        public void Attack()
        {
            Console.WriteLine($"{name}(이)가 공격함");
        }
        public void Jump()
        {
            Console.WriteLine($"{name}(이)가 점프함");
        }
        public void Walk()
        {
            Console.WriteLine($"{name}(이)가 걷고있음");
        }
        public void Run()
        {
            Console.WriteLine($"{name}(이)가 달린다");
        }
        public void Dash()
        {
            Console.WriteLine($"{name}(이)가 돌진함");
        }
        public void Home()
        {
            Console.WriteLine($"{name}(이)가 귀환함");
        }
    }

}
