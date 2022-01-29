using System;

namespace UnityLesson_CSharp_StaticExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Orc orc1 = new Orc();
            orc1.name = "오크1";
            orc1.age = 32;
            orc1.height = 201.21f; // 소수점 입력 마지막에 f
            orc1.isResting = false; ;
            orc1.genderChar = '남';
            Orc.typeName = "오크타입"; //static 변수에는 인스턴스(orc1)를 통해 접근할 수 없다. 클래스(Orc) 단에서 접근한다
                        
            orc1 .Jump();
            orc1 .Smash();
            Orc.SayTypeName(); //static 함수에도 인스턴스(orc1)를 통해 접근할 수 없다. 클래스(Orc) 단에서 접근한다

            Person newperson1 = new Person();

        }


    }

    public class Orc //Program Class에서 Orc Class 접근 할 수 있도록 'public'을 붙인다
    {
        //Program Class에서 Orc변수에 접근 할 수 있도록 변수에도'public'을 붙인다
        static public string typeName;
        public int age;
        public float height;
        public bool isResting;
        public char genderChar;
        public string name;

        static public void SayTypeName()
        {
            Console.WriteLine(typeName);
        }             

        public void Jump()
        {
            Console.WriteLine($"{name} (이)가 점프했다");
        }
        public void Smash()
        {
            Console.WriteLine($"{name} (이)가 휘둘렀다");
        }
        public void SayAllInfo()
        {
            SayName();
            SayAge();
            Sayheigt();
            SayIsResting();
            SayGenderChar();
            SayTypeName();


        }
        public void SayAge()
        {
            Console.WriteLine($"{name}의 나이 : {age}");

        }
        public void Sayheigt()
        {
            Console.WriteLine($"{name}의 키 : {height}");

        }
        public void SayIsResting()
        {
            if (isResting == true)
            
                Console.WriteLine($"{name}은 쉬고 있습니다");
            

            else
                Console.WriteLine($"{name}은 쉬고 있지 않습니다");

        }
        public void SayGenderChar()
        {
            Console.WriteLine($"{name}의 성별 : {genderChar}");

        }
        public void SayName()
        {
            Console.WriteLine($"이름 : {name}");

        }
    }



    public class Person
    {
        public int age;
        public float height;
        public bool isResting;
        public char genderChar;
        public string name;

        public void SayAllInfo()
        {
            SayName();
            SayAge();
            Sayheigt();
            SayIsResting();
            SayGenderChar();

        }


        public void SayAge()
        {
            Console.WriteLine($"{name}의 나이 : {age}");

        }
        public void Sayheigt()
        {
            Console.WriteLine($"{name}의 키 : {height}");

        }
        public void SayIsResting()
        {
            if (isResting == true)
            {
                Console.WriteLine($"{name}은 쉬고 있습니다");
            }

            else
                Console.WriteLine($"{name}은 쉬고 있지 않습니다");

        }
        public void SayGenderChar()
        {
            Console.WriteLine($"{name}의 성별 : {genderChar}");

        }
        public void SayName()
        {
            Console.WriteLine($"이름 : {name}");

        }
    }
}
