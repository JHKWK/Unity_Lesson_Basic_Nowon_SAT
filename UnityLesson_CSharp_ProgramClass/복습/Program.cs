
using System;
using System.Collections.Generic;
namespace 복습



{
    internal class Program
    {
        static void Main(string[] args)
        {
            Orc[] orcArray1 = new Orc[15];                  
            int length = orcArray1.Length;
            for (int i = 0; i < length; i++)
            {
                orcArray1[i] = new Orc();
                orcArray1[i].name = $"오크{i+1}";
                orcArray1[i].isRestiong = GetRandomBool();
                //orcArray1[i].OrcAction();
            }
            List<Orc> orcList1 = new List<Orc>();
            for (int i = 0; i < length; i++)
            {
                orcList1.Add(orcArray1[i]);
            }
            foreach (Orc item in orcList1)
            {
                item.OrcAction();
            }

            Dictionary<Orc, string> _dic = new Dictionary<Orc, string>();
            for (int i = 0; i < orcArray1.Length; i++)
            {
                _dic.Add(orcArray1[i], $"{i + 1}번째로 생성한 오크");
                Console.WriteLine($"{orcArray1[i].name} : {_dic[orcArray1[i]]}");
            }
                



        }


        static private bool GetRandomBool()
        {
            Random randomBool = new Random();
            int randomInt = randomBool.Next(0, 2);
            bool randomResult = Convert.ToBoolean(randomInt);
            return randomResult;
        }
    }
    public class Orc
    {
        public string name;
        public bool isRestiong;
        public char sex;
        public int age;
        public float tall;
        public float weight;
        
        public void Jump()
        {
            Console.WriteLine($"{name}이(가) 점프했다");
        }
        public void Swing()
        { 
            Console.WriteLine($"{name}이(가) 휘둘렀다");        
        }

        public void OrcAction()
        { 
            if (isRestiong)
                Swing();
            else
                Jump();
        }


    }

} 

