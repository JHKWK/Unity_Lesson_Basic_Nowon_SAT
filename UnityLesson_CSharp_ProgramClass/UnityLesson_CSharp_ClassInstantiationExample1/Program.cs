using System;

namespace UnityLesson_CSharp_ClassInstantiationExample1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Orc orc1 = new Orc();
            orc1.name = "상급오크";
            orc1.tall = 240.2f;
            orc1.weight = 200;
            orc1.age = 140;
            orc1.genderChar = '남';
            orc1.isResting = false;
            
            Orc orc2 = new Orc();
            orc2.name = "하급오크";
            orc2.tall = 140.4f;
            orc2.weight = 120;
            orc2.age = 60;
            orc2.genderChar = '여';
            orc2.isResting = true;
            
            orc1.action();
            orc2.action();                
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
