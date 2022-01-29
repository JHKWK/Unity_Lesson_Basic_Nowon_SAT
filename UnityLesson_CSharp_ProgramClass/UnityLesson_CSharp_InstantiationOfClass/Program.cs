using System;

namespace UnityLesson_CSharp_InstantiationOfClass
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Person person1 = new Person();
            person1.age = 40;
            person1.height = 173;
            person1.isResting = true;
            person1.genderChar = '남';
            person1.name = "홍길동";
            person1 .SayAllInfo();
             
            Person person2 = new Person();
            person2.age = 28;
            person2.height = 182;
            person2.isResting = false;
            person2.genderChar = '남';
            person2.name = "김준수";
            person2.SayAllInfo();


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
